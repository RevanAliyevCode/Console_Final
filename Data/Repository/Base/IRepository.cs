using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
        T? Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
    }
}
