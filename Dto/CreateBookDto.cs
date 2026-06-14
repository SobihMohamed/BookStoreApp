using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Dto
{
    public class CreateBookDto
    {
        public string Format { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
