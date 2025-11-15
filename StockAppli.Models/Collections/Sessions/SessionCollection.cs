using StockAppli.Models.Collections.BaseRepository;
using StockAppli.Models.Entities;

namespace StockAppli.Models.Collections.Sessions
{
    public class SessionCollection : BaseRepository<Session>, ISessionCollection
    {
        public SessionCollection(IMongoDbContext context) : base(context)
        {
        }
        public async Task<Session> CreateToken(string idUser, string token)
        {
            Session session = new()
            {
                IdUser = idUser,
                Token = token
            };

            var result = await Create(session);
            return result;
        }
    }
}
