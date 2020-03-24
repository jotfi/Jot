using Dapper;
using jotfi.Jot.Base.System;
using jotfi.Jot.Data.Base;
using jotfi.Jot.Model.System;

namespace jotfi.Jot.Data.System
{
    public class OrganizationRepository : BaseRepository<Organization>
    {
        public OrganizationRepository(RepositoryFactory data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}
