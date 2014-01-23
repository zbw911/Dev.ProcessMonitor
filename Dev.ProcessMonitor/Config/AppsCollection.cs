using System;
using System.Configuration;

namespace Dev.ProcessMonitor.Config
{
    /// <summary>
    /// 用于管理的应用程序集合
    /// </summary>
    public class AppsCollection : ConfigurationElementCollection
    {
        public AppsCollection()
        {
            //AppConfigElement app = (AppConfigElement)CreateNewElement();
            //Add(app);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AppConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((AppConfigElement)element).Name;
        }

        public AppConfigElement this[int index]
        {
            get
            {
                return (AppConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new AppConfigElement this[string Name]
        {
            get
            {
                return (AppConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(AppConfigElement app)
        {
            return BaseIndexOf(app);
        }

        public void Add(AppConfigElement app)
        {
            BaseAdd(app);
        }
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(AppConfigElement app)
        {
            if (BaseIndexOf(app) >= 0)
                BaseRemove(app.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}