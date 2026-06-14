using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.StrategyDP.Strategies
{
    public class FlatAmountDiscountStrategy : IBookRuleStrategy
    {
        private readonly decimal _amount;
        public FlatAmountDiscountStrategy(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Discount amount must be a positive value.");
            _amount = amount;
        }

        public void ApplyRule(Book book)
        {
            if (book.Price > _amount)
                book.Price -= _amount;
        }
    }
}
