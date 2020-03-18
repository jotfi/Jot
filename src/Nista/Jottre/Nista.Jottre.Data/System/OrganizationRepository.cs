using Dapper;
using Nista.Jottre.Base.System;
using Nista.Jottre.Data.Base;
using Nista.Jottre.Model.System;

namespace Nista.Jottre.Data.System
{
    public class OrganizationRepository : BaseRepository<Organization>
    {
        public OrganizationRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}
