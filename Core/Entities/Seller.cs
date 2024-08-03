using Core.Entities.Base;

namespace Core.Entities
{
    public class Seller : User
    {
        public bool IsVerified { get; set; }
        public decimal TotalIncome { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
