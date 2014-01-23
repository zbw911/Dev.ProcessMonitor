using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Dev.Log;
using Dev.Log.Config;
using Dev.Log.Impl;

namespace Dev.ProcessMonitor.WindowsService
{
    public partial class Service  : ServiceBase
    {
        #region C'tors

        private Monitor m;
        public Service()
        {
            m = new Monitor(false);


            Setting.AttachLog(new ObserverLogToLog4net());

            InitializeComponent();
        }

        #endregion

        #region Instance Methods

        private Process p;
        protected override void OnStart(string[] args)
        {
           
            m = new Monitor(true);
            m.StandardErrorOut += m_StandardErrorOut;
            m.StandardOut += m_StandardOut;
            m.Start();

        }



        void m_StandardOut(object sender, StandardOutArg e)
        {
            Loger.Info(e.OutPut);
        }

        void m_StandardErrorOut(object sender, StandardErrorArg e)
        {
            Loger.Error(e.OutPut);
        }

        protected override void OnStop()
        {
            m.Stop();
        }

        #endregion
    }
}