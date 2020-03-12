using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Nista.Jottre.Database
{
    public class SqliteDatabase : Database
    {
        public override void Open()
        {
            base.Open();
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "./SqliteDB.db"
            };
            try
            {
                DbConnection = new SqliteConnection(connectionStringBuilder.ConnectionString);
                DbConnection.Open();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public override void Setup()
        {
            base.Setup();
            foreach (var table in Model.Model.CreateTables)
            {
                DbConnection.Execute(table);
            }
        }
    }
}
