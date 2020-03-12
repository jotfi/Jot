using System;
using System.Data.Common;

namespace Nista.Jottre.Database.Base
{
    public class UnitOfWorkContext : IUnitOfWorkContext, IConnectionContext
    {
        private readonly DbConnection DbConnecttion;
        private UnitOfWork UnitOfWork;

        private bool IsUnitOfWorkOpen => !(UnitOfWork == null || UnitOfWork.IsDisposed);

        public UnitOfWorkContext(DbConnection connection)
        {
            DbConnecttion = connection;
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
            UnitOfWork = new UnitOfWork(DbConnecttion);
            return UnitOfWork;
        }
    }
}
