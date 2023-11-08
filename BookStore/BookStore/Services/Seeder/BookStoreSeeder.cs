using BookStore.Entities;
using BookStore.Services.DataReader;
using BookStore.Services.Interfaces;

namespace BookStore.Services.Seeder
{
    public class BookStoreSeeder : IDataSeeder<BookStore.Entities.BookStore>
    {
        private readonly BookStoreDbContext _dbContext;
        // private readonly JsonFileReader<BookStore.Entities.BookStore> _reader;

        public BookStoreSeeder(BookStoreDbContext dbContext)//, JsonFileReader<BookStore.Entities.BookStore> reader)
        {
            _dbContext = dbContext;
            // _reader = reader;
        }

        private IEnumerable<BookStore.Entities.BookStore> GetItems()
        {
            // Should be based on fileReader
            // var results = _reader.Read();
            // return results;

            
            var bookStores = new List<BookStore.Entities.BookStore>()
            {
                new BookStore.Entities.BookStore()
                {
                    Name = "Brooklyn Book Store",
                    Description = "Best Book Store in Brooklyn Yet",
                    IsActive = true,
                    ContactEmail = "BestBrooklynBookStore@gmail.com",
                    ContactNumber = 123456,
                    Address = new BookStore.Entities.Address()
                    {
                        City = "New York",
                        Street = "Long",
                        PostalCode = "30-001"
                    },
                    Books = new List<BookStore.Entities.Book>()
                    {
                        new BookStore.Entities.Book()
                        {
                            Title = "Top Ten 10 To Do In NY",
                            Description = "BestSeller of 99",
                            Price = 10.30M
                        }
                    }
                },
                new BookStore.Entities.BookStore()
                {
                    Name = "NYPD Local Library",
                    Description = "Open For True Caps Only",
                    IsActive = true,
                    ContactEmail = "caps@gmail.com",
                    ContactNumber = 997997997,
                    Address = new BookStore.Entities.Address()
                    {
                        City = "New York",
                        Street = "Short",
                        PostalCode = "30-115"
                    },
                    Books = new List<BookStore.Entities.Book>()
                    {
                        new BookStore.Entities.Book()
                        {
                            Title = "How to catch a thief in 10 ways",
                            Description = "Required for all of you",
                            Price = 2.50M
                        }
                    }
                }
            };
            return bookStores;

        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.BookStores.Any())
                {
                    var bookStores = GetItems();
                    _dbContext.BookStores.AddRange(bookStores);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
