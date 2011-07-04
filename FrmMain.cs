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
    public partial class FrmMain : Form
    {
        Object[] allowedBoostValues = new Object[6] { 10, 20, 30, 40, 50, 60 };

        NumericUpDown[] referenceMidiNotes = new NumericUpDown[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + 4];
        Button[] referenceButtons = new Button[ProDrumController.NUM_PADS];
        ProgressBar[] referenceVelocity = new ProgressBar[ProDrumController.NUM_PADS];
        CheckBox[] referenceCheckBox = new CheckBox[ProDrumController.NUM_PADS + ProDrumController.NUM_BUTTON_STATES + 4];
        ComboBox[] referenceBoostValues = new ComboBox[ProDrumController.NUM_PADS];
        const int BUTTON_OFFSET = ProDrumController.NUM_PADS;
        const int DPAD_OFFSET = BUTTON_OFFSET + ProDrumController.NUM_BUTTON_STATES;

        Int32 _lowerValues;
        String defaultFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PS360Drum.p3s");

        Random m_Random = new Random();

        public MidiSender MidiSender { get; private set; }
        private ProDrumController m_DrumController;
        private Serializer m_Serializer;

        private delegate byte Byte_DrumPadDelegate(DrumPad pad);
        private delegate void Void_DrumPadByteDelegate(DrumPad pad, byte velocity);

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
            referenceMidiNotes[(int)DrumPad.RedTom] = nupRed;
            referenceMidiNotes[(int)DrumPad.YellowTom] = nupYellow;
            referenceMidiNotes[(int)DrumPad.BlueTom] = nupBlue;
            referenceMidiNotes[(int)DrumPad.GreenTom] = nupGreen;
            referenceMidiNotes[(int)DrumPad.YellowCymbal] = nupCymYellow;
            referenceMidiNotes[(int)DrumPad.BlueCymbal] = nupCymBlue;
            referenceMidiNotes[(int)DrumPad.GreenCymbal] = nupCymGreen;
            referenceMidiNotes[(int)DrumPad.Pedal] = nupPedal;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.BigButton] = nupBigButton;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.Circle] = nupCircle;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.Rectangle] = nupRectangle;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.Select] = nupSelect;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.Start] = nupStart;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.Triangle] = nupTriangle;
            referenceMidiNotes[BUTTON_OFFSET + (int)DrumButton.X] = nupX;
            referenceMidiNotes[DPAD_OFFSET + (int)DrumDPad.Left/2] = nupDPadLeft;
            referenceMidiNotes[DPAD_OFFSET + (int)DrumDPad.Right/2] = nupDPadRight;
            referenceMidiNotes[DPAD_OFFSET + (int)DrumDPad.Up/2] = nupDPadUp;
            referenceMidiNotes[DPAD_OFFSET + (int)DrumDPad.Down/2] = nupDPadDown;


            referenceButtons[(int)DrumPad.RedTom] = btnRed;
            btnRed.Tag = DrumPad.RedTom;
            referenceButtons[(int)DrumPad.YellowTom] = btnYellow;
            btnYellow.Tag = DrumPad.YellowTom;
            referenceButtons[(int)DrumPad.BlueTom] = btnBlue;
            btnBlue.Tag = DrumPad.BlueTom;
            referenceButtons[(int)DrumPad.GreenTom] = btnGreen;
            btnGreen.Tag = DrumPad.GreenTom;
            referenceButtons[(int)DrumPad.YellowCymbal] = btnCymYellow;
            btnCymYellow.Tag = DrumPad.YellowCymbal;
            referenceButtons[(int)DrumPad.BlueCymbal] = btnCymBlue;
            btnCymBlue.Tag = DrumPad.BlueCymbal;
            referenceButtons[(int)DrumPad.GreenCymbal] = btnCymGreen;
            btnCymGreen.Tag = DrumPad.GreenCymbal;
            referenceButtons[(int)DrumPad.Pedal] = btnPedal;
            btnPedal.Tag = DrumPad.Pedal;

            referenceVelocity[(int)DrumPad.RedTom] = pbRed;
            referenceVelocity[(int)DrumPad.YellowTom] = pbYellow;
            referenceVelocity[(int)DrumPad.BlueTom] = pbBlue;
            referenceVelocity[(int)DrumPad.GreenTom] = pbGreen;
            referenceVelocity[(int)DrumPad.YellowCymbal] = pbCymYellow;
            referenceVelocity[(int)DrumPad.BlueCymbal] = pbCymBlue;
            referenceVelocity[(int)DrumPad.GreenCymbal] = pbCymGreen;
            referenceVelocity[(int)DrumPad.Pedal] = pbPedal;

            referenceCheckBox[(int)DrumPad.RedTom] = chkRed;
            referenceCheckBox[(int)DrumPad.YellowTom] = chkYellow;
            referenceCheckBox[(int)DrumPad.BlueTom] = chkBlue;
            referenceCheckBox[(int)DrumPad.GreenTom] = chkGreen;
            referenceCheckBox[(int)DrumPad.YellowCymbal] = chkCymYellow;
            referenceCheckBox[(int)DrumPad.BlueCymbal] = chkCymBlue;
            referenceCheckBox[(int)DrumPad.GreenCymbal] = chkCymGreen;
            referenceCheckBox[(int)DrumPad.Pedal] = chkPedal;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.BigButton] = chkBigButton;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.Circle] = chkCircle;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.Rectangle] = chkRectangle;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.Select] = chkSelect;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.Start] = chkStart;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.Triangle] = chkTriangle;
            referenceCheckBox[BUTTON_OFFSET + (int)DrumButton.X] = chkX;
            referenceCheckBox[DPAD_OFFSET + (int)DrumDPad.Left/2] = chkDPadLeft;
            referenceCheckBox[DPAD_OFFSET + (int)DrumDPad.Right/2] = chkDPadRight;
            referenceCheckBox[DPAD_OFFSET + (int)DrumDPad.Up/2] = chkDPadUp;
            referenceCheckBox[DPAD_OFFSET + (int)DrumDPad.Down/2] = chkDPadDown;

            referenceBoostValues[(int)DrumPad.RedTom] = ddlRed;
            referenceBoostValues[(int)DrumPad.YellowTom] = ddlYellow;
            referenceBoostValues[(int)DrumPad.BlueTom] = ddlBlue;
            referenceBoostValues[(int)DrumPad.GreenTom] = ddlGreen;
            referenceBoostValues[(int)DrumPad.YellowCymbal] = ddlCymYellow;
            referenceBoostValues[(int)DrumPad.BlueCymbal] = ddlCymBlue;
            referenceBoostValues[(int)DrumPad.GreenCymbal] = ddlCymGreen;
            referenceBoostValues[(int)DrumPad.Pedal] = ddlPedal;
            #endregion

            foreach (ComboBox currentDropDown in referenceBoostValues)
            {
                currentDropDown.Items.AddRange(allowedBoostValues);
                currentDropDown.SelectedIndex = 0;
            }

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
            Button b = sender as Button;
            byte vel = (byte)m_Random.Next(0, 255);
            MidiSender.TriggerNote((DrumPad)b.Tag, vel);
        }
        private void DrumButtonPressed(DrumButton b)
        {
            if (InvokeRequired)
            {
                Invoke(new ProDrumController.ButtonDelegate(DrumButtonPressed), new object[] { b });
            }
            else
            {
                referenceCheckBox[BUTTON_OFFSET + (int)b].Checked = true;
                MidiSender.SendNoteOn((byte)referenceMidiNotes[BUTTON_OFFSET + (int)b].Value, 127);
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
                referenceCheckBox[BUTTON_OFFSET + (int)b].Checked = false;
                MidiSender.SendNoteOff((byte)referenceMidiNotes[BUTTON_OFFSET + (int)b].Value);
            }
        }
        private void DPadOn(DrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");

            if (referenceCheckBox[DPAD_OFFSET + (int)dp / 2].Checked == false)
            {
                referenceCheckBox[DPAD_OFFSET + (int)dp / 2].Checked = true;
                MidiSender.SendNoteOff((byte)referenceMidiNotes[DPAD_OFFSET + (int)dp / 2].Value);
            }
        }
        private void DPadOn(DrumDPad dp, DrumDPad dp2)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            Debug.Assert((byte)dp2 % 2 == 0, "only left, up, right, down is allowed here");

            if (referenceCheckBox[DPAD_OFFSET + (int)dp / 2].Checked == false)
            {
                referenceCheckBox[DPAD_OFFSET + (int)dp / 2].Checked = true;
                MidiSender.SendNoteOn((byte)referenceMidiNotes[DPAD_OFFSET + (int)dp / 2].Value, 127);
            }
            if (referenceCheckBox[DPAD_OFFSET + (int)dp2 / 2].Checked == false)
            {
                referenceCheckBox[DPAD_OFFSET + (int)dp2 / 2].Checked = true;
                MidiSender.SendNoteOn((byte)referenceMidiNotes[DPAD_OFFSET + (int)dp / 2].Value, 127);
            }
        }
        private void DPadOff(DrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");

            if (referenceCheckBox[DPAD_OFFSET + (int)dp / 2].Checked == true)
            {
                referenceCheckBox[DPAD_OFFSET + (int)dp / 2].Checked = false;
                MidiSender.SendNoteOff((byte)referenceMidiNotes[DPAD_OFFSET + (int)dp / 2].Value);
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
        public byte GetMidiNote(DrumPad pad)
        {
            return (byte)referenceMidiNotes[(int)pad].Value;
        }
        public byte GetBoost(DrumPad pad)
        {
            if (InvokeRequired)
            {
                return (byte)Invoke(new Byte_DrumPadDelegate(GetBoost), new object[] { pad });
            }
            else
            {
                return byte.Parse(referenceBoostValues[(int)pad].SelectedItem.ToString());
            }
        }
        public bool GetBoostEnabled(DrumPad pad)
        {
            return referenceCheckBox[(int)pad].Checked;
        }
        public string GetMidiOutDeviceName()
        {
            return ddlMIDIDevice.SelectedValue as string;
        }
        public byte GetMidiChannel()
        {
            return (byte)nupMIDIChannel.Value;
        }

        public void SetMidiNote(DrumPad pad, byte note)
        {
            referenceMidiNotes[(int)pad].Value = note;
        }
        public void SetBoost(DrumPad pad, bool enabled, byte value)
        {
            referenceCheckBox[(int)pad].Checked = enabled;
            SetSelectIndex(ref referenceBoostValues[(int)pad], value);
        }
        private void SetSelectIndex(ref ComboBox currentCombobox, Object value)
        {
            int index = 0;
            for (int i = 0; i < currentCombobox.Items.Count; i++)
            {
                if (currentCombobox.Items[i].ToString() == value.ToString())
                {
                    index = i;
                }
            }
            currentCombobox.SelectedIndex = index;
        }
        public void SetMidiOutDeviceName(string deviceName)
        {
            SetSelectIndex(ref ddlMIDIDevice, deviceName);
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

