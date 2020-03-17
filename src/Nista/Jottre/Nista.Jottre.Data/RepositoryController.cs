using Nista.Jottre.Data.System;
using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data
{
    public class RepositoryController
    {
        public readonly UserRepository Users;
        public readonly OrganizationRepository Organizations;

        public RepositoryController(IConnectionContext context)
        {
            Organizations = new OrganizationRepository(context);
        }
    }
}
