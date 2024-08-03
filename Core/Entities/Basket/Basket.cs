using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Basket
{
    public class Basket : BaseEntity
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<BasketItem> Items { get; set; }
    }
}
