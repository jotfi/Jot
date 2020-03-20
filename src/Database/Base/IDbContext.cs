using System.Data.Common;

namespace johncocom.Jot.Database.Base
{
    public interface IDbContext
    {
        UnitOfWork Create();
        DbConnection GetConnection();
    }
}