using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Data.System
{
    public class SystemFactory : Logger
    {
        public readonly RepositoryFactory Data;
        public readonly OrganizationRepository Organization;
        public readonly TableNameRepository TableName;
        public readonly UserRepository User;

        public SystemFactory(RepositoryFactory data, LogOpts opts = null) : base(opts)
        {
            Data = data;
            Organization = new OrganizationRepository(data, opts);
            TableName = new TableNameRepository(data, opts);
            User = new UserRepository(data, opts);
        }
    }
}
