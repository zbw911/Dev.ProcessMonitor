// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 13:14
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/AppsSection.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Configuration;

namespace Dev.ProcessMonitor.Config
{
    public class MonitorSettingSection : ConfigurationSection
    {
        // Declare the Urls collection property using the
        // ConfigurationCollectionAttribute.
        // This allows to build a nested section that contains
        // a collection of elements.

        #region Instance Properties

        [ConfigurationProperty("apps", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof (AppsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public AppsCollection Apps
        {
            get
            {
                var appsCollection = (AppsCollection) base["apps"];
                return appsCollection;
            }
        }

        [ConfigurationProperty("checker")]
        public Checker CheckSetting
        {
            get { return (Checker) this["checker"]; }
        }

        #endregion
    }


    /// <summary>
    ///     监测设置
    /// </summary>
    public class Checker : AppConfigElement
    {
        #region Instance Properties

        /// <summary>
        ///     被监测的程序失败后是否重启
        /// </summary>
        [ConfigurationProperty("errorRestart", DefaultValue = false)]
        public bool ErrorRestart
        {
            get { return (bool) this["errorRestart"]; }
            set { this["errorRestart"] = value; }
        }

        /// <summary>
        ///     检测时间间隔
        /// </summary>
        [ConfigurationProperty("interval", DefaultValue = 60)]
        public int Interval
        {
            get { return (int) this["interval"]; }
            set { this["interval"] = value; }
        }

        /// <summary>
        ///     所容忍的最大错误次数
        /// </summary>
        [ConfigurationProperty("maxErrorCount", DefaultValue = 10)]
        public int MaxErrorCount
        {
            get { return (int) this["maxErrorCount"]; }
            set { this["maxErrorCount"] = value; }
        }

        #endregion

        //second="60" errorRestart="true" maxErrorCount="10"
    }
}