// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月23日 14:32
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.Test/TestMonitor.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.ProcessMonitor.Test
{
    [TestClass]
    public class TestMonitor
    {
        #region Instance Methods

        [TestMethod]
        public void TestMethod1()
        {
            var m = new Monitor(true);
            m.Start();

            Thread.Sleep(10*1000);
        }

        #endregion
    }
}