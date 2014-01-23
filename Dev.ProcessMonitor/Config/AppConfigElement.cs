using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dev.ProcessMonitor.Config
{
    /// <summary>
    /// 应用程序的设置
    /// </summary>
    public class AppConfigElement : ConfigurationElement
    {
        public AppConfigElement(String name, String url)
        {
            this.Name = name;
            this.Path = url;
        }

        public AppConfigElement()
        {

            this.Name = "";
            this.Path = "";
            this.Args = "";
        }

        [ConfigurationProperty("name", DefaultValue = "Contoso",
            IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("path", DefaultValue = "http://www.contoso.com",
            IsRequired = true)]

        public string Path
        {
            get
            {
                return (string)this["path"];
            }
            set
            {
                this["path"] = value;
            }
        }

        [ConfigurationProperty("args", IsRequired = true)]
        public string Args
        {
            get
            {
                return (string)this["args"];
            }
            set
            {
                this["args"] = value;
            }
        }
    }
}
