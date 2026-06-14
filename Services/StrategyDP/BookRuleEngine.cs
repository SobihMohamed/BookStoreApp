using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.StrategyDP
{
    public class BookRuleEngine
    {
        public void ApplyStrategy(IEnumerable<Book> books, IBookRuleStrategy strategy)
        {
            foreach (var book in books)
            {
                strategy.ApplyRule(book);
            }
        }
    }
}
