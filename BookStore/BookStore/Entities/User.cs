namespace BookStore.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
