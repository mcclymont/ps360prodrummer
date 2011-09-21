using System;
using System.Windows.Forms;
using UsbLibrary;
using System.Diagnostics; // http://www.codeproject.com/KB/cs/USB_HID.aspx 

namespace _PS360Drum
{
    public class GHWTDrumController : IDrumController
    {
        #region enums
        public enum DrumPad : byte
        {
            RedTom = 0,
            BlueTom = 1,
            GreenTom = 2,
            YellowCymbal = 3,
            OrangeCymbal = 4,
            Pedal1 = 5
        };
        public enum DrumButton : byte
        {
            A = 0,
            B,
            Y, 
            X,
            Start,
            Back,
            BigButton
        };
        public enum DPadValue : byte
        {
            Up = 5,
            RightUp = 9,
            Right = 13,
            RightDown = 17,
            Down = 21,
            LeftDown = 25,
            Left = 29,
            LeftUp = 33,
            None = 1
        };
        private enum ButtonValue : byte
        {
            A = 1 << 0,
            B = 1 << 1,
            Y = 1 << 3, 
            X = 1 << 2,
            Start = 1 << 7,
            Back = 1 << 6,
            //BigButton = 1 << 5
        };
        private enum PadValue : byte
        {
            Red = 1 << 1, 
            Blue = 1 << 2, 
            Green = 1 << 0, 
            Yellow = 1 << 3, 
            Orange = 1 << 5,
            Pedal = 1 << 4
        };
        #endregion

        public const int NUM_PADS = 6;
        public const int NUM_BUTTON_STATES = 7;

        public delegate void NoteHitDelegate(GuiDrumPad pad, byte velocity);

        public event ButtonDelegate ButtonPressedEvent;
        public event ButtonDelegate ButtonReleasedEvent;
        public event ButtonDelegate ButtonDownEvent;
        public event DPadDelegate DPadStateChanged;

        private bool[] m_ButtonState = new bool[NUM_BUTTON_STATES];
        private DPadValue m_DPadState = DPadValue.None;

        private HitFilter m_HitFilter;
        private GHWTRawToGui m_GuiTranslater = new GHWTRawToGui();

        private UsbHidPort m_UsbDrum;

        private Timer m_CheckForDrumTimer;
            
        #region Constructor
        public GHWTDrumController(FrmMain main)
        {
            m_UsbDrum = new UsbHidPort();
            m_UsbDrum.ProductId = 1817;
            m_UsbDrum.VendorId = 1118;
            m_UsbDrum.OnSpecifiedDeviceArrived += new System.EventHandler(UsbOnSpecifiedDeviceArrived);
            m_UsbDrum.OnSpecifiedDeviceRemoved += new System.EventHandler(UsbOnSpecifiedDeviceRemoved);
            m_UsbDrum.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(UsbOnDataRecieved);

            m_HitFilter = new HitFilter(main, 6, m_GuiTranslater);
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
            Debug.Assert(args.data.GetLength(0) == 15, "Length detected != 15");

            HandleDPad(args.data);
            
            if (HandleButtons(args.data) == false)
            {
                if (args.data[11] != 0)
                {
                    HandleDrumPads(args.data);
                }
            }
        }
        private void HandleDPad(byte[] data)
        {
            DPadValue dpad = (DPadValue)data[12];
            if (m_DPadState != dpad)
            {
                if (DPadStateChanged != null)
                    DPadStateChanged(m_GuiTranslater.TranslateDPad((byte)dpad));
                m_DPadState = dpad;
            }
        }
        private void HandleDrumPads(byte[] data)
        {
            if ((data[11] & (byte)PadValue.Red) != 0)
            {
                m_HitFilter.TriggerNote((byte)DrumPad.RedTom, (byte)((127 - data[4])*2));
            }
            if ((data[11] & (byte)PadValue.Blue) != 0)
            {
                m_HitFilter.TriggerNote((byte)DrumPad.BlueTom, (byte)((127 - (255 - data[6]))*2));
            }
            if ((data[11] & (byte)PadValue.Green) != 0)
            {
                m_HitFilter.TriggerNote((byte)DrumPad.GreenTom, (byte)((255 - data[3])*2));
            }
            if ((data[11] & (byte)PadValue.Yellow) != 0)
            {
                m_HitFilter.TriggerNote((byte)DrumPad.YellowCymbal, (byte)((data[5])*2));
            }
            if ((data[11] & (byte)PadValue.Orange) != 0)
            {
                m_HitFilter.TriggerNote((byte)DrumPad.OrangeCymbal, (byte)((255 - data[7])*2));
            }
            if ((data[11] & (byte)PadValue.Pedal) != 0)
            {
                m_HitFilter.TriggerNote((byte)DrumPad.Pedal1, (byte)((127 - data[8])*2));
            }
        }
        private bool HandleButtons(byte[] data)
        {
            bool[] newState = new bool[NUM_BUTTON_STATES];
            if (data[11] != 0) //color hit
            {
                if ((data[11] & (byte)ButtonValue.A) != 0 && data[3] == 255)
                    newState[(byte)DrumButton.A] = true;
                if ((data[11] & (byte)ButtonValue.B) != 0 && data[4] == 127)
                    newState[(byte)DrumButton.B] = true;
                if ((data[11] & (byte)ButtonValue.Y) != 0 && data[5] == 0)
                    newState[(byte)DrumButton.Y] = true;
                if ((data[11] & (byte)ButtonValue.X) != 0 && data[6] == 128)
                    newState[(byte)DrumButton.X] = true;
                if ((data[11] & (byte)ButtonValue.Start) != 0)
                    newState[(byte)DrumButton.Start] = true;
                //if ((data[11] & (byte)ButtonValue.BigButton) != 0 && data[3] == 255)
                //    newState[(byte)DrumButton.BigButton] = true;
                if ((data[11] & (byte)ButtonValue.Back) != 0)
                    newState[(byte)DrumButton.Back] = true;
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
            return false;
        }
        private void UsbOnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            m_CheckForDrumTimer.Stop();
            if (sender == m_UsbDrum)
                MessageBox.Show("The GHWT drums were found!");
            else
                Debug.Assert(false, "Unkown device found");
        }
        private void UsbOnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            m_CheckForDrumTimer.Start();
            if (sender == m_UsbDrum)
                MessageBox.Show("The GHWT drums were removed!");
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
