using BoardGamesStorageAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoardGamesStorageAPI.Data
{
    public class BoardGameRepository : IBoardGameRepository

    {
        readonly DataContextEF _entityFramework;

        public BoardGameRepository(IConfiguration configuration)
        {
            _entityFramework = new DataContextEF(configuration);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _entityFramework.SaveChangesAsync() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFramework.Remove(entityToRemove);
            }
        }

        public async Task<IEnumerable<BoardGame>> GetBoardGamesAsync()
        {
            IEnumerable<BoardGame> boardGames = await _entityFramework.BoardGames.ToListAsync();
            return boardGames;
        }

        public async Task<BoardGame> GetSingleBoardGameAsync(int boardGameId)
        {
            BoardGame game = await _entityFramework.BoardGames.FirstOrDefaultAsync(b => b.BoardGameId == boardGameId);
            if (game != null)
            {
                return game;
            }
            throw new Exception("Failed to Get Board Game");
        }
    }
}
