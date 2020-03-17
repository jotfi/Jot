﻿using Dapper;
using Nista.Jottre.Data.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nista.Jottre.Data.System
{
    public class OrganizationRepository : BaseRepository<Organization>
    {
        public OrganizationRepository(IDbContext context) : base(context)
        {

        }
    }
}
