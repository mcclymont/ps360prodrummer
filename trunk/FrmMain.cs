using System;
using System.Windows.Forms;
using CarlsMidiTools; // http://www.franklins.net/dotnet.aspx
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Threading;

namespace _PS360Drum
{
    public delegate byte Byte_DrumPadDelegate(DrumPad pad);
    public delegate bool Bool_DrumPadDelegate(DrumPad pad);
    public delegate void Void_DrumPadByteDelegate(DrumPad pad, byte velocity);
    public partial class FrmMain : Form
    {
        ProgressBar[] referenceVelocity = new ProgressBar[ProDrumController.NUM_PADS];
        public GuiLinker GuiLinker { get; private set; }

        Int32 _lowerValues;
        String defaultFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PS360Drum.p3s");

        Random m_Random = new Random();

        public MidiSender MidiSender { get; private set; }
        private ProDrumController m_DrumController;
        private Serializer m_Serializer;


        private DrumDPad m_PrevDPadState = DrumDPad.None;

        public FrmMain()
        {    
            InitializeComponent();

            m_Serializer = new Serializer(this);
            m_DrumController = new ProDrumController(this);
            m_DrumController.ButtonPressedEvent += DrumButtonPressed;
            m_DrumController.ButtonReleasedEvent += DrumButtonReleased;
            m_DrumController.DPadStateChanged += DrumDPadStateChanged;

            MidiSender = new MidiSender(this);

            // Lists MIDI devices
            ddlMIDIDevice.DataSource = MidiSender.MidiDevices;

            // Sets default file browser properties
            openFileDialog.Filter = "PS360 Setting Files|*.p3s";
            openFileDialog.DefaultExt = "*.p3s";
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            openFileDialog.FileName = "PS360DrumSettings";
            saveFileDialog.Filter = openFileDialog.Filter;
            saveFileDialog.DefaultExt = openFileDialog.DefaultExt;
            saveFileDialog.InitialDirectory = openFileDialog.InitialDirectory;
            saveFileDialog.FileName = openFileDialog.FileName;

            #region Gui link stuff
            GuiLinker = new GuiLinker(this);

            GuiLinker.AddDrum(DrumPad.RedTom, nupRedTom, nupAdvNoteRedTom, chkBoostRedTom, chkAdvBoostRedTom, ddlRedTom, ddlAdvBoostRedTom);
            GuiLinker.AddDrum(DrumPad.YellowTom, nupYellowTom, nupAdvNoteYellowTom, chkBoostYellowTom, chkAdvBoostYellowTom, ddlYellowTom, ddlAdvBoostYellowTom);
            GuiLinker.AddDrum(DrumPad.YellowCymbal, nupYellowCymbal, nupAdvNoteYellowCymbal, chkBoostYellowCymbal, chkAdvBoostYellowCymbal, ddlYellowCymbal, ddlAdvBoostYellowCymbal);
            GuiLinker.AddDrum(DrumPad.BlueTom, nupBlueTom, nupAdvNoteBlueTom, chkBoostBlueTom, chkAdvBoostBlueTom, ddlBlueTom, ddlAdvBoostBlueTom);
            GuiLinker.AddDrum(DrumPad.BlueCymbal, nupBlueCymbal, nupAdvNoteBlueCymbal, chkBoostBlueCymbal, chkAdvBoostBlueCymbal, ddlBlueCymbal, ddlAdvBoostBlueCymbal);
            GuiLinker.AddDrum(DrumPad.GreenTom, nupGreenTom, nupAdvNoteGreenTom, chkBoostGreenTom, chkAdvBoostGreenTom, ddlGreenTom, ddlAdvBoostGreenTom);
            GuiLinker.AddDrum(DrumPad.GreenCymbal, nupGreenCymbal, nupAdvNoteGreenCymbal, chkBoostGreenCymbal, chkAdvBoostGreenCymbal, ddlGreenCymbal, ddlAdvBoostGreenCymbal);

            GuiLinker.AddButton(DrumButton.BigButton, nupBigButton, nupAdvNoteBigButton, nupAdvVelBigButton, ddlAdvSwitchBigButton, chkBigButton);
            GuiLinker.AddButton(DrumButton.Circle, nupCircle, nupAdvNoteCircle, nupAdvVelCircle, ddlAdvSwitchCircle, chkCircle);
            GuiLinker.AddButton(DrumButton.PedalLeft, nupPedalLeft, nupAdvNotePedalLeft, nupAdvVelPedalLeft, ddlAdvSwitchPedalLeft, chkPedalLeft);
            GuiLinker.AddButton(DrumButton.PedalRight, nupPedalRight, nupAdvNotePedalRight, nupAdvVelPedalRight, ddlAdvSwitchPedalRight, chkPedalRight);
            GuiLinker.AddButton(DrumButton.Rectangle, nupRectangle, nupAdvNoteRectangle, nupAdvVelRectangle, ddlAdvSwitchRectangle, chkRectangle);
            GuiLinker.AddButton(DrumButton.Select, nupSelect, nupAdvNoteSelect, nupAdvVelSelect, ddlAdvSwitchSelect, chkSelect);
            GuiLinker.AddButton(DrumButton.Start, nupStart, nupAdvNoteStart, nupAdvVelStart, ddlAdvSwitchStart, chkStart);
            GuiLinker.AddButton(DrumButton.Triangle, nupTriangle, nupAdvNoteTriangle, nupAdvVelTriangle, ddlAdvSwitchTriangle, chkTriangle);
            GuiLinker.AddButton(DrumButton.X, nupX, nupAdvNoteX, nupAdvVelX, ddlAdvSwitchX, chkX);

            GuiLinker.AddDPad(DrumDPad.Up, nupDPadUp, nupAdvNoteDPadUp, nupAdvVelDPadUp, ddlAdvSwitchDPadUp, chkDPadUp);
            GuiLinker.AddDPad(DrumDPad.Down, nupDPadDown, nupAdvNoteDPadDown, nupAdvVelDPadDown, ddlAdvSwitchDPadDown, chkDPadDown);
            GuiLinker.AddDPad(DrumDPad.Right, nupDPadRight, nupAdvNoteDPadRight, nupAdvVelDPadRight, ddlAdvSwitchDPadRight, chkDPadRight);
            GuiLinker.AddDPad(DrumDPad.Left, nupDPadLeft, nupAdvNoteDPadLeft, nupAdvVelDPadLeft, ddlAdvSwitchDPadLeft, chkDPadLeft);

            referenceVelocity[(int)DrumPad.RedTom] = pbRedTom;
            referenceVelocity[(int)DrumPad.YellowTom] = pbYellowTom;
            referenceVelocity[(int)DrumPad.BlueTom] = pbBlueTom;
            referenceVelocity[(int)DrumPad.GreenTom] = pbGreenTom;
            referenceVelocity[(int)DrumPad.YellowCymbal] = pbYellowCymbal;
            referenceVelocity[(int)DrumPad.BlueCymbal] = pbBlueCymbal;
            referenceVelocity[(int)DrumPad.GreenCymbal] = pbGreenCymbal;
            #endregion

            // Tries to load default settings or reverts to 
            if (File.Exists(defaultFilePath))
            {
                m_Serializer.Load(defaultFilePath);
            }
            else
            {
                m_Serializer.LoadDefaultSettings();
            }

            // Sets high priority and enables search for drums
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
        }

