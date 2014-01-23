// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 14:18
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.TestTargerExe/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using Dev.Log;
using Dev.Log.Config;
using Dev.Log.Impl;

namespace Dev.ProcessMonitor.TestTargerExe
{
    internal class Program
    {
        #region Class Methods

        private static void Main(string[] args)
        {
            int all = 10000;
            Setting.SetLogSeverity(LogSeverity.Debug);
            Setting.AttachLog(new ObserverLogToLog4net());

            for (int i = 0; i < all; i++)
            {
                //if (i == all/2)
                //    throw new Exception("人为退出");

                Console.WriteLine(i + "<:>" + DateTime.Now);


                Loger.Error(i + "<:>" + DateTime.Now);
            }
        }

        #endregion
    }
}