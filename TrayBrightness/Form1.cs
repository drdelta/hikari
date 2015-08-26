﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrayBrightness.Monitor;
using TrayBrightness.MonitorCollection;
using TrayBrightness.MonitorFeature;
using TrayBrightness.FeatureType;
using TrayBrightness.Win32;
//[DllImport("dxva2.dll")]

namespace TrayBrightness
{
    public partial class Form1 : Form
    {
        private readonly MonitorCollection _monitorCollection = new MonitorCollection();
        private Monitor _currentMonitor;
        public Form1()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Minimized;

            /// Initialization
            /// 1. Enumerate Displays
            var MyInfoEnumProc = new NativeMethods.MonitorEnumDelegate(MonitorEnum);
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MyInfoEnumProc, IntPtr.Zero);  
            /// 2. Check supported functions / check brightness support

       }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width);
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.BackColor = System.Drawing.Color.DarkBlue;
        }
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
 
        private void setTo30ToolStripMenuItem_Click(object sender, EventArgs e)
        {

           // SetMonitorBrightness(1;100);
        }

        private void setTo50ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



    }
  
}
