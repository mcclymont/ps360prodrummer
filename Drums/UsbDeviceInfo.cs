using System;
using System.Collections.Generic;
using System.Text;

namespace _PS360Drum
{
    class UsbDeviceInfo
    {
        public string DeviceID { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
