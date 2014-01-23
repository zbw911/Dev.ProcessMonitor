using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.ProcessMonitor.Config
{
    public static class ConfigMananger
    {
        public static MonitorSettingSection MonitorSetting
        {
            get
            {
                var config = (Dev.ProcessMonitor.Config.MonitorSettingSection)
                  System.Configuration
                  .ConfigurationManager.GetSection("processMonitorGroup/settings");

                return config;
            }
        }
        public static AppsCollection Apps
        {
            get { return MonitorSetting.Apps; }
        }

        public static Checker CheckSetting
        {
            get { return MonitorSetting.CheckSetting; }
        }
    }
}
