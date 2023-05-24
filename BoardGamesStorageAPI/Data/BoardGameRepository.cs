using BoardGamesStorageAPI.Models;
using System.Linq;

namespace BoardGamesStorageAPI.Data
{
    public class BoardGameRepository : IBoardGameRepository

    {
        readonly DataContextEF _entityFramework;

        public BoardGameRepository(IConfiguration configuration)
        {
            _entityFramework= new DataContextEF(configuration);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges()>0;
        }
        public void AddEntity<T>(T entityToAdd)
        {
          if (entityToAdd!=null) 
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
        public IEnumerable<BoardGame> GetBoardGames()
        {
            IEnumerable<BoardGame> boardGames = _entityFramework.BoardGames.ToList<BoardGame>();
            return boardGames;
        }

        public BoardGame GetSingleBoardGame(int boardGameId)
        {
            BoardGame game = _entityFramework.BoardGames.Where(b => b.BoardGameId == boardGameId).FirstOrDefault<BoardGame>();
            if (game != null)
            {
                return game;
            }
            throw new Exception("Failed to Get Board Game");
        }
    }
}
