using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrayBrightness.Models.Display;
using TrayBrightness.Win32;

namespace TrayBrightness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MonitorCollection _monitorCollection = new MonitorCollection();
        //public uint initSliderValue = 40;
        public MainWindow()
        {
            InitializeComponent();
            /// 1. Enumerate Displays
            var MyInfoEnumProc = new NativeMethods.MonitorEnumDelegate(MonitorEnum);
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MyInfoEnumProc, IntPtr.Zero);
            /// 2. Check supported functions / check brightness support
            //TrayBrightness.Models.Display.Monitor();

        }

        /////// TODO //////////
        // GOTO right-bottom corner of the screen
        // COLOR change to aero/ metro

        // TRAY icon and CONTROLS
        //      Config
        //      Exit


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            uint i = (uint)e.NewValue;
            NativeMethods.SetMonitorBrightness(_monitorCollection[0].HPhysicalMonitor, i);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref System.Drawing.Rectangle lprcMonitor, IntPtr dwData)
        {
            _monitorCollection.Add(hMonitor);
            return true;
        }

    }
}
