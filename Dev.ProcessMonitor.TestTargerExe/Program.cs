using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dev.ProcessMonitor.TestTargerExe
{
    class Program
    {
        static void Main(string[] args)
        {

            for (var i = 0; i < 1000; i++)
            {

                Console.WriteLine(i + "<:>" + System.DateTime.Now);
                Thread.Sleep(1);
            }

        }
    }
}
