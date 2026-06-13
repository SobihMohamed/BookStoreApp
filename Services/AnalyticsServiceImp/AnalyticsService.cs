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
        public Book GetBestSellingBook()
        {
            var allItems = _orderRepo.GetAll().SelectMany(o => o.Items);

            var bestSeller = allItems
                .GroupBy(item => item.Key)
                .OrderByDescending(g => g.Sum(item => item.Value))
                .Select(g => g.Key)
                .FirstOrDefault();

            return bestSeller;
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

        //Filter books by category, author, and price range
        public IEnumerable<Book> FilterBooks(string category = null, string author = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = _orderRepo.GetAll().SelectMany(o => o.Items).Select(i => i.Key).Distinct().AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(b => b.Category.Equals(category,StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Equals(author,StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                query = query.Where(b => b.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(b => b.Price <= maxPrice.Value);

            return query.ToList();
        }
    }
}