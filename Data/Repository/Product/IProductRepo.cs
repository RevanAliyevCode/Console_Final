 namespace Data.Repository.Product
{
    public interface IProductRepo : IRepository<E.Product>
    {
        E.Product? GetProductWithCategoryAndSeller(int id);
    }
}
