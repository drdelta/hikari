using System;
using System.Windows;
using System.Collections;
using TrayBrightness.Models.Display;
using TrayBrightness.Win32;
using MahApps.Metro.Controls;
using System.Windows.Forms;
using Hardcodet.Wpf.TaskbarNotification;

namespace TrayBrightness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// API code:
    /// https://code.google.com/p/monitorprofiler/source/browse/
    /// mahapp.metro style
    /// http://mahapps.com/guides/quick-start.html
    /// hardcodet notifyicon for wpf
    /// http://www.codeproject.com/Articles/36468/WPF-NotifyIcon
    /// </summary>

    public partial class MainWindow : MetroWindow
    {
        private readonly MonitorCollection _monitorCollection = new MonitorCollection();
        // Choose first monitor in array
        public int currentMonitor = 0; ////////!!!!!!!!!!!!!!!!!!!!!!!!!
        //public static string[] monCollection;
        public static ArrayList monCollection = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            //this.WindowState = WindowState.Normal;
            //this.Show();

            /// Enumerate Displays
            var MyInfoEnumProc = new NativeMethods.MonitorEnumDelegate(MonitorEnum);
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MyInfoEnumProc, IntPtr.Zero);
            ReadInfo(_monitorCollection[currentMonitor]);
            foreach (Monitor monitor in _monitorCollection)
            {
                monCollection.Add(monitor.Name);
            }
            
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }
        /*
                private TaskbarIcon tb;

                private void InitApplication()
                {
                    //initialize NotifyIcon
                    tb = (TaskbarIcon)FindResource("MyNotifyIcon");
                }

                private void MyNotifyIcon_MouseClick(object sender, MouseEventArgs e)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (WindowState == WindowState.Minimized)
                        {
                            this.WindowState = WindowState.Normal;
                            this.Show();
                        }
                        else if (WindowState == WindowState.Normal)
                        {
                            this.Hide();
                            this.WindowState = WindowState.Minimized;
                        }
                    }
                }
        */



        /////// TODO //////////
        // COLOR change to aero/ metro
        // READ sensor data for AUTO adjustment
        // create CONFIG window
        //// choose monitor
        //// calibration between monitors
        //// light sensor settings

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            this.Hide();
            base.OnStateChanged(e);
        }

        private void ReadInfo(Monitor m) // Read info about current monitor
        {
            // Set Name 
            monName.Text = m.Name;
            // Set slider
            slValue.IsEnabled = m.Brightness.Supported;
            slValue.Maximum = m.Brightness.Max;
            slValue.Value = m.Brightness.Current;
        }

       private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            uint i = (uint)e.NewValue;
            NativeMethods.SetMonitorBrightness(_monitorCollection[currentMonitor].HPhysicalMonitor, i);
        }

        private bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref System.Drawing.Rectangle lprcMonitor, IntPtr dwData)
        {
            _monitorCollection.Add(hMonitor);
            return true;
        }
    }
}
