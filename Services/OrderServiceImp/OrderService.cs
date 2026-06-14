using BookStoreConsole.Data;
using BookStoreConsole.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.OrderService
{
    public class OrderService
    {
        // why action => Action is a delegate type that represents a method that takes a single parameter
        // and does not return a value.
        // In this case, the Action<Book> delegate is used to define an event that can be subscribed to by other parts of the application.
        // When the event is raised, it will pass a Book object as an argument to the subscribed methods.
        public event Action<Book> BookOutOfStock;
        private readonly InMemoryRepository<Order> _orderRepository;
        public OrderService()
        {
            _orderRepository = InMemoryRepository<Order>.Instance;
        }
        public void ProcessOrder(Customer customer , Dictionary<Book, int> order)
        {
            ValidateOrder(customer, order);
            decimal totalAmount = 0;
            foreach (var item in order)
            {
                var book = item.Key;
                var quantityToBuy = item.Value;

                book.Stock -= quantityToBuy;
                totalAmount += book.Price * quantityToBuy;

                // Observer Pattern 
                if (book.Stock == 0)
                    BookOutOfStock?.Invoke(book);

                var newOrder = new Order
                {
                    Customer = customer,
                    OrderDate = DateTime.Now,
                    Items = order,
                    TotalAmount = totalAmount
                };

                _orderRepository.Add(newOrder);
            }

        }
        private void ValidateOrder(Customer customer, Dictionary<Book, int> order)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            if (order == null || order.Count == 0)
                throw new ArgumentException("Order must contain at least one book.");
            foreach (var item in order)
                if (item.Key.Stock < item.Value)
                    throw new InvalidOperationException($"Not enough stock for '{item.Key.Title}'. Available: {item.Key.Stock}, Requested: {item.Value}");
        }
    }
}
