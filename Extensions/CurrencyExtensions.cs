using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Extensions
{
    public static class CurrencyExtensions
    {
        public static string ToEGP(this decimal amount) 
        {
            return $"{amount:N2} EGP";
        }
    }
}
