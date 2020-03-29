using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
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
                DbConnectionTypes.SQLite => new SQLiteConnection(settings.ConnectionString),
                DbConnectionTypes.SQLServer => new SqlConnection(settings.ConnectionString),
                DbConnectionTypes.PostgreSQL => new NpgsqlConnection(settings.ConnectionString),
                DbConnectionTypes.MySQL => new MySqlConnection(settings.ConnectionString),
                _ => null
            };
        }

        public static string CreateConnectionString(DbSettings settings)
        {
            var connectionType = (DbConnectionTypes)settings.ConnectionType;
            return connectionType switch
            {
                DbConnectionTypes.SQLite => SQLiteConnectionString(settings.Directory, settings.Name),
                DbConnectionTypes.SQLServer => SQLConnectionString(settings),
                DbConnectionTypes.PostgreSQL => PostgreSQLConnectionString(settings),
                DbConnectionTypes.MySQL => MySQLConnectionString(settings),
                _ => string.Empty
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
