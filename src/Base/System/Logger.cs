using johncocom.Jot.Base.System;
using System;

namespace johncocom.Jot.Base.System
{
    public enum LogLevels
    {
        Debug,
        Warn,
        Error,
        Info,
        Trace
    }

    public abstract class Logger
    {
        private NLog.Logger NLogger;
        public LogOpts Opts;

        public Logger(LogOpts opts = null)
        {
            Opts = opts ?? new LogOpts();
            InitLog();
        }

        public virtual void InitLog()
        {
            NLogger = NLog.LogManager.GetLogger(this.GetType().FullName);
        }

        public void WriteLine(string message)
        {
            Opts.ShowLog?.Invoke(message);
            if (!Opts.IsConsole)
            {
                return;
            }
            Console.WriteLine(message);
        }

        public void Log(Exception ex, string extra = "")
        {
            var message = "";
            if (!string.IsNullOrEmpty(ex.Message))
            {
                message += $"Exception: {ex.Message}\r\n";
            }
            if (!string.IsNullOrEmpty(extra))
            {
                message += $"Extra: {extra}\r\n";
            }
            if (!string.IsNullOrEmpty(ex.InnerException?.ToString()))
            {
                message += $"Inner Exception: {ex.InnerException}\r\n";
            }
            message += ex.StackTrace;
            Log(message, LogLevels.Error);
        }

        public void Log(string message, LogLevels level = LogLevels.Debug)
        {
            switch (level)
            {
                case LogLevels.Warn:
                    Warn(message);
                    break;
                case LogLevels.Error:
                    Error(message);
                    break;
                case LogLevels.Info:
                    Info(message);
                    break;
                case LogLevels.Trace:
                    Trace(message);
                    break;
                default:
                    Debug(message);
                    break;
            }
            WriteLine(message);
        }

        public void Debug(string message) => NLogger?.Debug(message);
        public void Warn(string message) => NLogger?.Warn(message);
        public void Error(string message) => NLogger?.Error(message);
        public void Info(string message) => NLogger?.Info(message);
        public void Trace(string message) => NLogger?.Trace(message);
    }
}
