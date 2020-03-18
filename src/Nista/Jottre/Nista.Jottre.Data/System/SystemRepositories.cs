using Nista.Jottre.Base.Log;
using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data.System
{
    public class SystemRepositories : Logging
    {
        public readonly RepositoryController Data;
        public readonly TableNameRepository TableNames;
        public readonly UserRepository Users;
        public readonly OrganizationRepository Organizations;

        public SystemRepositories(RepositoryController data, 
            bool isConsole = true, Action<string> showLog = null) : base(isConsole, showLog)
        {
            Data = data;
            TableNames = new TableNameRepository(data);
            Users = new UserRepository(data);
            Organizations = new OrganizationRepository(data);
        }

    }

}
