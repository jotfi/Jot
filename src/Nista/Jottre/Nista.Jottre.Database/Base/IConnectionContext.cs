using System.Data.Common;

namespace Nista.Jottre.Database.Base
{
    public interface IConnectionContext
    {
        DbConnection GetConnection();
    }
}