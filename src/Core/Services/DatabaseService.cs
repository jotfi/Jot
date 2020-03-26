using Dapper;
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Base;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jotfi.Jot.Core.Services
{
    //TODO: potentially abstract Database to not include Dapper, could use MongoDB in future, etc.
    
    public class DatabaseService : Logger
    {
        public readonly Application App;
        public readonly DbDialects Dialect;        
        public readonly IDbContext Context;
        public readonly List<ITransaction> Models;

        public DatabaseService(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Dialect = (DbDialects)app.AppSettings.DbDialect;
            Context = new DbContext(Dialect, app.AppSettings.DbDirectory, opts);
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

        public bool CheckTables(List<TableName> tableNames, out string error)
        {
            error = string.Empty;
            using var uow = Context.Create();
            foreach (var table in Models)
            {
                try
                {
                    if (!tableNames.Any(p => p.Name == table.TableName()))
                    {
                        Context.GetConnection().Execute(table.CreateTable(Dialect));
                    }
                }
                catch (Exception ex)
                {
                    Log(ex, table.CreateTable());
                    error = $"Exception during create table: {table.GetType().Name}. Check log for details.";
                    return false;
                }
            }
            uow.CommitAsync().Wait();
            return true;
        }
    }
}
