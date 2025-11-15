using System.Security.Cryptography;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StockAppli.Models.Collections.BaseRepository;
using StockAppli.Models.Entities;
using StockAppli.Models.Models;

namespace StockAppli.Models.Collections.Users
{
    public class UserCollection : BaseRepository<User>, IUserCollection
    {
        private readonly IOptions<AppSettings> _configuration;
        private readonly IMapper _mapper;

        public UserCollection(IMongoDbContext context, IOptions<AppSettings> configuration, IMapper mapper) : base(context)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<User?> FindByEmail(string email)
        {
            var filtre = Builders<User>.Filter.Eq(x => x.Email, email);
            filtre &= Builders<User>.Filter.Eq(x => x.Enable, true);
            IAsyncCursor<User?> user = await DbCollection.FindAsync<User>(filtre);
            return await user.FirstOrDefaultAsync();
        }
        public async Task<User?> CreateUSer(CreateUserModel userCreation)
        {
            var userToCreate = _mapper.Map<User>(userCreation);
            CreatePasswordHash(userCreation.Password, out var passwordHash, out var passwordSalt);
            userToCreate.PasswordHash = Convert.ToBase64String(passwordHash);
            userToCreate.PasswordSalt = Convert.ToBase64String(passwordSalt);
            userToCreate.AccountVerified = false;
            return await Create(userToCreate);
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<bool> DeleteUser(string id)
        {
            await Delete(id);
            return true;
        }
    }
}
