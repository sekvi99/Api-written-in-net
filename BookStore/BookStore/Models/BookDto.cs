namespace BookStore.Models
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
