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
using System.Management;

namespace _PS360Drum
{
    public delegate byte Byte_DrumPadDelegate(GuiDrumPad pad);
    public delegate bool Bool_DrumPadDelegate(GuiDrumPad pad);
    public delegate void Void_DrumPadByteDelegate(GuiDrumPad pad, byte velocity);
    public partial class FrmMain : Form
    {
        ProgressBar[] referenceVelocity = new ProgressBar[ProDrumController.NUM_PADS];
        public GuiLinker GuiLinker { get; private set; }

        Int32 _lowerValues;
        String defaultFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PS360Drum.p3s");

        Random m_Random = new Random();

        public MidiSender MidiSender { get; private set; }
        private IDrumController m_DrumController;
        private Serialize.Serializer m_Serializer;
        public MultiNoteGui MultiNoteGui { get; private set; }

        private GuiDrumDPad m_PrevDPadState = GuiDrumDPad.None;
        
        public FrmMain()
        {    
            InitializeComponent();

            m_Serializer = new Serialize.Serializer(this);

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

            GuiLinker.AddDrum(GuiDrumPad.RedTom, nupRedTom, nupAdvNoteRedTom, chkBoostRedTom, chkAdvBoostRedTom, ddlRedTom, ddlAdvBoostRedTom);
            GuiLinker.AddDrum(GuiDrumPad.YellowTom, nupYellowTom, nupAdvNoteYellowTom, chkBoostYellowTom, chkAdvBoostYellowTom, ddlYellowTom, ddlAdvBoostYellowTom);
            GuiLinker.AddDrum(GuiDrumPad.YellowCymbal, nupYellowCymbal, nupAdvNoteYellowCymbal, chkBoostYellowCymbal, chkAdvBoostYellowCymbal, ddlYellowCymbal, ddlAdvBoostYellowCymbal);
            GuiLinker.AddDrum(GuiDrumPad.BlueTom, nupBlueTom, nupAdvNoteBlueTom, chkBoostBlueTom, chkAdvBoostBlueTom, ddlBlueTom, ddlAdvBoostBlueTom);
            GuiLinker.AddDrum(GuiDrumPad.BlueCymbal, nupBlueCymbal, nupAdvNoteBlueCymbal, chkBoostBlueCymbal, chkAdvBoostBlueCymbal, ddlBlueCymbal, ddlAdvBoostBlueCymbal);
            GuiLinker.AddDrum(GuiDrumPad.GreenTom, nupGreenTom, nupAdvNoteGreenTom, chkBoostGreenTom, chkAdvBoostGreenTom, ddlGreenTom, ddlAdvBoostGreenTom);
            GuiLinker.AddDrum(GuiDrumPad.GreenCymbal, nupGreenCymbal, nupAdvNoteGreenCymbal, chkBoostGreenCymbal, chkAdvBoostGreenCymbal, ddlGreenCymbal, ddlAdvBoostGreenCymbal);
            GuiLinker.AddDrum(GuiDrumPad.PedalLeft, nupPedalLeft, nupAdvNotePedalLeft, chkBoostPedalLeft, chkAdvBoostPedalLeft, ddlPedalLeft, ddlAdvBoostPedalLeft);
            GuiLinker.AddDrum(GuiDrumPad.PedalRight, nupPedalRight, nupAdvNotePedalRight, chkBoostPedalRight, chkAdvBoostPedalRight, ddlPedalRight, ddlAdvBoostPedalRight);

            GuiLinker.AddButton(GuiDrumButton.BigButton, nupBigButton, nupAdvNoteBigButton, nupAdvVelBigButton, ddlAdvSwitchBigButton, chkBigButton);
            GuiLinker.AddButton(GuiDrumButton.Circle, nupCircle, nupAdvNoteCircle, nupAdvVelCircle, ddlAdvSwitchCircle, chkCircle);
            GuiLinker.AddButton(GuiDrumButton.Rectangle, nupRectangle, nupAdvNoteRectangle, nupAdvVelRectangle, ddlAdvSwitchRectangle, chkRectangle);
            GuiLinker.AddButton(GuiDrumButton.Select, nupSelect, nupAdvNoteSelect, nupAdvVelSelect, ddlAdvSwitchSelect, chkSelect);
            GuiLinker.AddButton(GuiDrumButton.Start, nupStart, nupAdvNoteStart, nupAdvVelStart, ddlAdvSwitchStart, chkStart);
            GuiLinker.AddButton(GuiDrumButton.Triangle, nupTriangle, nupAdvNoteTriangle, nupAdvVelTriangle, ddlAdvSwitchTriangle, chkTriangle);
            GuiLinker.AddButton(GuiDrumButton.X, nupX, nupAdvNoteX, nupAdvVelX, ddlAdvSwitchX, chkX);

            GuiLinker.AddDPad(GuiDrumDPad.Up, nupDPadUp, nupAdvNoteDPadUp, nupAdvVelDPadUp, ddlAdvSwitchDPadUp, chkDPadUp);
            GuiLinker.AddDPad(GuiDrumDPad.Down, nupDPadDown, nupAdvNoteDPadDown, nupAdvVelDPadDown, ddlAdvSwitchDPadDown, chkDPadDown);
            GuiLinker.AddDPad(GuiDrumDPad.Right, nupDPadRight, nupAdvNoteDPadRight, nupAdvVelDPadRight, ddlAdvSwitchDPadRight, chkDPadRight);
            GuiLinker.AddDPad(GuiDrumDPad.Left, nupDPadLeft, nupAdvNoteDPadLeft, nupAdvVelDPadLeft, ddlAdvSwitchDPadLeft, chkDPadLeft);

            referenceVelocity[(int)GuiDrumPad.RedTom] = pbRedTom;
            referenceVelocity[(int)GuiDrumPad.YellowTom] = pbYellowTom;
            referenceVelocity[(int)GuiDrumPad.BlueTom] = pbBlueTom;
            referenceVelocity[(int)GuiDrumPad.GreenTom] = pbGreenTom;
            referenceVelocity[(int)GuiDrumPad.YellowCymbal] = pbYellowCymbal;
            referenceVelocity[(int)GuiDrumPad.BlueCymbal] = pbBlueCymbal;
            referenceVelocity[(int)GuiDrumPad.GreenCymbal] = pbGreenCymbal;
            referenceVelocity[(int)GuiDrumPad.PedalLeft] = pbPedalLeft;
            referenceVelocity[(int)GuiDrumPad.PedalRight] = pbPedalRight;
            #endregion
            #region MultiNoteGUi
            MultiNoteGui = new MultiNoteGui(ddlMNvelCheck, nupMNvelCheck, ddlMNnote, nupMNnoteTo,
                                              nupMNvelMult, nupMNvelAdd, btnMNAdd, btnMNRemove, lbMN);
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
            
            ddlController.Items.Add(ControllerType.Xbox360_GHWT_GH5_Drum);
            ddlController.Items.Add(ControllerType.Ps3_RockBandProDrum);
            ddlController.SelectedIndex = 0;
            FillUsbList();
        }

