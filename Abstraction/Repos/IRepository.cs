using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BookStoreConsole.Abstraction.Repos
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll(bool includeDeleted);
        T GetById(int id);
    }
}
