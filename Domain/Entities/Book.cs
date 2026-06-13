using BookStoreConsole.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BookStoreConsole.Domain.Entities
{
    // to know how to deserialize the correct type of book, we use JsonDerivedType attribute to specify the derived types and their discriminators
    // This allows the JSON serializer to correctly identify and instantiate the appropriate subclass (Paperback or EBook) when deserializing JSON data that represents a Book object.
    [JsonDerivedType(typeof(Paperback), typeDiscriminator: "Paperback")]
    [JsonDerivedType(typeof(EBook), typeDiscriminator: "EBook")]
    public abstract class Book : ISoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; } = false;

        public abstract string GetBookType();
    }
}
