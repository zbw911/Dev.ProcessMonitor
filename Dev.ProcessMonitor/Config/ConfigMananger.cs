// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 13:43
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/ConfigMananger.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Configuration;

namespace Dev.ProcessMonitor.Config
{
    public static class ConfigMananger
    {
        #region Class Properties

        public static AppsCollection Apps
        {
            get { return MonitorSetting.Apps; }
        }

        public static Checker CheckSetting
        {
            get { return MonitorSetting.CheckSetting; }
        }

        public static MonitorSettingSection MonitorSetting
        {
            get
            {
                var config = (MonitorSettingSection)
                    ConfigurationManager.GetSection("processMonitorGroup/settings");

                return config;
            }
        }

        #endregion
    }
}