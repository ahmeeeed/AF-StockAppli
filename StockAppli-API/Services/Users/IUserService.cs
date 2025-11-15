using Microsoft.AspNetCore.Mvc;
using StockAppli.Models.Entities;
using StockAppli.Models.Models;

namespace StockAppli_API.Services.Users
{
    public interface IUserService
    {
        Task<User?> UserExists(string email);
        Task<ActionResult<User?>> CreateUser(CreateUserModel user);
        Task<User?> Login(string email, string password);
        Task<string> CreateToken(User user);
        Task<bool> DeleteUser(string id);
    }
}
