using log4net;
using System.Runtime.CompilerServices;

namespace Chess.Infrastructure.Logging
{
    public class Logger
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Logger));

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Log(LogLevel logLevel, string message, [CallerMemberName]string callerMemberName = "", [CallerLineNumber]int callerLineNumber = 0)
        {
            message = string.Format("{0}{1}- {2}", callerLineNumber, callerMemberName, message);
            switch (logLevel)
            {
                case LogLevel.Debug:
                    _logger.Debug(message);
                    break;
                case LogLevel.Info:
                    _logger.Info(message);
                    break;
                default:
                    break;
            }
        }
    }

    public enum LogLevel
    {
        Debug,
        Info
    }
}
