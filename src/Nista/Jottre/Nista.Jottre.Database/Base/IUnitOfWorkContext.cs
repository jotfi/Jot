namespace Nista.Jottre.Database.Base
{
    public interface IUnitOfWorkContext
    {
        UnitOfWork Create();
    }
}