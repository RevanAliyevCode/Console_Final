namespace Data.Repository.Order
{
    public interface IOrderRepo : IRepository<E.Order>
    {
        IQueryable<E.Order> GetWithSellerAndCustomer();
        IQueryable<E.Order> GetByDate(DateTime date);
    }
}
