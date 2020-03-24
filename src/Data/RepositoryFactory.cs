using jotfi.Jot.Base.System;
using jotfi.Jot.Data.Base;
using jotfi.Jot.Data.System;
using jotfi.Jot.Database;

namespace jotfi.Jot.Data
{
    public class RepositoryFactory : Logger
    {
        public readonly BaseFactory Base;
        public readonly SystemFactory System;
        public readonly DatabaseController Db;

        public RepositoryFactory(DatabaseController db, LogOpts opts = null) : base(opts)
        {
            Db = db;
            Base = new BaseFactory(this);
            System = new SystemFactory(this);
        }

    }
}
