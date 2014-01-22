﻿using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.ProcessMonitor.Test
{
    [TestClass]
    public class UnitTestProcessStart
    {
        #region Instance Methods

        [TestMethod]
        public void StartWinFormExe()
        {
            string filename =
                @"..\..\..\Dev.ProcessMonitor.FormTest\bin\Debug\Dev.ProcessMonitor.FormTest.exe";
            string arg = "";

            var p = new Process();

            p.StartInfo.FileName = filename; //@"";
            //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
        }

        [TestMethod]
        public void StartWinFormExeThenKill()
        {
            string filename =
                @"..\..\..\Dev.ProcessMonitor.FormTest\bin\Debug\Dev.ProcessMonitor.FormTest.exe";
            string arg = "";

            var p = new Process();

            p.StartInfo.FileName = filename; //@"";
            //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();

            Thread.Sleep(3*1000);

            ProcessManager.KillProcessById(p.Id);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string filename =
                @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            string arg = "";

            var p = new Process();

            p.StartInfo.FileName = filename; //@"";
            //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
            //p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
        }


       

        #endregion
    }
}