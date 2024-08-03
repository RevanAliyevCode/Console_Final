using Data.Repository.User;

namespace Data.Repository.Customer
{
    public interface ICustomerRepo : IUserRepo<E.Customer>
    {
        E.Customer? GetCustomerWithBasket(int id);
        E.Customer? GetCustomerWithOrder(int id);
        E.Customer? GetCustomerWithBasketAndOrder(int id);
    }
}
