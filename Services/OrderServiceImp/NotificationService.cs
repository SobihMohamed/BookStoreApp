using BookStoreConsole.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.OrderServiceImp
{
    public class NotificationService
    {
        public void HandleOutOfStockAlert(Book book)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Set the console text color to red for the alert message
            Console.WriteLine($"\n[SYSTEM ALERT] The book '{book.Title}' by {book.Author} is officially OUT OF STOCK! Please reorder.");
            Console.ResetColor();
        }
    }
}
