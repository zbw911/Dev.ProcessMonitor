// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 17:06
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/Args.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.ProcessMonitor
{
    public class OutArg : EventArgs
    {
        #region Instance Properties

        public string OutPut { get; set; }

        public int ProcessId { get; set; }

        #endregion
    }

    public class StandardOutArg : OutArg
    {
    }

    public class StandardErrorArg : OutArg
    {
    }
}