// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 14:18
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.TestTargerExe/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Threading;

namespace Dev.ProcessMonitor.TestTargerExe
{
    internal class Program
    {
        #region Class Methods

        private static void Main(string[] args)
        {
            int all = 10000;


            for (int i = 0; i < all; i++)
            {
                if (i == all/2)
                    throw new Exception("人为退出");

                Console.WriteLine(i + "<:>" + DateTime.Now);
                Thread.Sleep(1);
            }
        }

        #endregion
    }
}