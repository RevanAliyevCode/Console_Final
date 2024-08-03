using Core.Entities.Base;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
