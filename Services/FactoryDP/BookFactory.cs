using BookStoreConsole.Domain.Entities;
using BookStoreConsole.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreConsole.Services.FactoryDP
{
    public class BookFactory
    {
        private Dictionary<string, Func<Book>> _bookCreators = new(StringComparer.OrdinalIgnoreCase);
        public BookFactory()
        {
            RegisterBookType("Paperback", () => new Paperback());
            RegisterBookType("EBook", () => new EBook());
        }
        public void RegisterBookType(string bookType, Func<Book> creator)
        {
            _bookCreators[bookType] = creator;
        }
        public Book CreateBook(CreateBookDto dto)
        {
            ValidateBookDto(dto);

            // Creation Logic
            if (_bookCreators.TryGetValue(dto.Format, out var creator))
            {
                var book = creator();
                book.Title = dto.Title;
                book.Author = dto.Author;
                book.Category = dto.Category;
                book.Price = dto.Price;
                book.Stock = dto.Stock;

                return book;
            }

            throw new ArgumentException($"Book format '{dto.Format}' is not supported.");
        }
        private void ValidateBookDto(CreateBookDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(dto.Author))
                throw new ArgumentException("Author cannot be empty.");
            if (string.IsNullOrWhiteSpace(dto.Category))
                throw new ArgumentException("Category cannot be empty.");
            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");
            if (dto.Stock < 0)
                throw new ArgumentException("Stock cannot be negative.");
        }
    }
}
