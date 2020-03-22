using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Data.System
{
    public class SystemRepositories : Logger
    {
        public readonly RepositoryController Data;
        public readonly OrganizationRepository Organization;
        public readonly TableNameRepository TableName;
        public readonly UserRepository User;

        public SystemRepositories(RepositoryController data, LogOpts opts = null) : base(opts)
        {
            Data = data;
            Organization = new OrganizationRepository(data, opts);
            TableName = new TableNameRepository(data, opts);
            User = new UserRepository(data, opts);
        }
    }
}
