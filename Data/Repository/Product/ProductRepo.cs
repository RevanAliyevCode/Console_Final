
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Product
{
    public class ProductRepo : Repository<E.Product>, IProductRepo
    {
        readonly CommerseDbContext _context;
        public ProductRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        public E.Product? GetProductWithCategoryAndSeller(int id)
        {
            return _context.Products.Include(p => p.Category).Include(p => p.Seller).FirstOrDefault(p => p.Id == id);
        }
    }
}
