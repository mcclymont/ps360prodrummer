using System;
using System.Windows.Forms;
using UsbLibrary;
using System.Diagnostics; // http://www.codeproject.com/KB/cs/USB_HID.aspx 

namespace _PS360Drum
{
    enum CymbalType : byte
    {
        Yellow = 0,
        Blue = 1 << 2,
        Green = 1 << 3
    }
    public enum PadColor : byte
    {
        Blue = 1 << 0,
        Green = 1 << 1,
        Red = 1 << 2,
        Yellow = 1 << 3,
        Pedal = 1 << 4
    }
    public enum PadType : byte
    {
        Tom = 1 << 2,
        Cymbal = 1 << 3
    }
    public enum DrumPad
    {
        RedTom = 0,
        YellowTom = 1,
        BlueTom = 2,
        GreenTom = 3,
        YellowCymbal = 4,
        BlueCymbal = 5,
        GreenCymbal = 6,
        Pedal = 7
    };
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
    public class ProDrumController
    {
        public const int NUM_PADS = 8;
        public const int NUM_BUTTON_STATES = 7;

        public delegate void NoteHitDelegate(DrumPad pad, byte velocity);
        public delegate void ButtonDelegate(DrumButton button);
        public delegate void DPadDelegate(DrumDPad dpad);

        public event ButtonDelegate ButtonPressedEvent;
        public event ButtonDelegate ButtonReleasedEvent;
        public event ButtonDelegate ButtonDownEvent;
        public event DPadDelegate DPadStateChanged;

        private bool[] m_ButtonState = new bool[NUM_BUTTON_STATES];
        private DrumDPad m_DPadState = DrumDPad.None;


        private HitFilter m_HitFilter;

        private UsbHidPort m_UsbDrum;

        private Timer m_CheckForDrumTimer;
            
        #region Constructor
        public ProDrumController(FrmMain main)
        {
            m_UsbDrum = new UsbHidPort();
            m_UsbDrum.ProductId = 528;
            m_UsbDrum.VendorId = 4794;
            m_UsbDrum.OnSpecifiedDeviceArrived += new System.EventHandler(UsbOnSpecifiedDeviceArrived);
            m_UsbDrum.OnSpecifiedDeviceRemoved += new System.EventHandler(UsbOnSpecifiedDeviceRemoved);
            m_UsbDrum.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(UsbOnDataRecieved);

            m_HitFilter = new HitFilter(main);
            m_CheckForDrumTimer = new Timer();
            m_CheckForDrumTimer.Interval = 1000;
            m_CheckForDrumTimer.Tick += CheckForDrumsTick;
            m_CheckForDrumTimer.Start();
        }
        #endregion

        #region USB
        public void RegisterHandle(IntPtr handle)
        {
            m_UsbDrum.RegisterHandle(handle);
        }
        public void ParseMessages(ref Message m)
        {
            m_UsbDrum.ParseMessages(ref m);
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
                    if (args.data[2] != 0 || args.data[1] == (byte)PadColor.Pedal)
                        m_HitFilter.TriggerNotes(args.data[1], args.data[2], args.data[3], args.data, 12);
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
                        DPadStateChanged(dpad);
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
            for (int i = 0; i < NUM_BUTTON_STATES; ++i)
            {
                if (newState[i])
                    if (ButtonDownEvent != null)
                        ButtonDownEvent((DrumButton)i);
                if (m_ButtonState[i] != newState[i])
                {
                    if (newState[i] == false)
                    {
                        if (ButtonReleasedEvent != null)
                            ButtonReleasedEvent((DrumButton)i);
                    }
                    else
                    {
                        if (ButtonPressedEvent != null)
                            ButtonPressedEvent((DrumButton)i);
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
