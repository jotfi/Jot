using Dapper;
using Nista.Jottre.Data.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nista.Jottre.Data.System
{
    public class OrganizationRepository : BaseRepository
    {
        public OrganizationRepository(IConnectionContext context) : base(context)
        {

        }

        public async Task<Organization> GetOrDefaultAsync(int id)
        {
            return await Context.GetConnection().QuerySingleOrDefaultAsync<Organization>(
                @"
select * from Entity where Id = @id
", new { id });
        }
    }
}
