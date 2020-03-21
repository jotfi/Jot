using jotfi.Jot.Base.System;
using jotfi.Jot.Data.System;
using jotfi.Jot.Database;
using jotfi.Jot.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Data
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
