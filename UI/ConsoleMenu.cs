using BookStoreConsole.Data;
using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Dto;
using BookStoreConsole.Extensions;
using BookStoreConsole.Services.AnalyticsServiceImp;
using BookStoreConsole.Services.FactoryDP;
using BookStoreConsole.Services.FileStorageImp;
using BookStoreConsole.Services.OrderService;
using BookStoreConsole.Services.OrderServiceImp;

namespace BookStoreConsole.UI
{
    public class ConsoleMenu
    {
        private readonly InMemoryRepository<Book> _bookRepo;
        private readonly InMemoryRepository<Customer> _customerRepo; 
        private readonly BookFactory _bookFactory;
        private readonly FileStorageService _storageService;
        private readonly AnalyticsService _analyticsService;

        private readonly OrderService _orderService;
        private readonly NotificationService _notificationService;

        public ConsoleMenu()
        {
            _bookRepo = InMemoryRepository<Book>.Instance;
            _customerRepo = InMemoryRepository<Customer>.Instance;
            _bookFactory = new BookFactory();
            _storageService = FileStorageService.Instance;
            _analyticsService = new AnalyticsService();

            _orderService = new OrderService();
            _notificationService = new NotificationService();
            // Subscribe the notification service to the BookOutOfStock event
            _orderService.BookOutOfStock += _notificationService.HandleOutOfStockAlert;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Loading data from system...");
            var loadedBooks = await _storageService.LoadDataAsync<Book>("books.json");
            foreach (var book in loadedBooks) _bookRepo.Add(book);

            var loadedOrders = await _storageService.LoadDataAsync<Order>("orders.json");
            var orderRepo = InMemoryRepository<Order>.Instance;
            foreach (var order in loadedOrders) orderRepo.Add(order);

            InputValidator.ShowSuccess("Data loaded successfully!\n");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=================================");
                Console.WriteLine("       Book Store      ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Add a New Book");
                Console.WriteLine("2. List All Books");
                Console.WriteLine("3. Record a Purchase (Observer Test)");
                Console.WriteLine("4. Remove a Book (Soft Delete)");
                Console.WriteLine("5. Search & Filter Books");
                Console.WriteLine("6. System Analytics (Revenue & Top Sellers)");
                Console.WriteLine("7. Save & Exit");
                Console.WriteLine("=================================");

                int choice = InputValidator.GetInt("Enter your choice (1-7): ");

                switch (choice)
                {
                    case 1:
                        AddNewBook();
                        break;
                    case 2:
                        ListBooks();
                        break;
                    case 3:
                        RecordPurchase(); 
                        break;
                    case 4:
                        RemoveBook();
                        break;
                    case 5:
                        SearchBooks();
                        break;
                    case 6:
                        ShowAnalytics();
                        break;
                    case 7:
                        await _storageService.SaveDataAsync(_bookRepo.GetAll(includeDeleted: true), "books.json");

                        var orderRepository = InMemoryRepository<Order>.Instance;
                        await _storageService.SaveDataAsync(orderRepository.GetAll(), "orders.json");

                        InputValidator.ShowSuccess("Data saved successfully. Goodbye!");
                        exit = true;
                        break;
                }
            }
        }

        private void AddNewBook()
        {
            Console.WriteLine("\n--- Add New Book ---");
            string format = InputValidator.GetString("Enter Book Format (paperback/ebook): ");
            string title = InputValidator.GetString("Enter Title: ");
            string author = InputValidator.GetString("Enter Author: ");
            string category = InputValidator.GetString("Enter Category: ");
            decimal price = InputValidator.GetDecimal("Enter Price: ");
            int stock = InputValidator.GetInt("Enter Stock Quantity: ");

            try
            {
                var bookDto = new CreateBookDto
                {
                    Format = format,
                    Title = title,
                    Author = author,
                    Category = category,
                    Price = price,
                    Stock = stock
                };

                var newBook = _bookFactory.CreateBook(bookDto);
                _bookRepo.Add(newBook);

                InputValidator.ShowSuccess($"Book '{title}' added successfully!\n");
            }
            catch (Exception ex)
            {
                InputValidator.ShowError(ex.Message);
            }
        }

