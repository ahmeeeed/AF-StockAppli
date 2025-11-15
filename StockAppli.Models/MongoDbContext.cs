using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace StockAppli.Models
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase Db { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDbContext(IOptions<AppSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.DBcnx);
            Db = MongoClient.GetDatabase(configuration.Value.DBName);
        }

        public IMongoCollection<TEntity> GetCollectionAsync<TEntity>(string name)
        {
            return Db.GetCollection<TEntity>(name);
        }
    }
}
