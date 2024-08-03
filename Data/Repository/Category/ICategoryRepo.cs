 namespace Data.Repository.Category
{
    public interface ICategoryRepo : IRepository<E.Category>
    {
        E.Category? GetWithProducts(int id);
    }
}
