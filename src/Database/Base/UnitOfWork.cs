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

        public UnitOfWork(DatabaseController db, string sqliteDirectory, LogOpts opts = null) : base(opts)
        {
            try
            {
                if (db.Dialect == DbDialects.PostgreSQL)
                {
                    DbConnection = new NpgsqlConnection(string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "JotDb"));
                    DapperExt.SetDialect(DbDialects.PostgreSQL);
                }
                else if (db.Dialect == DbDialects.SQLite)
                {
                    var builder = new SQLiteConnectionStringBuilder
                    {
                        DataSource = Path.Combine(sqliteDirectory, "Jot.db")
                    };
                    DbConnection = new SQLiteConnection(builder.ConnectionString);
                    DapperExt.SetDialect(DbDialects.SQLite);
                }
                else if (db.Dialect == DbDialects.MySQL)
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