        private void ListBooks()
        {
            Console.WriteLine("\n--- Available Books ---");
            var books = _bookRepo.GetAll();

            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id} | Title: {book.Title} | Author: {book.Author} | Type: {book.GetBookType()} | Price: {book.Price} EGP | Stock: {book.Stock}");
            }
            Console.WriteLine("-----------------------\n");
        }

        private void RecordPurchase()
        {
            Console.WriteLine("\n--- Record a Purchase ---");

            // add customer first
            string customerName = InputValidator.GetString("Enter Customer Full Name: ");
            string customerEmail = InputValidator.GetString("Enter Customer Email: ");

            var customer = new Customer { FullName = customerName, Email = customerEmail };
            _customerRepo.Add(customer);

            // choose book to buy
            int bookId = InputValidator.GetInt("Enter the Book ID to purchase: ");
            var book = _bookRepo.GetById(bookId);

            if (book == null)
            {
                InputValidator.ShowError("Book not found!");
                return;
            }

            int quantity = InputValidator.GetInt($"Enter quantity to buy (Available: {book.Stock}): ");

            // create a dictionary of items to buy (in case we want to extend to multiple items in the future)
            var itemsToBuy = new Dictionary<Book, int>
            {
                { book, quantity }
            };

            try
            {
                // create order and process it, this will trigger the BookOutOfStock event if stock is insufficient
                _orderService.ProcessOrder(customer, itemsToBuy);
                InputValidator.ShowSuccess($"Purchase recorded successfully for {customerName}!");
            }
            catch (Exception ex)
            {
                InputValidator.ShowError(ex.Message);
            }
        }

        private void RemoveBook()
        {
            Console.WriteLine("\n--- Remove a Book ---");
            int bookId = InputValidator.GetInt("Enter the Book ID to remove: ");
            var book = _bookRepo.GetById(bookId);

            if (book == null)
            {
                InputValidator.ShowError("Book not found!");
                return;
            }
            _bookRepo.Remove(book);
            InputValidator.ShowSuccess($"Book '{book.Title}' removed (Soft Deleted) successfully!");
        }

        private void SearchBooks()
        {
            Console.WriteLine("\n--- Search & Filter Books ---");
            Console.WriteLine("(Press Enter to skip any filter)");

            Console.Write("Enter Category: ");
            string category = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            var filteredBooks = _analyticsService.FilterBooks(
                string.IsNullOrWhiteSpace(category) ? null : category,
                string.IsNullOrWhiteSpace(author) ? null : author
            );

            Console.WriteLine("\n--- Search Results ---");
            if (!filteredBooks.Any())
            {
                Console.WriteLine("No books found matching your criteria.");
            }
            else
            {
                foreach (var book in filteredBooks)
                {
                    Console.WriteLine($"ID: {book.Id} | Title: {book.Title} | Author: {book.Author} | Price: {book.Price} EGP");
                }
            }
        }

        private void ShowAnalytics()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("       SYSTEM ANALYTICS          ");
            Console.WriteLine("=================================");

            decimal totalRevenue = _analyticsService.GetTotalRevenue();
            Console.WriteLine($"Total Revenue: {totalRevenue.ToEGP()}");

            var bestBook = _analyticsService.GetBestSellingBook();
            if (bestBook != null)
                Console.WriteLine($"Best Selling Book: {bestBook}");
            else
                Console.WriteLine("Best Selling Book: No sales yet.");

            var topCustomer = _analyticsService.GetTopCustomer();
            if (topCustomer != null)
                Console.WriteLine($"Top Customer: {topCustomer.FullName} ({topCustomer.Email})");
            else
                Console.WriteLine("Top Customer: No customers yet.");

            Console.WriteLine("=================================\n");
        }
    }
}