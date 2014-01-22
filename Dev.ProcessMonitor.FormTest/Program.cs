// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 15:52
//  
//  修改于：2014年01月22日 16:50
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.FormTest/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Windows.Forms;

namespace Dev.ProcessMonitor.FormTest
{
    internal static class Program
    {
        #region Class Methods

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Console.WriteLine("started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #endregion
    }
}