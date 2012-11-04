using System;
using System.Globalization;
using log4net;

namespace StatusUpdater
{
    public interface ILogger
    {
        void LogException(Exception exception);
        void LogError(string message);
        void LogWarningMessage(string message);
        void LogInfoMessage(string message);
    }
    public class Logger : ILogger
    {
        private static ILog _log;
        static Logger()
        {
            //var log4NetConfigDirectory = 
            //AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            //var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
            _log = LogManager.GetLogger(typeof(Logger));
            GlobalContext.Properties["host"] = Environment.MachineName;
        }
        public Logger(Type logClass)
        {
            _log = LogManager.GetLogger(logClass);
        }

        #region ILogger Members
        public void LogException(Exception exception)
        {
            if (_log.IsErrorEnabled)
                _log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", exception.Message), exception);
        }
        public void LogError(string message)
        {
            if (_log.IsErrorEnabled)
                _log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", message));
        }
        public void LogWarningMessage(string message)
        {
            if (_log.IsWarnEnabled)
                _log.Warn(string.Format(CultureInfo.InvariantCulture, "{0}", message));
        }
        public void LogInfoMessage(string message)
        {
            if (_log.IsInfoEnabled)
                _log.Info(string.Format(CultureInfo.InvariantCulture, "{0}", message));
        }
        #endregion
    }
}
