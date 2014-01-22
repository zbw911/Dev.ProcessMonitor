// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 10:56
//  
//  修改于：2014年01月22日 16:50
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.ConsoleTest/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.ProcessMonitor.ConsoleTest
{
    internal class Program
    {
        #region Class Methods

        private static void Main(string[] args)
        {


            string filename =
                @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            string arg = "";


            //string filename =
            //   @"..\..\..\Dev.ProcessMonitor.FormTest\bin\Debug\Dev.ProcessMonitor.FormTest.exe";
            //string arg = "";

            var starter = new ProcessStarter(filename, arg);
            starter.StandardErrorOut += starter_StandardErrorOut;
            starter.StandardOut += starter_StandardOut;
            starter.Start();


            Console.WriteLine("pressanykey");
        }

        private static void starter_StandardErrorOut(object sender, StandardErrorArg e)
        {
            Console.WriteLine("error=>" + e.ProcessId + "=>" + e.OutPut);
        }

        private static void starter_StandardOut(object sender, StandardOutArg e)
        {
            Console.WriteLine("standout=>" + e.ProcessId + "=>" + e.OutPut);
        }

        #endregion
    }
}