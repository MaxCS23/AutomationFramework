using Serilog;
using Serilog.Events;



namespace AutomationFramework.Core
{
    public static class Logger
    {
        public static void Init() 
        {
            var level = ConfigManager.Settings.loglevel.ToLower();

            LogEventLevel logLevel = level switch
            {
                "debug" => LogEventLevel.Debug,
                "error" => LogEventLevel.Error,
                "warning" => LogEventLevel.Warning,
                _ => LogEventLevel.Information
            };

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var logPath = $"logs/execution_{timestamp}.log";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .WriteTo.File(logPath)
                .CreateLogger();
        }
        public static ILogger GetLogger() => Log.Logger;
    }    
}
