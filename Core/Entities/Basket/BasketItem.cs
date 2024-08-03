using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Basket
{
    public class BasketItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }

        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
