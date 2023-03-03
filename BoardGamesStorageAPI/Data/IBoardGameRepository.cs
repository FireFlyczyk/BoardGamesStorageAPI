using BoardGamesStorageAPI.Models;

namespace BoardGamesStorageAPI.Data
{
    public interface IBoardGameRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToRemove);
        public IEnumerable<BoardGame> GetBoardGames();
        public BoardGame GetSingleBoardGame(int boardGameId);

    }
}
