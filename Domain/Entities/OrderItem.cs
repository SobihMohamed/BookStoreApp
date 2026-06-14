using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
