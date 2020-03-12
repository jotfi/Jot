using System;

namespace Nista.Jottre.Base
{
    public abstract class Logger
    {
        protected readonly NLog.Logger Log;
        public Logger()
        {
            Log = NLog.LogManager.GetLogger(this.GetType().FullName);
        }
    }
}