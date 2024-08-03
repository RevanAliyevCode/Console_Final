using Core.Entities.Base;
using B = Core.Entities.Basket;

namespace Core.Entities
{
    public class Customer : User
    {
        public ICollection<B.Basket> Baskets { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
