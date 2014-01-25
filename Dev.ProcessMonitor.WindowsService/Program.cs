// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 21:10
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.WindowsService/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.ServiceProcess;

namespace Dev.ProcessMonitor.WindowsService
{
    internal static class Program
    {
        #region Class Methods

        //C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe [path]
        // cd C:\Windows\Microsoft.NET\Framework\v4.0.30319

        // 测试的时候安装服务
        //\bin\Debug>installutil Dev.ProcessMonitor.WindowsService.exe
        //\bin\Debug>installutil /u Dev.ProcessMonitor.WindowsService.exe

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
        }

        #endregion
    }
}