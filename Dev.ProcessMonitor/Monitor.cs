// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 13:42
//  
//  修改于：2014年01月23日 14:50
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/Monitor.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Diagnostics;
using System.Threading;
using Dev.ProcessMonitor.Config;

namespace Dev.ProcessMonitor
{
    public class Monitor
    {
        #region Readonly & Static Fields

        private readonly bool _isSync;

        #endregion

        #region Fields

        private Thread checkThread;
        private bool isStop;

        #endregion

        #region C'tors

        public Monitor(bool isSync)
        {
            _isSync = isSync;
        }

        #endregion

        #region Instance Methods

        public void Start()
        {
            isStop = false;
            if (_isSync)
            {
                checkThread = new Thread(IntervalCheck);
                checkThread.IsBackground = true;

                checkThread.Start();
            }
            else
            {
                IntervalCheck();
            }
        }

        public void Stop()
        {
            if (checkThread != null)
            {
                try
                {
                    checkThread.Abort();
                }
                catch (ThreadStateException e)
                {
                    // no thing 
                }
            }
            isStop = true;
            LoopKill();
        }

        protected virtual void OnStandardErrorOut(StandardErrorArg e)
        {
            EventHandler<StandardErrorArg> handler = StandardErrorOut;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnStandardOut(StandardOutArg e)
        {
            EventHandler<StandardOutArg> handler = StandardOut;
            if (handler != null) handler(this, e);
        }

        private void LoopCheck()
        {
            for (int i = 0; i < ConfigMananger.Apps.Count; i++)
            {
                AppConfigElement app = ConfigMananger.Apps[i];

                bool isrun = ProcessManager.IsProcessRunning(app.Name);
                bool isProcessResponding = ProcessManager.IsProcessResponding(app.Name);

                Dev.Log.Loger.Debug("isProcessResponding=>" + isProcessResponding + ";isrun=>" + isrun);


                 
                if (!isrun || !isProcessResponding)
                {
                    ProcessManager.KillProcessByName(app.Name);
                    var processstarter = new ProcessStarterSync(app.Path, app.Args);
                    processstarter.StandardErrorOut += processstarter_StandardErrorOut;
                    processstarter.StandardOut += processstarter_StandardOut;
                    processstarter.Finished += (o, e) =>
                    {
                        Dev.Log.Loger.Info(app.Name + "启动完成");
                    };
                    processstarter.StartSync();

                    Dev.Log.Loger.Info(app.Name + "启动中");
                }
            }
        }



        private void LoopKill()
        {
            for (int i = 0; i < ConfigMananger.Apps.Count; i++)
            {
                AppConfigElement app = ConfigMananger.Apps[i];

                ProcessManager.KillProcessByName(app.Name);
            }
        }


        private void IntervalCheck()
        {
            int interval = ConfigMananger.CheckSetting.Interval;
            while (true)
            {
                if (isStop)
                    break;


                LoopCheck();

                Thread.Sleep(interval * 1000);
            }
        }

        #endregion

        #region Event Handling

        private void processstarter_StandardErrorOut(object sender, StandardErrorArg e)
        {
            OnStandardErrorOut(e);
        }

        private void processstarter_StandardOut(object sender, StandardOutArg e)
        {
            OnStandardOut(e);
        }

        #endregion

        #region Event Declarations

        public event EventHandler<StandardErrorArg> StandardErrorOut;
        public event EventHandler<StandardOutArg> StandardOut;

        #endregion
    }
}