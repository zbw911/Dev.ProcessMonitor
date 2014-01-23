// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 15:52
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.FormTest/Program.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Text;
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

            //Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToLog4net());


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Dev.Log.Loger.Error("启动中。。。。。。。。。。。。。。。。。。");

            int all = 10000;
            Dev.Log.Config.Setting.SetLogSeverity(Log.LogSeverity.Debug);
            Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToLog4net());

            for (int i = 0; i < all; i++)
            {
                //if (i == all/2)
                //    throw new Exception("人为退出");

                Console.WriteLine(i + "<:>" + DateTime.Now);


                Dev.Log.Loger.Error(i + "<:>" + DateTime.Now);
            }
            x();
        }

        static void x()
        {
            try
            {
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                #region 应用程序的主入口点
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                #endregion
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);

                Dev.Log.Loger.Error(str);

                //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            Dev.Log.Loger.Error(str);
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            Dev.Log.Loger.Error(str);
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            var error = sb.ToString();

            Dev.Log.Loger.Error(error);

            return error;
        }
        #endregion
    }
}