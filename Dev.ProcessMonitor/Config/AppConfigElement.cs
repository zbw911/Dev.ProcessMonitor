// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 11:30
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/AppConfigElement.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Configuration;

namespace Dev.ProcessMonitor.Config
{
    /// <summary>
    ///     应用程序的设置
    /// </summary>
    public class AppConfigElement : ConfigurationElement
    {
        #region C'tors

        public AppConfigElement(String name, String url)
        {
            Name = name;
            Path = url;
        }

        public AppConfigElement()
        {
            Name = "";
            Path = "";
            Args = "";
        }

        #endregion

        #region Instance Properties

        [ConfigurationProperty("args", IsRequired = true)]
        public string Args
        {
            get { return (string) this["args"]; }
            set { this["args"] = value; }
        }

        [ConfigurationProperty("name", DefaultValue = "Contoso",
            IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "http://www.contoso.com",
            IsRequired = true)]
        public string Path
        {
            get { return (string) this["path"]; }
            set { this["path"] = value; }
        }

        #endregion
    }
}