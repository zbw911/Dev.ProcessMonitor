// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 13:14
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/AppsCollection.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Configuration;

namespace Dev.ProcessMonitor.Config
{
    /// <summary>
    ///     用于管理的应用程序集合
    /// </summary>
    public class AppsCollection : ConfigurationElementCollection
    {
        #region Instance Indexers

        public AppConfigElement this[int index]
        {
            get { return (AppConfigElement) BaseGet(index); }
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
            get { return (AppConfigElement) BaseGet(Name); }
        }

        #endregion

        #region Instance Properties

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        #endregion

        #region Instance Methods

        public void Add(AppConfigElement app)
        {
            BaseAdd(app);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(AppConfigElement app)
        {
            return BaseIndexOf(app);
        }

        public void Remove(AppConfigElement app)
        {
            if (BaseIndexOf(app) >= 0)
                BaseRemove(app.Name);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AppConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((AppConfigElement) element).Name;
        }

        #endregion
    }
}