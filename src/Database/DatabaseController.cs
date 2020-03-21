using Dapper;
using jotfi.Jot.Base;
using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Base;
using jotfi.Jot.Model;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace jotfi.Jot.Database
{
    //TODO: potentially abstract Database to not include Dapper, could use MongoDB in future, etc.
    
    public class DatabaseController : Logger
    {
        public DapperExt.Dialects Dialect;
        public readonly IDbContext Context;
        public readonly List<ITransaction> Models;

        public DatabaseController(LogOpts opts = null) : base(opts)
        {
            //Todo: get Dialect from settings
            Dialect = DapperExt.Dialects.SQLite;
            Context = new DbContext(this, opts);
            Models = new List<ITransaction>()
            {
                new User(),
                new Person(),
                new Email(),
                new Address(),
                new Password(),
                new Organization()
            };
        }

        public bool CheckTables(List<TableName> tableNames)
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
                    return false;
                }
            }
            uow.CommitAsync().Wait();
            return true;
        }
    }
}
