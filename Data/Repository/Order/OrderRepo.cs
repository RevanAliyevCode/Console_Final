
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Order
{
    public class OrderRepo : Repository<E.Order>, IOrderRepo
    {
        readonly CommerseDbContext _context;
        public OrderRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<E.Order> GetByDate(DateTime date)
        {
            return _context.Orders.Include(o => o.Product).Include(o => o.Seller).Include(o => o.Customer).Where(o => o.CreatedDate.Date == date.Date);
        }

        public IQueryable<E.Order> GetWithSellerAndCustomer()
        {
            return _context.Orders.Include(o => o.Customer).Include(o => o.Seller).Include(o => o.Product).OrderByDescending(o => o.CreatedDate);
        }
    }
}
