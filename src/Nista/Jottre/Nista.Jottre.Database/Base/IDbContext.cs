using System.Data.Common;

namespace Nista.Jottre.Database.Base
{
    public interface IDbContext
    {
        UnitOfWork Create();
        DbConnection GetConnection();
    }
}