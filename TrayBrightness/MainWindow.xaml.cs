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
using MahApps.Metro.Controls;

namespace TrayBrightness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// API code:
    /// https://code.google.com/p/monitorprofiler/source/browse/
    /// mahapp.metro style
    /// http://mahapps.com/guides/quick-start.html
    /// </summary>
    
    public partial class MainWindow : MetroWindow
    {
        private readonly MonitorCollection _monitorCollection = new MonitorCollection();
        // Choose first monitor in array
        int currentMonitor = 0; ////////!!!!!!!!!!!!!!!!!!!!!!!!!

        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            /// Enumerate Displays
            var MyInfoEnumProc = new NativeMethods.MonitorEnumDelegate(MonitorEnum);
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MyInfoEnumProc, IntPtr.Zero);
            ReadInfo(_monitorCollection[currentMonitor]);

            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            /// Create a tray icon & menu
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            System.Windows.Forms.ContextMenu cm = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem mi1 = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem mi2 = new System.Windows.Forms.MenuItem();
            ni.Icon = new System.Drawing.Icon("main.ico");
            ni.Visible = true;
            ni.Click +=
                delegate (object sender, EventArgs args)
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
                };
            ni.ContextMenu = cm;
            cm.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { mi2 });
            mi2.Index = 0;
            mi2.Text = "C&onfiguration";
            cm.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { mi1 });
            mi1.Index = 1;
            mi1.Text = "E&xit";
            mi1.Click +=
                delegate (object sender, EventArgs args)
                {
                    Application.Current.Shutdown();
                };

        }

        /////// TODO //////////
        // COLOR change to aero/ metro

        // TRAY icon and CONTROLS
        //      Config
        //      Exit

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void ReadInfo(Monitor m) // Resd info about current monitor
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref System.Drawing.Rectangle lprcMonitor, IntPtr dwData)
        {
            _monitorCollection.Add(hMonitor);
            return true;
        }

    }
}
