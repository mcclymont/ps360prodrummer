using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace _PS360Drum
{
    class GHWTRawToGui : IRawToGui
    {
        public GuiDrumPad TranslatePad(byte raw)
        {
            switch (raw)
            {
                case (byte)GHWTDrumController.DrumPad.RedTom: return GuiDrumPad.RedTom;
                case (byte)GHWTDrumController.DrumPad.BlueTom: return GuiDrumPad.BlueTom;
                case (byte)GHWTDrumController.DrumPad.GreenTom: return GuiDrumPad.GreenTom;
                case (byte)GHWTDrumController.DrumPad.YellowCymbal: return GuiDrumPad.YellowCymbal;
                case (byte)GHWTDrumController.DrumPad.OrangeCymbal: return GuiDrumPad.GreenCymbal;
                case (byte)GHWTDrumController.DrumPad.Pedal1: return GuiDrumPad.PedalRight;
            }
            Debug.Assert(false, "Unknown Pad value");
            return GuiDrumPad.BlueTom;
        }
        public GuiDrumButton TranslateButton(byte raw)
        {
            switch (raw)
            {
                case (byte)GHWTDrumController.DrumButton.A: return GuiDrumButton.X;
                case (byte)GHWTDrumController.DrumButton.B: return GuiDrumButton.Circle;
                case (byte)GHWTDrumController.DrumButton.Y: return GuiDrumButton.Triangle;
                case (byte)GHWTDrumController.DrumButton.X: return GuiDrumButton.Rectangle;
                case (byte)GHWTDrumController.DrumButton.Start: return GuiDrumButton.Start;
                case (byte)GHWTDrumController.DrumButton.Back: return GuiDrumButton.Select;
            }
            Debug.Assert(false, "Unknown Button value");
            return GuiDrumButton.X;
        }
        public GuiDrumDPad TranslateDPad(byte raw)
        {
            switch (raw)
            {
                case (byte)GHWTDrumController.DPadValue.Up: return GuiDrumDPad.Up;
                case (byte)GHWTDrumController.DPadValue.LeftUp: return GuiDrumDPad.LeftUp;
                case (byte)GHWTDrumController.DPadValue.Left: return GuiDrumDPad.Left;
                case (byte)GHWTDrumController.DPadValue.LeftDown: return GuiDrumDPad.LeftDown;
                case (byte)GHWTDrumController.DPadValue.Down: return GuiDrumDPad.Down;
                case (byte)GHWTDrumController.DPadValue.RightDown: return GuiDrumDPad.RightDown;
                case (byte)GHWTDrumController.DPadValue.Right: return GuiDrumDPad.Right;
                case (byte)GHWTDrumController.DPadValue.RightUp: return GuiDrumDPad.RightUp;
                case (byte)GHWTDrumController.DPadValue.None: return GuiDrumDPad.None;
            }
            Debug.Assert(false, "Unknown Dpad value");
            return GuiDrumDPad.Down;
        }
    }
}
