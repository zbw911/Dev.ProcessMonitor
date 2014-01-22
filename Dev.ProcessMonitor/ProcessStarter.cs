using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Dev.ProcessMonitor
{
    public class OutArg : EventArgs
    {
        #region Instance Properties

        public string OutPut { get; set; }

        public int ProcessId { get; set; }

        #endregion
    }

    public class StandardOutArg : OutArg
    {
    }

    public class StandardErrorArg : OutArg
    {
    }


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

        public void Start()
        {
            var worker = new BackgroundWorker();


            try
            {
                var p = new Process();
                //Asynchron read of standardoutput:
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.process.beginoutputreadline.aspx
                //p.ErrorDataReceived  +=p_ErrorDataReceived;

                p.ErrorDataReceived += p_ErrorDataReceived;
                p.StartInfo.FileName = fileName; //@"mencoder.exe";
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = arguments;
                p.Start();


                ProcessId = p.Id;

                //nur eins darf synchron gelesen werden!! http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
                p.BeginErrorReadLine();
                string standardOut;
                while (((standardOut = p.StandardOutput.ReadLine()) != null) && (!worker.CancellationPending))
                {
                    OnStandardOut(standardOut);
                }
                if (!worker.CancellationPending)
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