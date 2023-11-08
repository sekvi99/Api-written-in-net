namespace BookStore.Services.DataReader
{
    public abstract class AbstractFileReader<T>
    {
        private readonly string _filePath;

        public abstract IEnumerable<T> Read(); 
    }
}