        private void ButtonPress(object sender, EventArgs e)
        {
            //Button b = sender as Button;
            //byte vel = (byte)m_Random.Next(0, 255);
            //MidiSender.TriggerNote((DrumPad)b.Tag, vel);
        }
        private void DrumButtonPressed(DrumButton b)
        {
            if (InvokeRequired)
            {
                Invoke(new ProDrumController.ButtonDelegate(DrumButtonPressed), new object[] { b });
            }
            else
            {
                GuiLinker.CheckboxButton(b, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(b), 127);
            }
        }
        private void DrumButtonReleased(DrumButton b)
        {
            if (InvokeRequired)
            {
                Invoke(new ProDrumController.ButtonDelegate(DrumButtonReleased), new object[] { b });
            }
            else
            {
                GuiLinker.CheckboxButton(b, false);
                MidiSender.SendNoteOff(GuiLinker.GetMidiNote(b));
            }
        }
        private void DPadOn(DrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");

            if (GuiLinker.GetButtonChecked(dp) == false)
            {
                GuiLinker.CheckboxButton(dp, true);
                MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
            }
        }
        private void DPadOn(DrumDPad dp, DrumDPad dp2)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            Debug.Assert((byte)dp2 % 2 == 0, "only left, up, right, down is allowed here");

