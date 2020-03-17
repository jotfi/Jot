using MySql.Data.MySqlClient;
using Nista.Jottre.Base;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Nista.Jottre.Database.Base
{
    public class DbContext : Logger, IDbContext
    {
        private readonly DbConnection DbConnection;
        private readonly DapperExt.Dialects DbType;
        private UnitOfWork UnitOfWork;        

        private bool IsUnitOfWorkOpen => !(UnitOfWork == null || UnitOfWork.IsDisposed);

        public DbContext(DapperExt.Dialects dbType)
        {
            try
            {
                DbType = dbType;
                if (DbType == DapperExt.Dialects.PostgreSQL)
                {
                    DbConnection = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "JottreDb"));
                    DapperExt.SetDialect(DapperExt.Dialects.PostgreSQL);
                }
                else if (DbType == DapperExt.Dialects.SQLite)
                {
                    var builder = new SQLiteConnectionStringBuilder
                    {
                        DataSource = "./Jottre.db"                         
                    };
                    DbConnection = new SQLiteConnection(builder.ConnectionString);                    
                    DapperExt.SetDialect(DapperExt.Dialects.SQLite);
                }
                else if (DbType == DapperExt.Dialects.MySQL)
                {
                    DbConnection = new MySqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "3306", "root", "admin", "JottreDb"));
                    DapperExt.SetDialect(DapperExt.Dialects.MySQL);
                }
                else
                {
                    DbConnection = new SqlConnection(@"Data Source = .\sqlexpress;Initial Catalog=JottreDb;Integrated Security=True;MultipleActiveResultSets=true;");
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
