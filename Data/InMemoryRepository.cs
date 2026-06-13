using BookStoreConsole.Abstraction;
using BookStoreConsole.Abstraction.Repos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        // Thread-Safe Singleton Implementation Lazy
        private static readonly Lazy<InMemoryRepository<T>> _instance =
            new Lazy<InMemoryRepository<T>>(() => new InMemoryRepository<T>());

        // 2. Public Access Point
        public static InMemoryRepository<T> Instance => _instance.Value;

        // 3. Private Constructor 
        private InMemoryRepository()
        {
        }

        // Using a ConcurrentDictionary to store entities in memory for thread-safe operations
        private readonly ConcurrentDictionary<int, T> _storage = new();
        private int _currentId = 0;
        public void Add(T entity)
        {
            // Generate a new unique ID for the entity and save for thread-safe operations
            int newId = Interlocked.Increment(ref _currentId);
            var idProperty = typeof(T).GetProperty("Id"); // used Reflection to ge the Id property of the entity
            if(idProperty != null && idProperty.CanWrite) // canwrite => check if the property can be set
            {
                idProperty.SetValue(entity, newId);
            }
            _storage[newId] = entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _storage.Values
                           .Where(e => !(e is ISoftDelete sd) || sd.IsDeleted == false)// if isSoftDelete is implemented then check if IsDeleted is false, otherwise return all entities
                           .ToList();

        }

        public T GetById(int id)
        {
            _storage.TryGetValue(id, out T entity);
            return entity;
        }

        public void Remove(T entity)
        {
            // Check if the entity implements ISoftDelete then assign it to the variable softDeletableEntity
            if (entity is ISoftDelete softDeletableEntity) 
            {
                softDeletableEntity.IsDeleted = true;
            }
            else
            {
                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty != null)
                {
                    int id = (int)idProperty.GetValue(entity)!;
                    _storage.TryRemove(id, out _);
                }
            }
        }
    }
}
