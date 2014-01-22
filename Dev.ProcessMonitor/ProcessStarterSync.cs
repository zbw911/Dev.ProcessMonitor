// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 16:05
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/ProcessStarterSync.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************
using System;
using System.ComponentModel;
using System.Diagnostics;

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

        public int ProcessId { get; set; }

        #endregion

        #region Instance Methods

        /// <summary>
        ///     cancels the running encodingprocess
        /// </summary>
        public void CancelEncodeAsync()
        {
            _backgroundWorker1.CancelAsync();
        }

        public void StartAsync()
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            var parms = e.Argument as StartParameters;

            try
            {
                var p = new Process();
                //Asynchron read of standardoutput:
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.process.beginoutputreadline.aspx
                //p.ErrorDataReceived  +=p_ErrorDataReceived;

                p.ErrorDataReceived += p_ErrorDataReceived;
                p.StartInfo.FileName = parms.FileName; //@"";
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;

                p.StartInfo.Arguments = parms.Arguments;
                p.Start();


                ProcessId = p.Id;

                // http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
                p.BeginErrorReadLine();
                string standardOut;
                while (((standardOut = p.StandardOutput.ReadLine()) != null) && (!worker.CancellationPending))
                {
                    OnStandardOut(standardOut);
                }
                if (!worker.CancellationPending)
                {
                    p.WaitForExit();
                    string result = "Exited with the Exitcode: " + p.ExitCode + "\n" + _standardError;
                    OnStandardOut(result);
                }
                else
                {
                    const string result = "Canceld!";

                    OnStandardOut(result);

                    p.Close();
                    p.CancelErrorRead();
                    p.Dispose();
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

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _standardError += e.Data;
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