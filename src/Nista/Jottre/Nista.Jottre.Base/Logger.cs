using System;

namespace Nista.Jottre.Base
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

        protected readonly bool IsConsole;
        protected readonly NLog.Logger NLogger;
        public Logger()
        {
            NLogger = NLog.LogManager.GetLogger(this.GetType().FullName);
        }

        public Logger(bool isConsole) : this()
        {
            IsConsole = isConsole;
        }

        //TODO: support Exception param

        public void WriteLine(string message)
        {
            if (!IsConsole)
            {
                return;
            }
            Console.WriteLine(message);
        }

        public void Log(Exception ex)
        {
            Log($"{ex.Message} {ex.InnerException} {ex.StackTrace}", LogLevels.Error);
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

        public void Debug(string message) => NLogger.Debug(message);
        public void Warn(string message) => NLogger.Warn(message);
        public void Error(string message) => NLogger.Error(message);
        public void Info(string message) => NLogger.Info(message);
        public void Trace(string message) => NLogger.Trace(message);


    }
}