            if (GuiLinker.GetButtonChecked(dp) == false)
            {
                GuiLinker.CheckboxButton(dp, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(dp), 127);
            }
            if (GuiLinker.GetButtonChecked(dp2) == false)
            {
                GuiLinker.CheckboxButton(dp2, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(dp2), 127);
            }
        }
        private void DPadOff(DrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");

            if (GuiLinker.GetButtonChecked(dp) == true)
            {
                GuiLinker.CheckboxButton(dp, false);
                MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
            }
        }
        private void DrumDPadStateChanged(DrumDPad dp)
        {
            if (m_PrevDPadState == dp)
                return;
            if (InvokeRequired)
            {
                Invoke(new ProDrumController.DPadDelegate(DrumDPadStateChanged), new object[] { dp });
            }
            else
            {
                int new1, new2;
                if ((byte)dp % 2 == 0)
                {
                    new1 = (byte)dp;
                    new2 = (byte)dp;
                }
                else
                {
                    new1 = (byte)dp - 1;
                    new2 = (byte)dp + 1;
                    if (new2 == 8) new2 = 0;
                }

                if (dp != DrumDPad.None)
                {
                    DPadOn((DrumDPad)new1, (DrumDPad)new2);
                }
                if (m_PrevDPadState != DrumDPad.None)
                {
                    if ((byte)m_PrevDPadState % 2 == 0)
                    {
                        if ((byte)m_PrevDPadState != new1 && (byte)m_PrevDPadState != new2)
                            DPadOff(m_PrevDPadState);
                    }
                    else
                    {
                        int prev1 = (byte)m_PrevDPadState - 1;
                        int prev2 = (byte)m_PrevDPadState + 1;
                        if (prev2 == 8) prev2 = 0;

                        if (prev1 != new1 && prev1 != new2)
                            DPadOff((DrumDPad)prev1);
                        if (prev2 != new1 && prev2 != new2)
                            DPadOff((DrumDPad)prev2);
                    }
                }                
                m_PrevDPadState = dp;
            }
        }
        private void UpdateMidi(object sender, EventArgs e)
        {
            MidiSender.UpdateMidiSettings();
        }

        #region Getters/Setters
        public string GetMidiOutDeviceName()
        {
            return ddlMIDIDevice.SelectedValue as string;
        }
        public byte GetMidiChannel()
        {
            return (byte)nupMIDIChannel.Value;
        }

        public void SetMidiOutDeviceName(string deviceName)
        {
            GuiLinker.SetSelectIndex(ref ddlMIDIDevice, deviceName);
        }
        public void SetMidiChannel(byte channel)
        {
            nupMIDIChannel.Value = channel;
        }
        #endregion

        #region Settings Methods
        private void SaveSettingsClick(object sender, EventArgs e)
        {
            DialogResult dlgResult = saveFileDialog.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                m_Serializer.Save(saveFileDialog.FileName);
            }
        }
        private void LoadSettingsClick(object sender, EventArgs e)
        {
            DialogResult dlgResult = openFileDialog.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                m_Serializer.Load(saveFileDialog.FileName);
            }
        }
        private void SaveDefaultSettings(object sender, FormClosingEventArgs e)
        {
            m_Serializer.Save(defaultFilePath);
        }
        private void LoadDefaultSettings(object sender, EventArgs e)
        {
            m_Serializer.Load(defaultFilePath);
        }
        #endregion
        #region Override WndProc and OnHandleCreate
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_DrumController.RegisterHandle(Handle);
        }
        protected override void WndProc(ref Message m)
        {
            m_DrumController.ParseMessages(ref m);
            base.WndProc(ref m);	// pass message on to base form
        }
        #endregion
        #region Velocity Meters
        public void UpdateVelocityPb(DrumPad pad, byte hitVelocity)
        {
            if (InvokeRequired)
            {
                Invoke(new Void_DrumPadByteDelegate(UpdateVelocityPb), new object[] { pad, hitVelocity });
            }
            else
            {
                referenceVelocity[(int)pad].Value = hitVelocity;
            }
        }
        private void LowerValues(object sender, EventArgs e)
        {
            for (int i = 0; i < referenceVelocity.GetLength(0); i++)
            {
                _lowerValues = referenceVelocity[i].Value - 2;
                if (_lowerValues > 0)
                {
                    if (_lowerValues > 110)
                    {
                        referenceVelocity[i].ForeColor = Color.Red;
                    }
                    if (_lowerValues > 63)
                    {
                        referenceVelocity[i].ForeColor = Color.FromArgb((_lowerValues) * 2, 255- (_lowerValues - 64) * 4, 0);
                    }
                    else
                    {
                        referenceVelocity[i].ForeColor = Color.FromArgb((_lowerValues) * 2, (_lowerValues) * 4, 255 - _lowerValues * 4);
                    }
                    referenceVelocity[i].Value = _lowerValues;
                }
                else
                    referenceVelocity[i].Value = 0;
            }
        }
        #endregion
    }
}