        private void ButtonPress(object sender, EventArgs e)
        {
            //Button b = sender as Button;
            //byte vel = (byte)m_Random.Next(0, 255);
            //MidiSender.TriggerNote((DrumPad)b.Tag, vel);
        }
        private void DrumButtonPressed(GuiDrumButton b)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonDelegate(DrumButtonPressed), new object[] { b });
            }
            else
            {
                GuiLinker.CheckboxButton(b, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(b), GuiLinker.GetButtonVelocity(b));
                if (GuiLinker.GetButtonSwitchType(b) != SwitchType.KeyboardLike)
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(b));
            }
        }
        private void DrumButtonReleased(GuiDrumButton b)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonDelegate(DrumButtonReleased), new object[] { b });
            }
            else
            {
                GuiLinker.CheckboxButton(b, false);
                if (GuiLinker.GetButtonSwitchType(b) == SwitchType.KeyboardLike)
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(b));
                else if (GuiLinker.GetButtonSwitchType(b) == SwitchType.OnOff)
                {
                    MidiSender.SendNoteOn(GuiLinker.GetMidiNote(b), GuiLinker.GetButtonVelocity(b));
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(b));
                }
            }
        }
        private void DPadOn(GuiDrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");

            if (GuiLinker.GetButtonChecked(dp) == false)
            {
                GuiLinker.CheckboxButton(dp, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(dp), GuiLinker.GetButtonVelocity(dp));
                if (GuiLinker.GetButtonSwitchType(dp) != SwitchType.KeyboardLike)
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
            }
        }
        private void DPadOn(GuiDrumDPad dp, GuiDrumDPad dp2)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            Debug.Assert((byte)dp2 % 2 == 0, "only left, up, right, down is allowed here");

            if (GuiLinker.GetButtonChecked(dp) == false)
            {
                GuiLinker.CheckboxButton(dp, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(dp), GuiLinker.GetButtonVelocity(dp));
                if (GuiLinker.GetButtonSwitchType(dp) != SwitchType.KeyboardLike)
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
            }
            if (GuiLinker.GetButtonChecked(dp2) == false)
            {
                GuiLinker.CheckboxButton(dp2, true);
                MidiSender.SendNoteOn(GuiLinker.GetMidiNote(dp2), GuiLinker.GetButtonVelocity(dp));
                if (GuiLinker.GetButtonSwitchType(dp) != SwitchType.KeyboardLike)
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
            }
        }
        private void DPadOff(GuiDrumDPad dp)
        {
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");
            Debug.Assert((byte)dp % 2 == 0, "only left, up, right, down is allowed here");

            if (GuiLinker.GetButtonChecked(dp) == true)
            {
                GuiLinker.CheckboxButton(dp, false);
                if (GuiLinker.GetButtonSwitchType(dp) == SwitchType.KeyboardLike)
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
                else if (GuiLinker.GetButtonSwitchType(dp) == SwitchType.OnOff)
                {
                    MidiSender.SendNoteOn(GuiLinker.GetMidiNote(dp), GuiLinker.GetButtonVelocity(dp));
                    MidiSender.SendNoteOff(GuiLinker.GetMidiNote(dp));
                }
            }
        }
        private void DrumDPadStateChanged(GuiDrumDPad dp)
        {
            if (m_PrevDPadState == dp)
                return;
            if (InvokeRequired)
            {
                Invoke(new DPadDelegate(DrumDPadStateChanged), new object[] { dp });
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

                if (dp != GuiDrumDPad.None)
                {
                    DPadOn((GuiDrumDPad)new1, (GuiDrumDPad)new2);
                }
                if (m_PrevDPadState != GuiDrumDPad.None)
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
                            DPadOff((GuiDrumDPad)prev1);
                        if (prev2 != new1 && prev2 != new2)
                            DPadOff((GuiDrumDPad)prev2);
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
                m_Serializer.Load(openFileDialog.FileName);
            }
        }
        private void SaveDefaultSettings(object sender, FormClosingEventArgs e)
        {
            m_Serializer.Save(defaultFilePath);
        }
        private void LoadDefaultSettings(object sender, EventArgs e)
        {
            m_Serializer.LoadDefaultSettings();
        }
        #endregion
        #region Override WndProc and OnHandleCreate
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (m_DrumController != null)
                m_DrumController.RegisterHandle(Handle);
        }
        protected override void WndProc(ref Message m)
        {
            if (m_DrumController != null)
                m_DrumController.ParseMessages(ref m);               
            base.WndProc(ref m);	// pass message on to base form
        }
        #endregion
        #region Velocity Meters
        public void UpdateVelocityPb(GuiDrumPad pad, byte hitVelocity)
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

        private void nupPedalRight_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnRefreshUsb_Click(object sender, EventArgs e)
        {
            FillUsbList();
        }

        private void FillUsbList()
        {
            List<UsbDeviceInfo> devices = new List<UsbDeviceInfo>();
            int defaultIndex = 0;

            ManagementObjectCollection devicesCollection;
            using (var searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity where DeviceID like 'USB%'"))
            {
                devicesCollection = searcher.Get();      
            }

            var i = 0;
            foreach (var device in devicesCollection)
            {
                var deviceDescription = (string)device.GetPropertyValue("Description");
                devices.Add(new UsbDeviceInfo()
                {
                    Description = deviceDescription,
                    DeviceID = (string)device.GetPropertyValue("DeviceID")
                });

                // Try to choose the correct USB device based on the controller type.
                if (  ControllerType.Xbox360_GHWT_GH5_Drum.Equals(ddlController.SelectedItem) &&
                      deviceDescription == "Xbox 360 Wireless Receiver for Windows") {
                    defaultIndex = i;
                }
                // TODO PS3 RockBand version of the above

                i += 1;
            }

            devicesCollection.Dispose();

            ddlUsbController.Items.Clear();
            ddlUsbController.Items.AddRange(devices.ToArray());

            ddlUsbController.SelectedIndex = defaultIndex;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            UsbDeviceInfo info = (UsbDeviceInfo)ddlUsbController.SelectedItem;
            string vid = info.DeviceID.Substring(info.DeviceID.IndexOf("VID_") + 4, 4);
            string pid = info.DeviceID.Substring(info.DeviceID.IndexOf("PID_") + 4, 4);
            
            int iVid = int.Parse(vid, System.Globalization.NumberStyles.AllowHexSpecifier);
            int iPid = int.Parse(pid, System.Globalization.NumberStyles.AllowHexSpecifier);

            if (m_DrumController != null)
            {
                m_DrumController.Dispose();

                m_DrumController.ButtonPressedEvent -= DrumButtonPressed;
                m_DrumController.ButtonReleasedEvent -= DrumButtonReleased;
                m_DrumController.DPadStateChanged -= DrumDPadStateChanged;
            }

            switch ((ControllerType)ddlController.SelectedItem)
            {
                case ControllerType.Xbox360_GHWT_GH5_Drum:
                    m_DrumController = new GHWTDrumController(this, iPid, iVid); break;
                case ControllerType.Ps3_RockBandProDrum:
                    m_DrumController = new ProDrumController(this, iPid, iVid); break;
            }

            m_DrumController.ButtonPressedEvent += DrumButtonPressed;
            m_DrumController.ButtonReleasedEvent += DrumButtonReleased;
            m_DrumController.DPadStateChanged += DrumDPadStateChanged;
        }
    }
}

