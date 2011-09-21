using System;
using System.Collections.Generic;
using System.Text;

namespace _PS360Drum
{
    interface IRawToGui
    {
        GuiDrumPad TranslatePad(byte raw);
        GuiDrumButton TranslateButton(byte raw);
        GuiDrumDPad TranslateDPad(byte raw);
        //static GuiDrumDPad Translate(byte raw);
    }
}
