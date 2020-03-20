using Dapper;
using johncocom.Jot.Base.System;
using johncocom.Jot.Data.Base;
using johncocom.Jot.Model.System;

namespace johncocom.Jot.Data.System
{
    public class OrganizationRepository : BaseRepository<Organization>
    {
        public OrganizationRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}
