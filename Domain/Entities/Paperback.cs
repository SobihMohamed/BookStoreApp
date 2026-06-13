using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Domain.Entities
{
    public class Paperback : Book
    {
        public override string GetBookType() =>"Paperback";
    }
}
