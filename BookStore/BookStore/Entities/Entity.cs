namespace BookStore.Entities
{
    public abstract class Entity
    {
        public string? CreatedBy { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateModified { get; set; }
    }
}
