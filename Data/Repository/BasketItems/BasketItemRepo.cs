namespace Data.Repository.BasketItems
{
    public class BasketItemRepo : Repository<E.Basket.BasketItem>, IBasketItemRepo
    {
        public BasketItemRepo(CommerseDbContext context) : base(context)
        {
        }
    }
}
