namespace BookStore.Entities
{
    public class BookStore : Entity
    {
        // Current Table
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? ContactEmail { get; set; }
        public int? ContactNumber { get; set; }

        // Table refernce
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Book> Books { get; set; }

    }
}
