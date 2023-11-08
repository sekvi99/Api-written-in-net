namespace BookStore.Services.Interfaces
{
    public interface IDataService<T>
    {
        public T GetById(int id);
        public IEnumerable<T> GetAll();
        public void Create(T dto);
        // public T UpdateById(int id);

    }
}
