// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 21:10
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.WindowsService/Service.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Diagnostics;
using System.ServiceProcess;
using Dev.Log;
using Dev.Log.Config;
using Dev.Log.Impl;

namespace Dev.ProcessMonitor.WindowsService
{
    public partial class Service : ServiceBase
    {
        #region Fields

        private Monitor m;

        private Process p;

        #endregion

        #region C'tors

        public Service()
        {
            //m = new Monitor(false);
            m = new Monitor(true);
            m.StandardErrorOut += m_StandardErrorOut;
            m.StandardOut += m_StandardOut;
            //Setting.SetLogSeverity(LogSeverity.Info);
            Setting.AttachLog(new ObserverLogToLog4net());

            InitializeComponent();
        }

        #endregion

        #region Instance Methods

        protected override void OnStart(string[] args)
        {
           
            m.Start();
        }


        protected override void OnStop()
        {
            m.Stop();
        }

        #endregion

        #region Event Handling

        private void m_StandardErrorOut(object sender, StandardErrorArg e)
        {
            Loger.Error(e.OutPut);
        }

        private void m_StandardOut(object sender, StandardOutArg e)
        {
            Loger.Info(e.OutPut);
        }

        #endregion
    }
}