using johncocom.Jot.Base.System;
using johncocom.Jot.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Data.System
{
    public class SystemRepositories : Logger
    {
        public readonly RepositoryController Data;
        public readonly TableNameRepository TableNames;
        public readonly UserRepository Users;
        public readonly OrganizationRepository Organizations;

        public SystemRepositories(RepositoryController data, LogOpts opts = null) : base(opts)
        {
            Data = data;
            TableNames = new TableNameRepository(data, opts);
            Users = new UserRepository(data, opts);
            Organizations = new OrganizationRepository(data, opts);
        }

    }

}
