using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }

        public Dictionary<Book, int> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }
    }
}
