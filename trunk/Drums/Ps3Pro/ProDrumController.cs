using System;
using System.Windows.Forms;
using UsbLibrary;
using System.Diagnostics; // http://www.codeproject.com/KB/cs/USB_HID.aspx 

namespace _PS360Drum
{  
    public class ProDrumController : IDrumController
    {
        #region enums
        private enum CymbalType : byte
        {
            Yellow = 0,
            Blue = 1 << 2,
            Green = 1 << 3
        }
        private enum PadColor : byte
        {
            Blue = 1 << 0,
            Green = 1 << 1,
            Red = 1 << 2,
            Yellow = 1 << 3
        }
        private enum PadType : byte
        {
            Tom = 1 << 2,
            Cymbal = 1 << 3
        }
        public enum DrumDPad : byte
        {
            Up = 0,
            RightUp = 1,
            Right = 2,
            RightDown = 3,
            Down = 4,
            LeftDown = 5,
            Left = 6,
            LeftUp = 7,
            None = 8
        }
        public enum DrumButton : byte
        {
            Select,
            Start,
            BigButton,
            Triangle,
            Rectangle,
            Circle,
            X
        }
        public enum DrumPad : byte
        {
            RedTom = 0,
            YellowTom = 1,
            BlueTom = 2,
            GreenTom = 3,
            YellowCymbal = 4,
            BlueCymbal = 5,
            GreenCymbal = 6,
            PedalLeft = 7,
            PedalRight = 8
        };
        #endregion

        public const int NUM_PADS = 9;
        public const int NUM_BUTTON_STATES = 7;

        public delegate void NoteHitDelegate(DrumPad pad, byte velocity);
        private byte m_MinVelocitySensitivity = 42;

        private bool[] m_ButtonState = new bool[NUM_BUTTON_STATES];
        private DrumDPad m_DPadState = DrumDPad.None;

        public event ButtonDelegate ButtonPressedEvent;
        public event ButtonDelegate ButtonReleasedEvent;
        public event ButtonDelegate ButtonDownEvent;
        public event DPadDelegate DPadStateChanged;

        private HitFilter m_HitFilter;
        private ProDrumRawToGui m_GuiTranslater = new ProDrumRawToGui();

        private UsbHidPort m_UsbDrum;

        private Timer m_CheckForDrumTimer;
            
        #region Constructor
        public ProDrumController(FrmMain main, int pid, int vid)
        {
            m_UsbDrum = new UsbHidPort();
            m_UsbDrum.ProductId = pid;
            m_UsbDrum.VendorId = vid;
            m_UsbDrum.OnSpecifiedDeviceArrived += new System.EventHandler(UsbOnSpecifiedDeviceArrived);
            m_UsbDrum.OnSpecifiedDeviceRemoved += new System.EventHandler(UsbOnSpecifiedDeviceRemoved);
            m_UsbDrum.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(UsbOnDataRecieved);

            m_HitFilter = new HitFilter(main, NUM_PADS, m_GuiTranslater);
            m_CheckForDrumTimer = new Timer();
            m_CheckForDrumTimer.Interval = 1000;
            m_CheckForDrumTimer.Tick += CheckForDrumsTick;
            m_CheckForDrumTimer.Start();
        }
        #endregion

        public void Dispose()
        {
            m_UsbDrum.OnSpecifiedDeviceArrived -= UsbOnSpecifiedDeviceArrived;
            m_UsbDrum.OnSpecifiedDeviceRemoved -= UsbOnSpecifiedDeviceRemoved;
            m_UsbDrum.OnDataRecieved -= UsbOnDataRecieved;
            m_UsbDrum.UnregisterHandle();
            m_UsbDrum.Dispose();
        }

