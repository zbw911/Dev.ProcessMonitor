// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 15:08
//  
//  修改于：2014年01月22日 16:50
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/ProcessStarter.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Diagnostics;

namespace Dev.ProcessMonitor
{
  


    /// <summary>
    ///     进程启动器
    /// </summary>
    public class ProcessStarter
    {
        #region Readonly & Static Fields

        private readonly string arguments;
        private readonly string fileName;

        #endregion

        #region Fields

        private bool CancellationPending;
        private string standardError;

        #endregion

        #region C'tors

        public ProcessStarter(string fileName, string arguments)
        {
            this.fileName = fileName;
            this.arguments = arguments;
        }

        #endregion

        #region Instance Properties

        public int ProcessId { get; set; }

        #endregion

        #region Instance Methods

        public void Cancel()
        {
            CancellationPending = true;
        }

        public void Start()
        {
            //var worker = new BackgroundWorker();
            CancellationPending = false;

            try
            {
                var p = new Process();
                //Asynchron read of standardoutput:
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.process.beginoutputreadline.aspx
                //p.ErrorDataReceived  +=p_ErrorDataReceived;

                p.ErrorDataReceived += p_ErrorDataReceived;
                p.StartInfo.FileName = fileName; //@"";
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = arguments;
                p.Start();


                ProcessId = p.Id;

                // http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
                p.BeginErrorReadLine();
                string standardOut;
                while (((standardOut = p.StandardOutput.ReadLine()) != null) && (!CancellationPending))
                {
                    OnStandardOut(standardOut);
                }
                if (!CancellationPending)
                {
                    p.WaitForExit();
                    string result = "Exited with the Exitcode: " + p.ExitCode + "\n" + standardError;
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

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            standardError += e.Data;
        }

        #endregion

        #region Event Declarations

        public event EventHandler<StandardErrorArg> StandardErrorOut;
        public event EventHandler<StandardOutArg> StandardOut;

        #endregion
    }
}