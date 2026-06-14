using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.StrategyDP.Strategies
{
    public class PercentageDiscountStrategy : IBookRuleStrategy
    {
        private readonly decimal _percentage;
        public PercentageDiscountStrategy(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100.");
            _percentage = percentage;
        }

        public void ApplyRule(Book book)
        {
            book.Price -= book.Price * (_percentage / 100);
        }
    }
}
