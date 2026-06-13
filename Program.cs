using BookStoreConsole.Data;
using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Dto;
using BookStoreConsole.Services.FactoryDP;
using BookStoreConsole.Services.OrderService;
using BookStoreConsole.Services.OrderServiceImp;

namespace BookStoreConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BookStore Setup & Architecture Testing ===");

            // Singleton Repository
            var bookRepo = InMemoryRepository<Book>.Instance;
            var customerRepo = InMemoryRepository<Customer>.Instance;

            // Factory Pattern to create books
            var bookFactory = new BookFactory();

            var book1 = bookFactory.CreateBook(new CreateBookDto
            {
                Format = "paperback",
                Title = "Clean Architecture in .NET",
                Author = "Robert C. Martin",
                Category = "Technology",
                Price = 350.00m,
                Stock = 2 
            });

            var book2 = bookFactory.CreateBook(new CreateBookDto
            {
                Format = "ebook",
                Title = "Utopia",
                Author = "Ahmed Khaled Tawfik",
                Category = "Novels",
                Price = 120.00m,
                Stock = 10
            });

            // adding books to the repository
            bookRepo.Add(book1);
            bookRepo.Add(book2);
        }
    }
}
