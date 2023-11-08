namespace BookStore.Services.Interfaces
{
    public interface IDataSeeder<T>
    {
        public void Seed();

        // protected IEnumerable<T> GetItems();
    }
}
