using Serilog;

namespace ATM.LogUtils
{
    internal class Logger
    {
        private static readonly Logger Instance = new Logger();
        private readonly ILogger _logger;

        private Logger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void Log(string message)
        {
            Instance._logger.Information(message);
        }
    }
}
