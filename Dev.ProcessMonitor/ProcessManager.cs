using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Dev.ProcessMonitor
{
    /// <summary>
    /// </summary>
    public static class ProcessManager
    {
        #region Class Methods

        /// <summary>
        ///     Gets all processes.
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetProcesses()
        {
            var ht = new Hashtable();
            foreach (Process process in Process.GetProcesses())
                ht.Add(Convert.ToInt32(process.Id), process.ProcessName);
            return ht;
        }

        /// <summary>
        ///     Determines if the process is running or NOT.
        /// </summary>
        public static bool IsProcessRunning(string processname)
        {
            Process[] proc = Process.GetProcessesByName(processname);

            bool x = proc.First().Responding;

            return !(proc.Length == 0 && proc == null);
        }

        /// <summary>
        ///     Kills the process by id.
        /// </summary>
        /// <param name="idToKill">The process Id.</param>
        public static void KillProcessById(int idToKill)
        {
            foreach (Process process in Process.GetProcesses())
                if (process.Id == idToKill)
                    process.Kill();
        }

        /// <summary>
        ///     Kills the process by name.
        /// </summary>
        /// <param name="nameToKill">The process name.</param>
        public static void KillProcessByName(string nameToKill)
        {
            foreach (Process process in Process.GetProcesses())
                if (process.ProcessName == nameToKill)
                    process.Kill();
        }

        /// <summary>
        ///     取得进程名的CPU占用情况
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static IDictionary<int, double> ProcessCpu(string processName, int interval = 1*1000)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return Cpu(processes, interval);
        }

        /// <summary>
        ///     根据进程编号取得CPU占用量
        /// </summary>
        /// <param name="processid"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static IDictionary<int, double> ProcessCpu(int processid, int interval = 1*1000)
        {
            Process processes = Process.GetProcessById(processid);
            return Cpu(new[] {processes}, interval);
        }

        /// <summary>
        ///     工作集内在，这个与 从任务管理器中看到的 Work Set - Privte 内存是不一样的
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static long ProcessWorkSetMemory(int processId)
        {
            Process process = Process.GetProcessById(processId);

            return process.WorkingSet64;
        }

        private static Dictionary<int, double> Cpu(Process[] processes, int interval)
        {
            int processorcount = Environment.ProcessorCount;


            var Dic = new Dictionary<int, TimeSpan>();

            foreach (Process process in processes)
            {
                TimeSpan now = process.TotalProcessorTime;
                Dic[process.Id] = now;
            }

            Thread.Sleep(interval);

            var dicle = new Dictionary<int, double>();
            foreach (Process process in processes)
            {
                TimeSpan now = process.TotalProcessorTime;

                TimeSpan ts = now - Dic[process.Id];
                dicle[process.Id] = ts.TotalMilliseconds;
            }

            var result = new Dictionary<int, double>();

            foreach (var l in dicle)
            {
                double c = l.Value/(processorcount*interval);

                result[l.Key] = c;
            }

            return result;
        }

        #endregion
    }
}