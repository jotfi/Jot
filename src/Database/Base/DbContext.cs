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

using jotfi.Jot.Base.System;
using System;
using System.Data.Common;
using System.IO;

namespace jotfi.Jot.Database.Base
{
    public class DbContext : Logger, IDbContext
    {
        private UnitOfWork UnitOfWork;
        private readonly DbDialects DbDialect;        
        private readonly string DbDirectory;

        private bool IsUnitOfWorkOpen => !(UnitOfWork == null || UnitOfWork.IsDisposed);

        public DbContext(DbDialects dbDialect, string dbDirectory, LogOpts opts = null) : base(opts)
        {
            DbDialect = dbDialect;
            DbDirectory = dbDirectory;
            if (string.IsNullOrEmpty(DbDirectory))
            {
                DbDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                DbDirectory = Path.Combine(DbDirectory, "jotfi");
            }
            if (!Directory.Exists(DbDirectory))
            {
                Directory.CreateDirectory(DbDirectory);
            }
#if (RESET)
            File.Delete(Path.Combine(DbDirectory, "Jot.db"));
#endif
        }


        public DbConnection GetConnection()
        {
            if (!IsUnitOfWorkOpen)
            {
                throw new InvalidOperationException(
                    "There is not current unit of work from which to get a connection. Call BeginTransaction first");
            }
            return UnitOfWork.DbConnection;
        }

        public UnitOfWork Create()
        {
            if (IsUnitOfWorkOpen)
            {
                throw new InvalidOperationException(
                    "Cannot begin a transaction before the unit of work from the last one is disposed");
            }
            UnitOfWork = new UnitOfWork(DbDialect, DbDirectory, Opts);
            return UnitOfWork;
        }


    }
}
