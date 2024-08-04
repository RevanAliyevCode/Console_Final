using Core.Concrets;
using Core.Entities.Base;
using Data.Contexts;
using Data.Repository.Admin;
using Data.Repository.Basket;
using Data.Repository.BasketItems;
using Data.Repository.Category;
using Data.Repository.Customer;
using Data.Repository.Order;
using Data.Repository.Product;
using Data.Repository.Seller;
using Data.Repository.User;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly CommerseDbContext _commerseDbContext;

        public readonly AdminRepo Admin;
        public readonly BasketRepo Basket;
        public readonly BasketItemRepo BasketItem;
        public readonly CustomerRepo Customer;
        public readonly CategoryRepo Category;
        public readonly OrderRepo Order;
        public readonly ProductRepo Product;
        public readonly SellerRepo Seller;

        public UnitOfWork()
        {
            _commerseDbContext = new CommerseDbContext();

            Admin = new(_commerseDbContext);
            Basket = new(_commerseDbContext);
            BasketItem = new(_commerseDbContext);
            Customer = new(_commerseDbContext);
            Category = new(_commerseDbContext);
            Order = new(_commerseDbContext);
            Product = new(_commerseDbContext);
            Seller = new(_commerseDbContext);
        }

        public bool Commit()
        {
            try
            {
                _commerseDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
                return false;
            }
        }

        public void Dispose()
        {
            _commerseDbContext?.Dispose();
        }
    }
}
