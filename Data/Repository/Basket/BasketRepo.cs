
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Basket
{
    public class BasketRepo : Repository<E.Basket.Basket>, IBasketRepo
    {
        readonly CommerseDbContext _context;
        public BasketRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        public E.Basket.Basket? GetBasketWithBasketItem(int id)
        {
            return _context.Baskets.Include(b => b.Items).ThenInclude(i => i.Product).FirstOrDefault(b => b.Id == id);
        }
    }
}
