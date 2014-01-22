// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 17:30
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.WindowServiceTest/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************
using System.ServiceProcess;

namespace Dev.ProcessMonitor.WindowServiceTest
{
    //\bin\Debug>installutil Dev.ProcessMonitor.WindowServiceTest.exe
    //\bin\Debug>installutil /u Dev.ProcessMonitor.WindowServiceTest.exe
    internal static class Program
    {
        #region Class Methods

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }

        #endregion
    }
}