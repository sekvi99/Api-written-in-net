namespace BookStore.Entities
{
    public class Address : Entity
    {
        // Table
        public int Id { get; set; }
        public string City { get; set; }
        public string Street {  get; set; }
        public string? PostalCode { get; set; }

        // Table Reference
        public virtual BookStore BookStore { get; set; }
    }
}
