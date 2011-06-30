using System;
using System.Windows.Forms;
using UsbLibrary;
using System.Diagnostics; // http://www.codeproject.com/KB/cs/USB_HID.aspx 

namespace _PS360Drum
{
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
        Button = 0,
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
    public class ProDrumController
    {
        public const int NUM_PADS = 8;

        public enum DPad : byte
        {
            Left = 6, 
            Up = 0, 
            Right = 2, 
            Down = 4
        }
        public enum Button
        {
            Select, 
            Start,
            Triangle, 
            Rectangle, 
            Circle, 
            X
        }

        public delegate void NoteHitDelegate(DrumPad pad, byte velocity);
        public delegate void ButtonDelegate(Button button);

        public event NoteHitDelegate NotHitEvent;
        public event ButtonDelegate ButtonPressedEvent;
        public event ButtonDelegate ButtonReleasedEvent;
        public event ButtonDelegate ButtonDownEvent;

        private bool[] m_ButtonState;
        private bool[] m_DPadState;

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

                if (args.data[1] > 0)
                {
                    m_HitFilter.TriggerNotes(args.data[1], args.data[2], args.data[3], args.data, 12);
                }
                else
                {
                    if (args.data[3] != 8)
                    {

                    }
                }
            }
            else
            {
                Debug.Assert(false, "Length detected != 28");
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
