using System;

namespace Nista.Jottre.Base
{
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

        public void Debug(string message)
        {
            NLogger.Debug(message);
            WriteLine(message);
        }

        public void Error(string message)
        {
            NLogger.Error(message);
            WriteLine(message);
        }

        public void Info(string message)
        {
            NLogger.Info(message);
            WriteLine(message);
        }

        public void Trace(string message)
        {
            NLogger.Trace(message);
            WriteLine(message);
        }

    }
}