using BookStoreConsole.Data;
using BookStoreConsole.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.AnalyticsServiceImp
{
    public class AnalyticsService
    {
        private readonly InMemoryRepository<Order> _orderRepo;
        private readonly InMemoryRepository<Book> _bookRepo;

        public AnalyticsService()
        {
            // Initialize repositories Singleton instances
            _orderRepo = InMemoryRepository<Order>.Instance;
            _bookRepo = InMemoryRepository<Book>.Instance;
        }

        //Calculate total revenue
        public decimal GetTotalRevenue()
        {
            return _orderRepo.GetAll().Sum(order => order.TotalAmount);
        }

        //Best-selling book
        public string GetBestSellingBook()
        {
            var bestSellerName = _orderRepo.GetAll()
                .SelectMany(o => o.OrderItems)
                .GroupBy(item => item.BookTitle)
                .OrderByDescending(g => g.Sum(item => item.Quantity))
                .Select(g => g.Key)
                .FirstOrDefault();

            return bestSellerName ?? "No sales yet.";
        }
        //Top customer
        public Customer GetTopCustomer()
        {
            var topCustomer = _orderRepo.GetAll()
                .GroupBy(o => o.Customer)
                .OrderByDescending(g => g.Sum(o => o.TotalAmount))
                .Select(g => g.Key)
                .FirstOrDefault();

            return topCustomer;
        }

        public IEnumerable<Book> FilterBooks(string category = null, string author = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = _bookRepo.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(b => b.Category.Contains(category, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                query = query.Where(b => b.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(b => b.Price <= maxPrice.Value);

            return query.ToList();
        }
    }
}