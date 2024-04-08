using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoardGamesStorageAPI.Data
{
    public interface IBoardGameRepository<TEntity>
    {
        Task<bool> SaveChangesAsync();
        void AddEntity(TEntity entityToAdd);
        void RemoveEntity(TEntity entityToRemove);
        Task<IEnumerable<TEntity>> GetEntitiesAsync();
        Task<TEntity> GetSingleEntityAsync(int entityId);
    }
}