using StockAppli.Models.Entities;

namespace StockAppli.Models.Collections.Sessions
{
    public interface ISessionCollection
    {
        Task<Session> CreateToken(string idUser, string token);
       // Task<bool> CheckToken(string id, string token);
        //Task<bool> DeleteToken(string id, string token);
    }
}
