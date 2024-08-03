using Data.Repository.User;

namespace Data.Repository.Seller
{
    public interface ISellerRepo : IUserRepo<E.Seller>
    {
        IQueryable<E.Seller> GetNotVerified();
        E.Seller? GetWithProducts(int id);
        E.Seller? GetWithOrders(int id);
    }
}
