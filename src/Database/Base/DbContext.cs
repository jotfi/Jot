//#define RESET

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
