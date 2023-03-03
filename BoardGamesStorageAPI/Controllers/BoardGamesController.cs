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
        IBoardGameRepository _boardGameRepository;
        IMapper _mapper;

        public BoardGamesController(IConfiguration configuration, IBoardGameRepository boardGameRepository)
        {
            _boardGameRepository = boardGameRepository;
            _mapper = new Mapper(new MapperConfiguration(config =>
            {

                config.CreateMap<BoardGameDTO, BoardGame>();
            }));
            _boardGameRepository = boardGameRepository;
        }

        [HttpGet("GetBoardGames")]
        public IEnumerable<BoardGame> GetBoardGames()
        {
            IEnumerable<BoardGame> boardGames = _boardGameRepository.GetBoardGames();
            return boardGames;
        }

        [HttpGet("GetSingleBoardGame/{boardGameId}")]
        public BoardGame GetSingleBoardGame(int boardGameId)
        {
          return _boardGameRepository.GetSingleBoardGame(boardGameId);
        }

        [HttpPut("EditBoardGame")]
        public IActionResult EditBoardGame(BoardGame boardGame)
        {
            BoardGame? gameDb = _boardGameRepository.GetSingleBoardGame(boardGame.BoardGameId);

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

                if (_boardGameRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Update Board Game");
            }
            throw new Exception("Failed to Update Board Game");
        }

        [HttpPost("AddBoardGame")]
        public IActionResult AddBoardGame(BoardGameDTO boardGame)
        {
            BoardGame gameDb = _mapper.Map<BoardGame>(boardGame);

            _boardGameRepository.AddEntity<BoardGame>(gameDb);
            if (_boardGameRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Add Board Game");
        }

        [HttpDelete("DeleteBoardGame/{boardGameId}")]

        public IActionResult DeleteBoardGame(int boardGameId)
        {
            BoardGame? gameDb = _boardGameRepository.GetSingleBoardGame(boardGameId);

            if (gameDb != null)
            {
                _boardGameRepository.RemoveEntity<BoardGame>(gameDb);

                if (_boardGameRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete Board Game");

            }
            throw new Exception("Failed to Delete Board Game");
        }
    }
}
