using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookStoreService
    {
        BookStoreDto GetById(int id);
        void Delete(int id);
        void Update(UpdateBookStoreDto dto, int id);
        IEnumerable<BookStoreDto> GetAll();
        int Create(CreateBookStoreDto dto);
    }
}
