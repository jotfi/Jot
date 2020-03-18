using Nista.Jottre.Base.System;
using Nista.Jottre.Data.System;
using Nista.Jottre.Database;
using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data
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
