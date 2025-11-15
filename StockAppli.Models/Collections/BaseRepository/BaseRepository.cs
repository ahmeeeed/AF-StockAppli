using MongoDB.Driver;
using StockAppli.Logger;
using StockAppli.Models.Entities;

namespace StockAppli.Models.Collections.BaseRepository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoDbContext MongoContext;
        protected IMongoCollection<TEntity> DbCollection;

        protected BaseRepository(IMongoDbContext context)
        {
            MongoContext = context;
            DbCollection = MongoContext.GetCollectionAsync<TEntity>(typeof(TEntity).Name);
        }
        public async Task<TEntity> Create(TEntity obj)
        {
            if (obj == null)
            {
                NLogManager.Error("object is null", nameof(BaseRepository<TEntity>), nameof(Create));
                return null;
            }

            obj.CreatedAt = DateTime.UtcNow;
            await DbCollection.InsertOneAsync(obj);

            return obj;
        }
        public Task<TEntity> CreateOrUpdate(TEntity obj)
        {
            throw new NotImplementedException();
        }
        public async Task<TEntity> Update(TEntity obj)
        {
            obj.UpdatedAt = DateTime.UtcNow;

            await DbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, obj.Id), obj);

            return obj;
        }
        public async Task Delete(string id)
        {
            var obj = await Get(id);
            if (obj == null)
            {
                NLogManager.Error($"object is null : id= {id}", nameof(BaseRepository<TEntity>), nameof(Create));
                return;
            }

            obj.DeletedAt = DateTime.UtcNow;
            obj.Enable = false;

            await DbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, obj.Id), obj);
        }
        public async Task<TEntity> Get(string id)
        {
            var obj = await DbCollection.FindAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id));
            return await obj.FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetAll()
        {
            var all = await DbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }
    }
}
