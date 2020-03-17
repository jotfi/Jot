using Dapper;
using Nista.Jottre.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model;
using Nista.Jottre.Model.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Nista.Jottre.Database
{
    //TODO: potentially abstract Database to not include Dapper, could use MongoDB in future, etc.
    
    public class DatabaseController : Logger
    {
        private readonly IDbContext Context;
        private readonly List<ITransaction> Models;
        
        public DatabaseController(IDbContext context)
        {
            Context = context;
            Models = new List<ITransaction>()
            {
                new User(),
                new Person(),
                new Organization()
            };
        }

        public void Setup(List<TableName> tableNames)
        {
            using var uow = Context.Create();
            foreach (var table in Models)
            {
                try
                {
                    if (!tableNames.Any(p => p.Name == table.TableName()))
                    {
                        Context.GetConnection().Execute(table.CreateTable());
                    }
                }
                catch (Exception ex)
                {
                    Log(ex, table.CreateTable());
                }
            }
            uow.CommitAsync().Wait();
        }
    }
}
