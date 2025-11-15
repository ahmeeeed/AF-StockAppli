using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockAppli.Models;
using StockAppli.Models.Collections.Sessions;
using StockAppli.Models.Collections.Users;
using StockAppli.Models.Entities;
using StockAppli.Models.Models;

namespace StockAppli_API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserCollection _userCollection;
        private readonly AppSettings _configuration;
        private readonly ISessionCollection _sessionCollection;
        public UserService(IUserCollection iUserCollection, IOptions<AppSettings> configuration, ISessionCollection sessionCollection)
        {
            _userCollection = iUserCollection;
            _configuration = configuration.Value;
            _sessionCollection = sessionCollection;
        }
        public async Task<User?> UserExists(string email)
        {
            var user = await _userCollection.FindByEmail(email);
            return user;
        }
        public async Task<ActionResult<User?>> CreateUser(CreateUserModel user)
        {
            var result = await _userCollection.CreateUSer(user);
            return result;
        }
        public async Task<User?> Login(string email, string password)
        {
            var user = await _userCollection.FindByEmail(email);

            if (user == null)
            {
                return null;
            }

            return !VerifyPasswordHash(
                Encoding.UTF8.GetBytes(password),
                Convert.FromBase64String(user.PasswordHash),
                Convert.FromBase64String(user.PasswordSalt)
            ) ? null : user;
        }
        public bool VerifyPasswordHash(byte[] password, byte[] passwordHash, byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new(passwordSalt);
            var computedHash = hmac.ComputeHash(password);
            return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
        }
        public async Task<string> CreateToken(User user)
        {
            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new();
            var key = Encoding.ASCII.GetBytes(_configuration.JWTsecret);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id),
                new(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new(ClaimTypes.Email, user.Email)
            };

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            await _sessionCollection.CreateToken(user.Id, token);

            return token;
        }
        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userCollection.DeleteUser(id);
            return user;
        }
    }
}
