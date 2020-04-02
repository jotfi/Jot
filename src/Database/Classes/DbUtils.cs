#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
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
//
#endregion

using FluentMigrator.Runner;
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Classes;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace jotfi.Jot.Base.Utils
{
    public class DbUtils
    {
        public static DbConnection CreateConnection(DbSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                settings.ConnectionString = CreateConnectionString(settings);
            }
            var connectionType = (DbConnectionTypes)settings.ConnectionType;
            return connectionType switch
            {
                DbConnectionTypes.SQLServer => new SqlConnection(settings.ConnectionString),
                DbConnectionTypes.PostgreSQL => new NpgsqlConnection(settings.ConnectionString),
                DbConnectionTypes.MySQL => new MySqlConnection(settings.ConnectionString),
                _ => new SQLiteConnection(settings.ConnectionString)
            };
        }

        public static void AddMigration(IMigrationRunnerBuilder rb, DbSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                settings.ConnectionString = CreateConnectionString(settings);
            }
            // Add Database support to FluentMigrator
            var connectionType = (DbConnectionTypes)settings.ConnectionType;
            switch (connectionType)
            {
                case DbConnectionTypes.SQLServer:
                    rb.AddSqlServer();
                    break;
                case DbConnectionTypes.PostgreSQL:
                    rb.AddPostgres();
                    break;
                case DbConnectionTypes.MySQL:
                    rb.AddMySql5();
                    break;
                default:
                    rb.AddSQLite();
                    break;
            };
            // Set the connection string
            rb.WithGlobalConnectionString(CreateConnectionString(settings));
            // Define the assembly containing the migrations
            rb.ScanIn(typeof(DbManager).Assembly).For.Migrations();
        }

        public static string CreateConnectionString(DbSettings settings)
        {
            var connectionType = (DbConnectionTypes)settings.ConnectionType;
            return connectionType switch
            {
                DbConnectionTypes.SQLServer => SQLConnectionString(settings),
                DbConnectionTypes.PostgreSQL => PostgreSQLConnectionString(settings),
                DbConnectionTypes.MySQL => MySQLConnectionString(settings),
                _ => SQLiteConnectionString(settings.Directory, settings.Name)
            };
        }

        static string SQLiteConnectionString(string directory, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = $"{Constants.DefaultApplicationName}.db";
            }
            if (string.IsNullOrEmpty(directory))
            {
                directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                directory = Path.Combine(directory, Constants.DefaultBrandName);
            }
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
#if (RESET)
            File.Delete(Path.Combine(directory, name));
#endif
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = Path.Combine(directory, name)
            };
            return builder.ConnectionString;
        }

        static string SQLConnectionString(DbSettings settings)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = settings.Server,
                InitialCatalog = settings.Name,
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };
            return builder.ConnectionString;
        }

        static string PostgreSQLConnectionString(DbSettings settings)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = settings.Server,
                Port = settings.Port,
                Username = settings.UserName,
                Password = settings.Password,
                Database = settings.Name
            };
            return builder.ConnectionString;
        }

        static string MySQLConnectionString(DbSettings settings)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = settings.Server,
                Port = (uint)settings.Port,
                UserID = settings.UserName,
                Password = settings.Password,
                Database = settings.Name
            };
            return builder.ConnectionString;
        }
    }
}
