using NLog;
using Serilog;
using System.Diagnostics;

static class Program
{
    static void Main()
    {
        // event viewer
        var eventLog = new EventLog("Application")
        {
            Source = "MyCompanyApp"
        };
        eventLog.WriteEntry("Hello World from event viewver logger", EventLogEntryType.Information);

        // serilog 
        string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        string serilogPath = Path.Combine(root, $"serilog-logs.txt");
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(serilogPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        Log.Information("Hello World from Serilog logger");

        // nlog
        string nlogPath = Path.Combine(root, $"nlog-logs.txt");
        var config = new NLog.Config.LoggingConfiguration();

        var logfile = new NLog.Targets.FileTarget("logfile") { FileName = nlogPath };
        var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

        config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

        NLog.LogManager.Configuration = config;
        var logger = NLog.LogManager.GetCurrentClassLogger();
        logger.Info("Hello from NLog logger");

        // log4net
    }
}