using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace johncocom.Jot.Database.Base
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbTransaction DbTransaction;
        public DbConnection DbConnection { get; }

        public bool IsDisposed { get; private set; } = false;

        public UnitOfWork(DbConnection connection)
        {
            DbConnection = connection;
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