        #region USB
        public void RegisterHandle(IntPtr handle)
        {
            m_UsbDrum.RegisterHandle(handle);
        }
        public void ParseMessages(ref Message m)
        {
            m_UsbDrum.ParseMessages(ref m);
        }
        private byte calcVelocity(byte velocity)
        {
            return (byte)(Math.Max(0, Math.Min(255, 255 - (velocity - m_MinVelocitySensitivity))));
        }
        public void TriggerNotes(byte color, byte type, byte flag, byte[] velocities, int velocityArrayOffset)
        {
            int isRed = color & ((byte)PadColor.Red);
            int isYellow = color & ((byte)PadColor.Yellow);
            int isBlue = color & ((byte)PadColor.Blue);
            int isGreen = color & ((byte)PadColor.Green);

            int isTom = type & ((byte)PadType.Tom);
            int isCymbal = type & ((byte)PadType.Cymbal);

            bool OneColor = ((isRed != 0 ? 1 : 0) + (isYellow != 0 ? 1 : 0) + (isBlue != 0 ? 1 : 0) + (isGreen != 0 ? 1 : 0)) == 1;

            if (isCymbal != 0)
            {
                if (flag == 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.YellowCymbal, calcVelocity(velocities[velocityArrayOffset + (OneColor ? 0 : 1)]));
                    if (OneColor == false)
                        isYellow = 0;
                }
                if ((flag & (byte)CymbalType.Blue) != 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.BlueCymbal, calcVelocity(velocities[velocityArrayOffset + (OneColor ? 3 : 1)]));
                    if (OneColor == false)
                        isBlue = 0;
                }
                if ((flag & (byte)CymbalType.Green) != 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.GreenCymbal, calcVelocity(velocities[velocityArrayOffset + (OneColor ? 2 : 1)]));
                    if (OneColor == false)
                        isGreen = 0;
                }
            }
            if (isTom != 0)
            {
                if (isRed != 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.RedTom, calcVelocity(velocities[velocityArrayOffset + 1]));
                }
                if (isYellow != 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.YellowTom, calcVelocity(velocities[velocityArrayOffset + 0]));
                }
                if (isBlue != 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.BlueTom, calcVelocity(velocities[velocityArrayOffset + 3]));
                }
                if (isGreen != 0)
                {
                    m_HitFilter.TriggerNote((byte)DrumPad.GreenTom, calcVelocity(velocities[velocityArrayOffset + 2]));
                }
            }
        }
        private void UsbOnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            // Gets byte with the info about which buttons/pads/pedals are down
            if (args.data.GetLength(0) == 28)
            {
                //byte[] test = new byte[28] { 0, 0, 0, 8, 127, 127, 127, 127, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 2, 0, 2, 0 };
                HandleDPad(args.data);
                HandleButtons(args.data);
                if (args.data[1] > 0)
                {
                    if (args.data[2] != 0)
                        TriggerNotes(args.data[1], args.data[2], args.data[3], args.data, 12);
                }
            }
            else
            {
                Debug.Assert(false, "Length detected != 28");
            }
        }
        private bool IsCymbal(byte[] data)
        {
            return ((data[2] & (byte)PadType.Cymbal) != 0);
        }
        private DrumDPad TranslateDPad(byte raw)
        {
            return (DrumDPad)raw;
        }
        private void HandleDPad(byte[] data)
        {
            if (IsCymbal(data) == false) //can't check dpad if cymbal is hit
            {
                DrumDPad dpad = TranslateDPad(data[3]);
                if (m_DPadState != dpad)
                {
                    if (DPadStateChanged != null)
                        DPadStateChanged(m_GuiTranslater.TranslateDPad((byte)dpad));
                    m_DPadState = dpad;
                }
            }
        }
        private void HandleButtons(byte[] data)
        {
            bool[] newState = new bool[NUM_BUTTON_STATES];
            if (data[2] == 0) //O, X, Rect, Triangle
            {
                if ((data[1] & 1) != 0)
                    newState[(byte)DrumButton.Rectangle] = true;
                if ((data[1] & 2) != 0)
                    newState[(byte)DrumButton.X] = true;
                if ((data[1] & 4) != 0)
                    newState[(byte)DrumButton.Circle] = true;
                if ((data[1] & 8) != 0)
                    newState[(byte)DrumButton.Triangle] = true;
            }
            else
            {
                if ((data[2] & 1) != 0) //select
                {
                    newState[(byte)DrumButton.Select] = true;
                }
                if ((data[2] & 2) != 0) //start
                {
                    newState[(byte)DrumButton.Start] = true;
                }
                if ((data[2] & 16) != 0) //big button
                {
                    newState[(byte)DrumButton.BigButton] = true;
                }
            }
            if ((data[1] & 16) != 0) //Right pedal
            {
                m_HitFilter.TriggerNote((byte)DrumPad.PedalRight, (byte)(255 - 80));
            }
            if ((data[1] & 32) != 0) //Left pedal
            {
                m_HitFilter.TriggerNote((byte)DrumPad.PedalLeft, (byte)(255 - 80));
            }

            for (byte i = 0; i < NUM_BUTTON_STATES; ++i)
            {
                if (newState[i])
                    if (ButtonDownEvent != null)
                        ButtonDownEvent(m_GuiTranslater.TranslateButton(i));
                if (m_ButtonState[i] != newState[i])
                {
                    if (newState[i] == false)
                    {
                        if (ButtonReleasedEvent != null)
                            ButtonReleasedEvent(m_GuiTranslater.TranslateButton(i));
                    }
                    else
                    {
                        if (ButtonPressedEvent != null)
                            ButtonPressedEvent(m_GuiTranslater.TranslateButton(i));
                    }
                    m_ButtonState[i] = newState[i];
                }
            }
        }
        private void UsbOnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            m_CheckForDrumTimer.Stop();
            if (sender == m_UsbDrum)
                MessageBox.Show("The Rockband Pro drums were found!");
            else
                Debug.Assert(false, "Unkown device found");
        }
        private void UsbOnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            m_CheckForDrumTimer.Start();
            if (sender == m_UsbDrum)
                MessageBox.Show("The Rockband Pro drums were removed!");
            else
                Debug.Assert(false, "Unkown device found");
        }
        private void CheckForDrumsTick(object sender, EventArgs e)
        {                
            m_UsbDrum.CheckDevicePresent();
        }
        #endregion


    }
}
