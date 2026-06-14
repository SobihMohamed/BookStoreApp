using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.StrategyDP.Strategies
{
    public class VatTaxStrategy : IBookRuleStrategy
    {
        private readonly decimal _taxRate = 0.14m;

        public void ApplyRule(Book book)
        {
            book.Price += book.Price * _taxRate;
        }
    }
}
