using johncocom.Jot.Base.System;
using johncocom.Jot.Data.System;
using johncocom.Jot.Database;
using johncocom.Jot.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Data
{
    public class RepositoryController : Logger
    {
        public readonly SystemRepositories System;
        public readonly DatabaseController Db;

        public RepositoryController(DatabaseController db, LogOpts opts = null) : base(opts)
        {
            Db = db;
            System = new SystemRepositories(this);
        }

    }
}
