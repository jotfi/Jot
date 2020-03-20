using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Base.System
{
    public class LogOpts
    {
        public bool IsConsole { get; set; }
        public Action<string> ShowLog { get; set; }

        public LogOpts(bool isConsole = false, Action<string> showLog = null) => (IsConsole, ShowLog) = (isConsole, showLog);
    }
}
