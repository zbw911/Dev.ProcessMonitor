using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.ProcessMonitor.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            var arg = "";

            Dev.ProcessMonitor.ProcessStarter starter = new ProcessStarter(filename, arg);
            starter.StandardErrorOut += starter_StandardErrorOut;
            starter.StandardOut += starter_StandardOut;
            starter.Start();


            Console.WriteLine("pressanykey");

        }

        static void starter_StandardOut(object sender, StandardOutArg e)
        {
            Console.WriteLine("standout=>" + e.ProcessId + "=>" + e.OutPut);
        }

        static void starter_StandardErrorOut(object sender, StandardErrorArg e)
        {
            Console.WriteLine("error=>" + e.ProcessId + "=>" + e.OutPut);
        }
    }
}
