using Nista.Jottre.Base.Log;
using Nista.Jottre.Data.System;
using Nista.Jottre.Database;
using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data
{
    public class RepositoryController : Logging
    {
        public readonly SystemRepositories System;
        public readonly DatabaseController Db;

        public RepositoryController(DatabaseController db, 
            bool isConsole = true, Action<string> showLog = null) : base(isConsole, showLog)
        {
            Db = db;
            System = new SystemRepositories(this);
        }

    }
}
