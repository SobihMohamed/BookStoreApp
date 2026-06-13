using BookStoreConsole.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.Strategies
{
    public interface IBookRuleStrategy
    {
        void ApplyRule(Book book);
    }
}
