using MongoDB.Driver;

namespace StockAppli.Models
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollectionAsync<TEntity>(string name);

    }
}
