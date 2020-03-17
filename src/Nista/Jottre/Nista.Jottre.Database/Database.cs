using Dapper;
using Nista.Jottre.Base;
using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Nista.Jottre.Database
{
    //TODO: potentially abstract Database to not include Dapper, could use MongoDB in future, etc.
    
    public class Database : Logger
    {
        protected UnitOfWorkContext Context;

        public Database(SimpleCRUD.Dialects dbType)
        {
            Context = new UnitOfWorkContext(dbType);
        }

        public void Setup()
        {
            using (Context.Create())
            {
                foreach (var table in Model.Model.CreateTables)
                {
                    Context.GetConnection().Execute(table);
                }
            }
        }
    }
}
