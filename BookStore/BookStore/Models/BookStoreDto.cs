using BookStore.Entities;

namespace BookStore.Models
{
    public class BookStoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? ContactEmail { get; set; }
        public int? ContactNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
