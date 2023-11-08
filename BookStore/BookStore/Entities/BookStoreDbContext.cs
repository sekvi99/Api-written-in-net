using Microsoft.EntityFrameworkCore;

namespace BookStore.Entities
{
    public class BookStoreDbContext : DbContext
    {
        private string _connectionString = "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbSet<BookStore> BookStores { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .IsRequired();

            // BookStore
            modelBuilder.Entity<BookStore>()
                .Property(bookStore => bookStore.Name)
                .IsRequired()
                .HasMaxLength(60);

            // Book
            modelBuilder.Entity<Book>()
                .Property(book => book.Title)
                .IsRequired()
                .HasMaxLength (60);

            modelBuilder.Entity<Book>()
                .Property(book => book.Price)
                .IsRequired();

            // Address
            modelBuilder.Entity<Address>()
                .Property(address => address.City)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
