using BookStore.Services.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using BookStore.Entities;
using BookStore.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookStore.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly BookStore.Entities.BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookStoreService> _logger;

        public AuthenticationService(IPasswordHasher<User> passwordHasher, BookStore.Entities.BookStoreDbContext dbContext, IMapper mapper, ILogger<BookStoreService> logger)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task<User> GetUser(UserDto model)
        {
            var existingUser = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            return existingUser;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test-secret-key-losowe-jwt-tokeny-hue-hue")); // TODO Change to key in appSetting
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> Login(UserDto model)
        {
            var existingUser = await GetUser(model);
            if (existingUser == null)
            {
                throw new NotFoundException("User with provided UserName does not exist in database");
            }

            if (_passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, model.Password) == PasswordVerificationResult.Success)
            {
                var token = GenerateJwtToken(existingUser);
                return token;
            }
            else
            {
                throw new UnauthorizedAccessException("Wrong Username or Password");
            }
        }

        public async Task Register(UserDto model)
        {
            var existingUser = await GetUser(model);

            if (existingUser == null) 
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = _passwordHasher.HashPassword(null, model.Password) // Hash the password
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ResourceExistException("User with given name already exist");
            }
        }

    }
}
