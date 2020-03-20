using Dapper;
using johncocom.Jot.Base;
using johncocom.Jot.Base.System;
using johncocom.Jot.Database.Base;
using johncocom.Jot.Model;
using johncocom.Jot.Model.Base;
using johncocom.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace johncocom.Jot.Database
{
    //TODO: potentially abstract Database to not include Dapper, could use MongoDB in future, etc.
    
    public class DatabaseController : Logger
    {
        public DapperExt.Dialects Dialect;
        public readonly IDbContext Context;
        public readonly List<ITransaction> Models;

        public DatabaseController(LogOpts opts = null) : base(opts)
        {
            //Todo: get from settings
            Dialect = DapperExt.Dialects.SQLite;
            Context = new DbContext(this, opts);
            Models = new List<ITransaction>()
            {
                new User(),
                new Person(),
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
