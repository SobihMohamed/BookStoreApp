using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Abstraction
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
