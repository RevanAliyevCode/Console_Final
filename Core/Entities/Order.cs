using Core.Entities.Base;
using B = Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Concrets.Enums;

namespace Core.Entities
{
    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public Status Status { get; set; }

        public int SellerId { get; set; }
        public Seller Seller { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } 
        

    }
}
