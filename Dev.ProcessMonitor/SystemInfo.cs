// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月21日 16:29
//  
//  修改于：2014年01月23日 21:25
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor/SystemInfo.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Diagnostics;
using System.Management;

namespace Dev.ProcessMonitor
{
    public class SystemInfo
    {
        #region Readonly & Static Fields

        private readonly long m_PhysicalMemory; //物理内存
        private readonly int m_ProcessorCount; //CPU个数
        private readonly PerformanceCounter pcCpuLoad; //CPU计数器

        #endregion

        #region C'tors

        /// <summary>
        ///     构造函数，初始化计数器等
        /// </summary>
        public SystemInfo()
        {
            //初始化CPU计数器
            pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pcCpuLoad.MachineName = ".";
            pcCpuLoad.NextValue();

            //CPU个数
            m_ProcessorCount = Environment.ProcessorCount;

            //获得物理内存
            var mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    m_PhysicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString());
                }
            }
        }

        #endregion

        #region Instance Properties

        /// <summary>
        ///     获取CPU占用率
        /// </summary>
        public float CpuLoad
        {
            get { return pcCpuLoad.NextValue(); }
        }

        /// <summary>
        ///     获取可用内存
        /// </summary>
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory");
                //foreach (ManagementObject mo in mos.Get())
                //{
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString());
                //}
                var mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024*long.Parse(mo["FreePhysicalMemory"].ToString());
                    }
                }
                return availablebytes;
            }
        }

        /// <summary>
        ///     获取物理内存
        /// </summary>
        public long PhysicalMemory
        {
            get { return m_PhysicalMemory; }
        }

        /// <summary>
        ///     获取CPU个数
        /// </summary>
        public int ProcessorCount
        {
            get { return m_ProcessorCount; }
        }

        #endregion
    }
}