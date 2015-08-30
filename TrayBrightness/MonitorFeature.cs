using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrayBrightness.Models.Display
{
    public struct MonitorFeature
    {
        public bool Supported;
        public uint Min, Max, Current, Original;
    }
}
