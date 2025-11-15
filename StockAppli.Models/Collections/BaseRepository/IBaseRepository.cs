using StockAppli.Models.Entities;

namespace StockAppli.Models.Collections.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity obj);
        Task<TEntity> CreateOrUpdate(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task Delete(string id);
        Task<TEntity> Get(string id);
        Task<IList<TEntity>> GetAll();
    }

}
