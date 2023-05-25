using BoardGamesStorageAPI.Models;

namespace BoardGamesStorageAPI.Data
{
    public interface IBoardGameRepository
    {
        Task<bool> SaveChangesAsync();
        void AddEntity<T>(T entityToAdd);
        void RemoveEntity<T>(T entityToRemove);
        Task<IEnumerable<BoardGame>> GetBoardGamesAsync();
        Task<BoardGame> GetSingleBoardGameAsync(int boardGameId);

    }
}
