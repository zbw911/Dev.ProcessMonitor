using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.ProcessMonitor.Test
{
    [TestClass]
    public class TestMonitor
    {
        [TestMethod]
        public void TestMethod1()
        {
            Monitor m = new Monitor(true);
            m.Start();

            Thread.Sleep(10 * 1000);
        }
    }
}
