using BookStoreConsole.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Domain.Entities
{
    public abstract class Book : ISoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; } = false;

        public abstract string GetBookType();
    }
}
