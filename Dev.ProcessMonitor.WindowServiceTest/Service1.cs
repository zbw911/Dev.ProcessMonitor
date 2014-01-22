using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace Dev.ProcessMonitor.WindowServiceTest
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToFile("./test.txt"));

            Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToLog4net());

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //MessageBox.Show("asdfasdfasdf");
            Dev.Log.Loger.Error("testaaa");
        }

        protected override void OnStop()
        {
        }
    }
}
