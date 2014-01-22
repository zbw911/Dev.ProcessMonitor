// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月21日 15:06
//  
//  修改于：2014年01月22日 16:50
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.Test/UnitTest1.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.ProcessMonitor.Test
{
    [TestClass]
    public class UnitTestCpuMem
    {
        #region Instance Methods

        [TestMethod]
        public void GetAllProcess()
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                Console.WriteLine("Process Name: {0}, Responding: {1}", process.ProcessName, process.Responding);
            }

            Console.Write("press enter");
        }


        [TestMethod]
        public void GetProcessByName()
        {
            string processName = "conhost";
            Process[] prcess = Process.GetProcessesByName(processName);

            foreach (Process process in prcess)
            {
                Console.WriteLine(process.ProcessName + "=>" + process.Id + "=>" + process.MainWindowTitle + "=>" +
                                  process.Responding);
            }
        }


        [TestMethod]
        public void TestCpu2()
        {
            string processName = "StatService";
            //获取当前进程对象
            Process cur = Process.GetProcessesByName(processName)[0];

            var curpcp = new PerformanceCounter("Process", "Working Set - Private", cur.ProcessName);
            var curpc = new PerformanceCounter("Process", "Working Set", cur.ProcessName);
            var curtime = new PerformanceCounter("Process", "% Processor Time", cur.ProcessName);

            //上次记录CPU的时间
            TimeSpan prevCpuTime = TimeSpan.Zero;
            //Sleep的时间间隔
            int interval = 1000;

            var totalcpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            var sys = new SystemInfo();
            const int KB_DIV = 1024;
            const int MB_DIV = 1024 * 1024;
            const int GB_DIV = 1024 * 1024 * 1024;

            int i = 0;
            while (i++ < 5)
            {
                //第一种方法计算CPU使用率
                //当前时间
                TimeSpan curCpuTime = cur.TotalProcessorTime;
                //计算
                double value = (curCpuTime - prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
                prevCpuTime = curCpuTime;

                Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集(进程类)", cur.WorkingSet64 / 1024,
                    value); //这个工作集只是在一开始初始化，后期不变
                Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集        ", curpc.NextValue() / 1024,
                    value); //这个工作集是动态更新的
                //第二种计算CPU使用率的方法
                Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}%", cur.ProcessName, "私有工作集    ", curpcp.NextValue() / 1024,
                    curtime.NextValue() / Environment.ProcessorCount);
                //Thread.Sleep(interval);


                //第一种方法获取系统CPU使用情况
                Console.WriteLine("1系统CPU使用率：{0}%", totalcpu.NextValue());
                //Thread.Sleep(interval);

                //第二章方法获取系统CPU和内存使用情况
                Console.WriteLine("2系统CPU使用率：{0}%，系统内存使用大小：{1}MB({2}GB)", sys.CpuLoad,
                    (sys.PhysicalMemory - sys.MemoryAvailable) / MB_DIV,
                    (sys.PhysicalMemory - sys.MemoryAvailable) / (double)GB_DIV);
                Thread.Sleep(interval);
            }

            //Console.ReadLine();
        }


        [TestMethod]
        public void TestCpuLev()
        {
            string processName = "StatService";


            IDictionary<int, double> lev = ProcessManager.ProcessCpu(processName);

            foreach (var f in lev)
            {
                Console.WriteLine(f.Key + "=>" + f.Value);
            }
        }

        [TestMethod]
        public void TestGetCpu()
        {
            string processName = "StatService";
            IDictionary<int, double> dicCpu = ProcessManager.ProcessCpu(processName);

            foreach (var f in dicCpu)
            {
                Console.WriteLine(f.Key + "=>" + f.Value);
            }
        }


        [TestMethod]
        public void TestMem()
        {
            string processName = "StatService";
            //string processName = "BingDict";

            Process[] process = Process.GetProcessesByName(processName);

            foreach (Process process1 in process)
            {
                //Console.WriteLine(process1.NonpagedSystemMemorySize64 / 1024 / 1024);
                //Console.WriteLine(process1.VirtualMemorySize64 / 1024 / 1024);
                //Console.WriteLine(process1.PagedMemorySize64 / 1024 / 1024);                //Console.WriteLine(process1.NonpagedSystemMemorySize64 / 1024 );
                //Console.WriteLine(process1.VirtualMemorySize64 / 1024 / 1024);
                //Console.WriteLine(process1.WorkingSet64 / 1024 / 1024);
                //Console.WriteLine(process1.PeakWorkingSet64 / 1024 / 1024);
                //Console.WriteLine(process1.PrivateMemorySize64 / 1024 / 1024);


                Console.WriteLine(ProcessManager.ProcessWorkSetMemory(process1.Id) / 1024);

                Console.WriteLine();
            }
        }

        [TestMethod]
        public void getAllProcessCpu()
        {
            //新建一个Stopwatch变量用来统计程序运行时间
            Stopwatch watch = Stopwatch.StartNew();
            //获取本机运行的所有进程ID和进程名,并输出哥进程所使用的工作集和私有工作集
            foreach (Process ps in Process.GetProcesses())
            {
                var pf1 = new PerformanceCounter("Process", "Working Set - Private", ps.ProcessName);
                var pf2 = new PerformanceCounter("Process", "Working Set", ps.ProcessName);
                Console.WriteLine("{0}:{1}  {2:N}KB", ps.ProcessName, "工作集(进程类)", ps.WorkingSet64 / 1024);
                Console.WriteLine("{0}:{1}  {2:N}KB", ps.ProcessName, "工作集        ", pf2.NextValue() / 1024);
                //私有工作集
                Console.WriteLine("{0}:{1}  {2:N}KB", ps.ProcessName, "私有工作集    ", pf1.NextValue() / 1024);
            }

            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }


        [TestMethod]
        public void GetProcessByid()
        {

            Process localById = Process.GetProcessById(11234);
        }


        [TestMethod]
        public void MyTestMethod()
        {

            string filename =
                @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            string arg = "";


           var  _processStarterSync = new ProcessStarterSync(filename, arg);
            //_processStarterSync.StandardErrorOut += starter_StandardErrorOut;
            //_processStarterSync.StandardOut += starter_StandardOut;
            //_processStarterSync.Finished += starter_Finished;
            _processStarterSync.StartAsync();

            var pro = Process.GetProcessesByName("Dev.ProcessMonitor.TestTargerExe");

            foreach (var process in pro)
            {
                var isrunn = Dev.ProcessMonitor.ProcessManager.IsProcessRunning(process.Id);

                var isRespding = Dev.ProcessMonitor.ProcessManager.IsProcessResponding(process.Id);
            }

           
        }

        #endregion
    }
}