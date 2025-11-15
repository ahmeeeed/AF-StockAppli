using StockAppli.Models.Collections.BaseRepository;
using StockAppli.Models.Entities;
using StockAppli.Models.Models;

namespace StockAppli.Models.Collections.Users
{
    public interface IUserCollection : IBaseRepository<User>
    {
        Task<User?> FindByEmail(string email);
        Task<User?> CreateUSer(CreateUserModel userCreation);
        Task<bool> DeleteUser(string id);
    }
}
