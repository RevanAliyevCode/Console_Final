using Data.Contexts;
using Data.Repository.User;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Customer
{
    public class CustomerRepo : UserRepo<E.Customer>, ICustomerRepo
    {
        readonly CommerseDbContext _context;
        public CustomerRepo(CommerseDbContext context) : base(context)
        {
            _context = context;
        }

        public E.Customer? GetCustomerWithBasket(int id)
        {
            return _context.Customers.Include(c => c.Baskets).FirstOrDefault(c => c.Id == id);
        }

        public E.Customer? GetCustomerWithBasketAndOrder(int id)
        {
            return _context.Customers.Include(c => c.Baskets).Include(c => c.Orders).FirstOrDefault(c => c.Id == id);
        }

        public E.Customer? GetCustomerWithOrder(int id)
        {
            return _context.Customers.Include(c => c.Orders.OrderByDescending(o => o.CreatedDate)).ThenInclude(o => o.Product).Include(c => c.Orders).ThenInclude(o => o.Seller).FirstOrDefault(c => c.Id == id);
        }
    }
}
