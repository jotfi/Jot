using jotfi.Jot.Base.System;
using jotfi.Jot.Data.Base;
using jotfi.Jot.Data.System;
using jotfi.Jot.Database.Base;

namespace jotfi.Jot.Data
{
    public class RepositoryFactory : Logger
    {
        public readonly BaseFactory Base;
        public readonly SystemRepositories System;
        public readonly IDbContext Context;

        public RepositoryFactory(IDbContext context, LogOpts opts = null) : base(opts)
        {
            Context = context;
            Base = new BaseFactory(this);
            System = new SystemRepositories(this);
        }

    }
}
