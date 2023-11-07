namespace BookStore.Entities
{
    public class Book : Entity
    {
        // Table
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        // Table Reference
        public int BookStoreId { get; set; }
        public virtual BookStore BookStore { get; set; }

        public bool isValidBook()
        {
            return Price > 0;
        }
    }
}
