using System.Data.Common;

namespace jotfi.Jot.Database.Base
{
    public interface IDbContext
    {
        UnitOfWork Create();
        DbConnection GetConnection();
    }
}