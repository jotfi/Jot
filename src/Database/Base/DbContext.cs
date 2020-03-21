using MySql.Data.MySqlClient;
using jotfi.Jot.Base.System;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace jotfi.Jot.Database.Base
{
    public class DbContext : Logger, IDbContext
    {
        private readonly DatabaseController Db;
        private readonly DbConnection DbConnection;
        private UnitOfWork UnitOfWork;        

        private bool IsUnitOfWorkOpen => !(UnitOfWork == null || UnitOfWork.IsDisposed);

        public DbContext(DatabaseController db, LogOpts opts = null) : base(opts)
        {
            try
            {
                Db = db;
                if (Db.Dialect == DapperExt.Dialects.PostgreSQL)
                {
                    DbConnection = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "JotDb"));
                    DapperExt.SetDialect(DapperExt.Dialects.PostgreSQL);
                }
                else if (Db.Dialect == DapperExt.Dialects.SQLite)
                {
                    var builder = new SQLiteConnectionStringBuilder
                    {
                        DataSource = "./Jot.db"                         
                    };
                    DbConnection = new SQLiteConnection(builder.ConnectionString);                    
                    DapperExt.SetDialect(DapperExt.Dialects.SQLite);
                }
                else if (Db.Dialect == DapperExt.Dialects.MySQL)
                {
                    DbConnection = new MySqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "3306", "root", "admin", "JotDb"));
                    DapperExt.SetDialect(DapperExt.Dialects.MySQL);
                }
                else
                {
                    DbConnection = new SqlConnection(@"Data Source = .\sqlexpress;Initial Catalog=JotDb;Integrated Security=True;MultipleActiveResultSets=true;");
                    DapperExt.SetDialect(DapperExt.Dialects.SQLServer);
                }
                DbConnection.Open();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
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
            UnitOfWork = new UnitOfWork(DbConnection);
            return UnitOfWork;
        }
    }
}
