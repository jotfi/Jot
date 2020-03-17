using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Nista.Jottre.Database.Base
{
    public class UnitOfWorkContext : IUnitOfWorkContext, IConnectionContext
    {
        private readonly DbConnection DbConnection;
        private readonly SimpleCRUD.Dialects DbType;
        private UnitOfWork UnitOfWork;        

        private bool IsUnitOfWorkOpen => !(UnitOfWork == null || UnitOfWork.IsDisposed);

        public UnitOfWorkContext(SimpleCRUD.Dialects dbType)
        {
            DbType = dbType;
            if (DbType == SimpleCRUD.Dialects.PostgreSQL)
            {
                DbConnection = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "testdb"));
                SimpleCRUD.SetDialect(SimpleCRUD.Dialects.PostgreSQL);
            }
            else if (DbType == SimpleCRUD.Dialects.SQLite)
            {
                DbConnection = new SqliteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                SimpleCRUD.SetDialect(SimpleCRUD.Dialects.SQLite);
            }
            else if (DbType == SimpleCRUD.Dialects.MySQL)
            {
                DbConnection = new MySqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "3306", "root", "admin", "testdb"));
                SimpleCRUD.SetDialect(SimpleCRUD.Dialects.MySQL);
            }
            else
            {
                DbConnection = new SqlConnection(@"Data Source = .\sqlexpress;Initial Catalog=DapperSimpleCrudTestDb;Integrated Security=True;MultipleActiveResultSets=true;");
                SimpleCRUD.SetDialect(SimpleCRUD.Dialects.SQLServer);
            }
            DbConnection.Open();
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
