using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Abstraction.Repos
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
