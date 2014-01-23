Dev.ProcessMonitor
==================

C# 进程监控管理
注意，如果启动带GUI的程序。
这个项目存在一个问题，现在只能启动 控制台应用程序，对于 带有GUI的程序是无法显示的,但可以在任何用户未登录的情况下运行。

下一步，将实现对于GUI的支持。
对于GUI可参见,当然，这是用户登录后的为每个用户会话显示应用程序GUI。
这个好像有点难度。

安装服务可以：
 
    \bin\Debug>installutil Dev.ProcessMonitor.WindowService.exe 安装
    \bin\Debug>installutil /u Dev.ProcessMonitor.WindowService.exe 卸载