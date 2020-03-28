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

using MySql.Data.MySqlClient;
using jotfi.Jot.Base.System;
using Npgsql;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Base
{
    public class UnitOfWork : Logger, IDisposable
    {
        private readonly DbTransaction DbTransaction;
        public DbConnection DbConnection { get; }
        public bool IsDisposed { get; private set; } = false;

        public UnitOfWork(DbDialects dbDialect, string dbDirectory, LogOpts opts = null) : base(opts)
        {
            try
            {
                if (dbDialect == DbDialects.PostgreSQL)
                {
                    DbConnection = new NpgsqlConnection(string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "JotDb"));
                    DapperExt.SetDialect(DbDialects.PostgreSQL);
                }
                else if (dbDialect == DbDialects.SQLite)
                {
                    var builder = new SQLiteConnectionStringBuilder
                    {
                        DataSource = Path.Combine(dbDirectory, "Jot.db")
                    };
                    DbConnection = new SQLiteConnection(builder.ConnectionString);
                    DapperExt.SetDialect(DbDialects.SQLite);
                }
                else if (dbDialect == DbDialects.MySQL)
                {
                    DbConnection = new MySqlConnection(string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "3306", "root", "admin", "JotDb"));
                    DapperExt.SetDialect(DbDialects.MySQL);
                }
                else
                {
                    DbConnection = new SqlConnection(@"Data Source = .\sqlexpress;Initial Catalog=JotDb;Integrated Security=True;MultipleActiveResultSets=true;");
                    DapperExt.SetDialect(DbDialects.SQLServer);
                }
                DbConnection.Open();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            DbTransaction = DbConnection.BeginTransaction();
        }

        public async Task RollBackAsync()
        {
            await DbTransaction.RollbackAsync();
        }

        public async Task CommitAsync()
        {
            await DbTransaction.CommitAsync();
        }

        public void Dispose()
        {
            DbTransaction?.Dispose();
            IsDisposed = true;
        }
    }
}
