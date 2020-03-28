// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
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
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AddressMap());
                config.ForDommel();
            });
        }

        //public bool CheckTables(List<TableName> tableNames, out string error)
        //{
        //    error = string.Empty;
        //    using var uow = Context.Create();
        //    foreach (var table in Models)
        //    {
        //        try
        //        {
        //            if (!tableNames.Any(p => p.Name == table.TableName()))
        //            {
        //                Context.GetConnection().Execute(table.CreateTable(Dialect));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log(ex, table.CreateTable());
        //            error = $"Exception during create table: {table.GetType().Name}. Check log for details.";
        //            return false;
        //        }
        //    }
        //    uow.CommitAsync().Wait();
        //    return true;
        //}
    }
}
