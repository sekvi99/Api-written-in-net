using BookStore.Entities;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task Register(UserDto model);

        public Task<string> Login(UserDto model);
    }
}
