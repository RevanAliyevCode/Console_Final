namespace Data.Repository.Basket
{
    public interface IBasketRepo : IRepository<E.Basket.Basket>
    {
        E.Basket.Basket? GetBasketWithBasketItem(int id);
    }
}
