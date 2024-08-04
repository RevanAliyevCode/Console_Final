using Data.Contexts;
using Data.Repository.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Seller
{
    public class SellerRepo : UserRepo<E.Seller>, ISellerRepo
    {
        readonly CommerseDbContext _commerseDbContext;
        public SellerRepo(CommerseDbContext context) : base(context)
        {
            _commerseDbContext = context;
        }

        public IQueryable<E.Seller> GetNotVerified()
        {
            return _commerseDbContext.Sellers.Where(s => !s.IsVerified);
        }

        public E.Seller? GetWithOrders(int id)
        {
            return _commerseDbContext.Sellers.Include(s => s.Orders.OrderByDescending(o => o.CreatedDate)).ThenInclude(o => o.Customer).Include(s => s.Orders).ThenInclude(o => o.Product).FirstOrDefault(s => s.Id == id);
        }

        public E.Seller? GetWithProducts(int id)
        {
            return _commerseDbContext.Sellers.Include(s => s.Products).FirstOrDefault(s => s.Id == id);
        }
    }
}
