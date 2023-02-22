using AutoMapper;
using BoardGamesStorageAPI.Data;
using BoardGamesStorageAPI.DTO;
using BoardGamesStorageAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesStorageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardGamesController : ControllerBase
    {
        DataContextEF _entityFramework;
        IMapper _mapper;

        public BoardGamesController(IConfiguration configuration)
        {
            _entityFramework = new DataContextEF(configuration);
            _mapper = new Mapper(new MapperConfiguration(config => {

                config.CreateMap<BoardGameToAddDTO, BoardGame>();
            }));
        }

        [HttpGet("GetBoardGames")]
        public IEnumerable<BoardGame> GetBoardGames()
        {
            IEnumerable<BoardGame> boardGames = _entityFramework.BoardGames.ToList<BoardGame>();
            return boardGames;
        }

        [HttpGet("GetSingleBoardGame/{boardGameId}")]
        public BoardGame GetSingleBoardGame(int boardGameId)
        {
            BoardGame game = _entityFramework.BoardGames.Where(b => b.BoardGameId == boardGameId).FirstOrDefault<BoardGame>();
            if (game != null)
            {
                return game;
            }
            throw new Exception("Failed to Get Board Game");
        }

        [HttpPut("EditBoardGame")]
        public IActionResult EditBoardGame(BoardGame boardGame)
        {
            BoardGame? gameDb = _entityFramework.BoardGames.Where(b => b.BoardGameId == boardGame.BoardGameId).FirstOrDefault<BoardGame>();

            if (gameDb != null)
            {
                gameDb.BoardGameName = boardGame.BoardGameName;
                gameDb.MinimumPlayers = boardGame.MinimumPlayers;
                gameDb.MaximumPlayers = boardGame.MaximumPlayers;
                gameDb.PlayTimeInMinutes = boardGame.PlayTimeInMinutes;
                gameDb.AgeLimit = boardGame.AgeLimit;
                gameDb.YearOfProduction = boardGame.YearOfProduction;
                gameDb.Publisher = boardGame.Publisher;
                gameDb.Author = boardGame.Author;

                if (_entityFramework.SaveChanges()>0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Update Board Game");
            }
            throw new Exception("Failed to Update Board Game");
        }

        [HttpPost("AddBoardGame")]
        public IActionResult AddBoardGame(BoardGameToAddDTO boardGame)
        {
            BoardGame gameDb = _mapper.Map<BoardGame>(boardGame);

            _entityFramework.Add(gameDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Add Board Game");
        }

        [HttpDelete("DeleteBoardGame/{boardGameId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteBoardGame(int boardGameId)
        {
            BoardGame? gameDb = _entityFramework.BoardGames.Where(b => b.BoardGameId ==boardGameId).FirstOrDefault<BoardGame>();

            if (gameDb != null)
            {
                _entityFramework.BoardGames.Remove(gameDb);

                if (_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete Board Game");

            }
            throw new Exception("Failed to Delete Board Game");
        }
    }
}
