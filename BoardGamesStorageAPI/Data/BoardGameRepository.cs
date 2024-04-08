using Microsoft.EntityFrameworkCore;

namespace BoardGamesStorageAPI.Data
{
    public class BoardGameRepository<TEntity> : IBoardGameRepository<TEntity> where TEntity : class
    {
        readonly DataContextEF _entityFramework;

        public BoardGameRepository(DataContextEF entityFramework)
        {
            _entityFramework = entityFramework;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _entityFramework.SaveChangesAsync() > 0;
        }

        public void AddEntity(TEntity entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity(TEntity entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFramework.Remove(entityToRemove);
            }
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesAsync()
        {
            return await _entityFramework.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetSingleEntityAsync(int entityId)
        {
            TEntity entity = await _entityFramework.Set<TEntity>().FindAsync(entityId);
            if (entity != null)
            {
                return entity;
            }
            throw new Exception("Failed to Get Entity");
        }
    }
}
