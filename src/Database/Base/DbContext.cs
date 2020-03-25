//#define RESET

using jotfi.Jot.Base.System;
using System;
using System.Data.Common;
using System.IO;

namespace jotfi.Jot.Database.Base
{
    public class DbContext : Logger, IDbContext
    {
        private readonly DatabaseController Db;
        private UnitOfWork UnitOfWork;
        private readonly string SQLiteDirectory;

        private bool IsUnitOfWorkOpen => !(UnitOfWork == null || UnitOfWork.IsDisposed);

        public DbContext(DatabaseController db, LogOpts opts = null) : base(opts)
        {
            Db = db;
            SQLiteDirectory = db.Settings.DbDirectory;
            if (string.IsNullOrEmpty(SQLiteDirectory))
            {
                SQLiteDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                SQLiteDirectory = Path.Combine(SQLiteDirectory, "jotfi");
            }
            if (!Directory.Exists(SQLiteDirectory))
            {
                Directory.CreateDirectory(SQLiteDirectory);
            }
#if (RESET)
            File.Delete(Path.Combine(SQLiteDirectory, "Jot.db"));
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
            UnitOfWork = new UnitOfWork(Db, SQLiteDirectory, Opts);
            return UnitOfWork;
        }


    }
}
