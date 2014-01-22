// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 17:30
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.WindowServiceTest/Service1.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************
using System.ServiceProcess;
using Dev.Log;
using Dev.Log.Config;
using Dev.Log.Impl;

namespace Dev.ProcessMonitor.WindowServiceTest
{
    public partial class Service1 : ServiceBase
    {
        #region C'tors

        public Service1()
        {
            Setting.AttachLog(new ObserverLogToFile("./test.txt"));

            Setting.AttachLog(new ObserverLogToLog4net());

            InitializeComponent();
        }

        #endregion

        #region Instance Methods

        protected override void OnStart(string[] args)
        {
            //MessageBox.Show("asdfasdfasdf");
            Loger.Error("testaaa");
        }

        protected override void OnStop()
        {
        }

        #endregion
    }
}