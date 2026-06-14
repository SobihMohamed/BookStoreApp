# Makanak Book Store (Console Application)

A highly robust, object-oriented .NET 10 console application designed for managing a bookstore. The system is architected from scratch using Clean Architecture guidelines, strict SOLID principles, and industry-standard Design Patterns to ensure scalability and thread safety.

## 🚀 Key Features & Implemented Patterns

* **Design Patterns Implementation:**
  * **Factory Pattern:** Encapsulates the creation logic of different book formats (`Paperback`, `EBook`) cleanly.
  * **Strategy Pattern:** Implements pure OOP GoF standards (`IBookRuleStrategy`) to allow developers to inject custom business rules (e.g., Percentage Discount, Flat Discount, VAT Tax) seamlessly without altering core code.
  * **Observer Pattern:** Decouples order processing from system alerts by utilizing C# `event` and `Action` delegates to trigger out-of-stock notifications automatically.
  * **Singleton Pattern:** Enforces a single reference point for data repositories and asynchronous file I/O operations.

* **SOLID Principles Adherence:**
  * **Single Responsibility Principle (SRP):** Complete isolation between business operations (`OrderService`), persistence operations (`InMemoryRepository`), and reporting analytics (`AnalyticsService`).
  * **Open/Closed Principle (OCP):** Introducing new book formats or pricing strategies requires zero modifications to existing working classes.

* **Advanced Architecture Components:**
  * **Asynchronous Polymorphic Persistency:** Utilizes .NET 10 `[JsonDerivedType]` serialization attributes inside `FileStorageService` to flawlessly save and reload abstract hierarchies (`Book` implementations) and transaction logs (`orders.json`) asynchronously.
  * **Thread-Safe Memory Management:** Implements high-performance concurrent architectures using `ConcurrentDictionary` and atomic CPU operations via `Interlocked.Increment` for entity ID generation, fully capable of executing multi-threaded safe purchases.
  * **Complex LINQ Analytics:** Performs advanced in-memory groupings and projections to calculate historical data such as Total Revenue (formatted via Custom Extension Methods), Best-Selling Products, and Top Customers.
  * **Logical Deletion (Soft Delete):** Features an `ISoftDelete` pipeline allowing books to be removed from client views while safely maintaining tracking integrity in persistence.
  * **Bulletproof Validation:** Isolated input streaming that intercepts invalid data types and out-of-bound arguments, providing detailed feedback without ever allowing the app runtime to crash.

---

## 📸 Project in Action (Screenshots)

![Screenshot 1](https://github.com/user-attachments/assets/9d846794-d1bc-421b-8fb2-910a6ca33be3)

![Screenshot 3](https://github.com/user-attachments/assets/e5498c40-3123-466c-9de0-620550ac5263)

![Screenshot 4](https://github.com/user-attachments/assets/d0fa5700-527a-4095-8236-34060238e7af)

![Screenshot 5](https://github.com/user-attachments/assets/86041285-a9b1-46e5-b6d0-e284731553d5)

![Screenshot 6](https://github.com/user-attachments/assets/ebd1f7c2-c6e8-4e06-b58d-2efdc76e4701)

![Screenshot 8](https://github.com/user-attachments/assets/fb2c7e36-1107-47d8-b967-92e68b097122)
