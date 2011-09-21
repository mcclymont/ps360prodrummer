using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace _PS360Drum
{
    class ProDrumRawToGui : IRawToGui
    {
        public GuiDrumPad TranslatePad(byte raw)
        {
            switch (raw)
            {
                case (byte)ProDrumController.DrumPad.RedTom: return GuiDrumPad.RedTom;
                case (byte)ProDrumController.DrumPad.YellowTom: return GuiDrumPad.YellowTom;
                case (byte)ProDrumController.DrumPad.BlueTom: return GuiDrumPad.BlueTom;
                case (byte)ProDrumController.DrumPad.GreenTom: return GuiDrumPad.GreenTom;
                case (byte)ProDrumController.DrumPad.YellowCymbal: return GuiDrumPad.YellowCymbal;
                case (byte)ProDrumController.DrumPad.BlueCymbal: return GuiDrumPad.BlueCymbal;
                case (byte)ProDrumController.DrumPad.GreenCymbal: return GuiDrumPad.GreenCymbal;
            }
            Debug.Assert(false, "");
            return GuiDrumPad.BlueTom;
        }
        public GuiDrumButton TranslateButton(byte raw)
        {
            switch (raw)
            {
                case (byte)ProDrumController.DrumButton.BigButton: return GuiDrumButton.BigButton;
                case (byte)ProDrumController.DrumButton.Select: return GuiDrumButton.Select;
                case (byte)ProDrumController.DrumButton.Start: return GuiDrumButton.Start;
                case (byte)ProDrumController.DrumButton.Triangle: return GuiDrumButton.Triangle;
                case (byte)ProDrumController.DrumButton.X: return GuiDrumButton.X;
                case (byte)ProDrumController.DrumButton.Circle: return GuiDrumButton.Circle;
                case (byte)ProDrumController.DrumButton.Rectangle: return GuiDrumButton.Rectangle;
            }
            Debug.Assert(false, "");
            return GuiDrumButton.X;
        }       
        public GuiDrumDPad TranslateDPad(byte raw)
        {
            switch (raw)
            {
                case (byte)ProDrumController.DrumDPad.Up: return GuiDrumDPad.Down;
                case (byte)ProDrumController.DrumDPad.LeftUp: return GuiDrumDPad.LeftUp;
                case (byte)ProDrumController.DrumDPad.Left: return GuiDrumDPad.Left;
                case (byte)ProDrumController.DrumDPad.LeftDown: return GuiDrumDPad.LeftDown;
                case (byte)ProDrumController.DrumDPad.Down: return GuiDrumDPad.Down;
                case (byte)ProDrumController.DrumDPad.RightDown: return GuiDrumDPad.RightDown;
                case (byte)ProDrumController.DrumDPad.Right: return GuiDrumDPad.Right;
                case (byte)ProDrumController.DrumDPad.RightUp: return GuiDrumDPad.RightUp;
            }
            Debug.Assert(false, "");
            return GuiDrumDPad.Down;
        }
    }
}
