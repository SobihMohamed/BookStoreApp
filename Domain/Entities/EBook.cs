using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Domain.Entities
{
    public class EBook : Book
    {
        public double FileSizeMB { get; set; }
        public override string GetBookType() => "E-Book";
    }
}
