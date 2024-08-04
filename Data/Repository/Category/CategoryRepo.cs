
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Category
{
    public class CategoryRepo : Repository<E.Category>, ICategoryRepo
    {
        readonly CommerseDbContext _context;
        public CategoryRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        public E.Category? GetWithProducts(int id)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
        }
    }
}
