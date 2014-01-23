// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 16:05
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/ProcessStarterSync.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using Dev.Log;

namespace Dev.ProcessMonitor
{
    /// <summary>
    ///     进程启动器
    /// </summary>
    public class ProcessStarterSync
    {
        #region Readonly & Static Fields

        private readonly StartParameters _parameters;

        #endregion

        #region Fields

        private BackgroundWorker _backgroundWorker1;
        private Process _process;
        private string _standardError;

        #endregion

        #region C'tors

        public ProcessStarterSync(StartParameters parameters)
        {
            _parameters = parameters;
        }

        public ProcessStarterSync(string fileName, string arguments)
            : this(new StartParameters
            {
                Arguments = arguments,
                FileName = fileName
            })
        {
        }

        #endregion

        #region Instance Properties

        public int ProcessId
        {
            get
            {
                if (_process == null)
                {
                    return -1;
                    throw new Exception("进程未初始化");
                }
                return _process.Id;
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     cancels the running encodingprocess
        /// </summary>
        public void CancelEncodeAsync()
        {
            _backgroundWorker1.CancelAsync();
        }

        public void StartSync()
        {
            _backgroundWorker1 = new BackgroundWorker();
            _backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            _backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            _backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;


            _backgroundWorker1.RunWorkerAsync(_parameters);
            _backgroundWorker1.WorkerReportsProgress = true;
            _backgroundWorker1.WorkerSupportsCancellation = true;
        }

        protected virtual void OnFinished()
        {
            EventHandler handler = Finished;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnStandardErrorOut(StandardErrorArg e)
        {
            EventHandler<StandardErrorArg> handler = StandardErrorOut;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnStandardErrorOut(string stre)
        {
            OnStandardErrorOut(new StandardErrorArg {ProcessId = ProcessId, OutPut = stre});
        }

        protected virtual void OnStandardOut(StandardOutArg e)
        {
            EventHandler<StandardOutArg> handler = StandardOut;
            if (handler != null) handler(this, e);
        }


        protected virtual void OnStandardOut(string stre)
        {
            OnStandardOut(new StandardOutArg {ProcessId = ProcessId, OutPut = stre});
        }

        #endregion

        #region Event Handling

        private void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Loger.Error(e.Data);
            _standardError += e.Data;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            var parms = e.Argument as StartParameters;

            try
            {
                //parms.FileName =
                //   @"E:\Github\Dev.ProcessMonitor\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
                //parms.FileName =
                //                 @"E:\Github\Dev.ProcessMonitor\Dev.ProcessMonitor.FormTest\bin\Debug\Dev.ProcessMonitor.FormTest.exe";

                Loger.Info(string.Format("现在启动的程序路径为{0}", parms.FileName));

                _process = new Process();
                //Asynchron read of standardoutput:
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.process.beginoutputreadline.aspx


                _process.ErrorDataReceived += ProcessErrorDataReceived;
                _process.StartInfo.FileName = parms.FileName; //@"";
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                _process.StartInfo.RedirectStandardOutput = true;
                _process.StartInfo.RedirectStandardError = true;
                _process.StartInfo.UseShellExecute = false;
                _process.StartInfo.CreateNoWindow = true;
                //_process.StartInfo.UserName = System.Environment.UserName;


                _process.StartInfo.Arguments = parms.Arguments;
                _process.Start();


                //ProcessId = _process.Id;

                // http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
                _process.BeginErrorReadLine();
                string standardOut;
                while (((standardOut = _process.StandardOutput.ReadLine()) != null) && (!worker.CancellationPending))
                {
                    OnStandardOut(standardOut);
                }
                if (!worker.CancellationPending)
                {
                    _process.WaitForExit();
                    string result = "Exited with the Exitcode: " + _process.ExitCode + "\n" + _standardError;
                    OnStandardOut(result);
                }
                else
                {
                    const string result = "Canceld!";

                    OnStandardOut(result);

                    _process.Close();
                    _process.CancelErrorRead();
                    _process.Dispose();
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                OnStandardErrorOut(result);
                throw;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinished();
        }

        #endregion

        #region Event Declarations

        public event EventHandler Finished;

        public event EventHandler<StandardErrorArg> StandardErrorOut;
        public event EventHandler<StandardOutArg> StandardOut;

        #endregion
    }

    public class StartParameters
    {
        #region Instance Properties

        public string Arguments { get; set; }
        public string FileName { get; set; }

        #endregion
    }
}