using System;
using System.Threading;

namespace Dev.ProcessMonitor.TestTargerExe
{
    internal class Program
    {
        #region Class Methods

        private static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i + "<:>" + DateTime.Now);
                Thread.Sleep(1);
            }
        }

        #endregion
    }
}