// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 17:47
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.WindowServiceTest/ProjectInstaller.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.ComponentModel;
using System.Configuration.Install;

namespace Dev.ProcessMonitor.WindowServiceTest
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        #region C'tors

        public ProjectInstaller()
        {
            InitializeComponent();
        }

        #endregion
    }
}