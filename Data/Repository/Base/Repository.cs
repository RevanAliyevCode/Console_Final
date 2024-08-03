using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly CommerseDbContext _context;

        public Repository(CommerseDbContext context)
        {
            _context = context;
        }

        DbSet<T> Table => _context.Set<T>(); 

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public T? Get(int id)
        {
            return Table.Find(id);
        }


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }

        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            Table.Add(entity);
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            Table.Update(entity);
        }

        public void Delete(T entity)
        {
             Table.Remove(entity);
        }

        public void DeleteRange(ICollection<T> entities)
        {
            Table.RemoveRange(entities);
        }
    }
}
