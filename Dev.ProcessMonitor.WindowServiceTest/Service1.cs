// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 17:30
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.WindowServiceTest/Service1.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Dev.Log;
using Dev.Log.Config;
using Dev.Log.Impl;

namespace Dev.ProcessMonitor.WindowServiceTest
{
    public partial class Service1 : ServiceBase
    {
        #region C'tors

        private Monitor m;
        public Service1()
        {
            m = new Monitor(false);
            //Setting.AttachLog(new ObserverLogToFile("./test.txt"));
            //Setting.AttachLog(new Dev.Log.Impl.ObserverLogToEventlog());

            Setting.AttachLog(new ObserverLogToLog4net());

            InitializeComponent();
        }

        #endregion

        #region Instance Methods

        private Process p;
        protected override void OnStart(string[] args)
        {
            //MessageBox.Show("asdfasdfasdf");
            //Loger.Error("Start");

            m = new Monitor(true);
            m.StandardErrorOut += m_StandardErrorOut;
            m.StandardOut += m_StandardOut;
            m.Start();


            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerAsync();




        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            p = new Process();
            var file =
                @"E:\Github\Dev.ProcessMonitor\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";

            //@"‪E:\Github\Dev.ProcessMonitor\Dev.ProcessMonitor.FormTest\bin\Debug\Dev.ProcessMonitor.FormTest.exe";
            p.StartInfo = new ProcessStartInfo(file);//@"E:\Github\Dev.ProcessMonitor\Dev.ProcessMonitor.ConsoleTest\bin\Debug\Dev.ProcessMonitor.ConsoleTest.exe");
            p.Start();


            p.WaitForExit();
            base.Stop();
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
            //if (p != null && !p.HasExited)
            //    p.Kill();
            //Loger.Error("Stop");
            //m.Stop();
        }

        #endregion
    }
}