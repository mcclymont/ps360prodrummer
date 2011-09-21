using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _PS360Drum
{
    public delegate void ButtonDelegate(GuiDrumButton button);
    public delegate void DPadDelegate(GuiDrumDPad dpad);
    interface IDrumController
    {
        //public int NumPads { get; }
        //public int NumButtonStates { get; }

        //public delegate void NoteHitDelegate(DrumPad pad, byte velocity);z

        event ButtonDelegate ButtonPressedEvent;
        event ButtonDelegate ButtonReleasedEvent;
        event ButtonDelegate ButtonDownEvent;
        event DPadDelegate DPadStateChanged;

        void RegisterHandle(IntPtr handle);
        void ParseMessages(ref Message m);
    }
}
