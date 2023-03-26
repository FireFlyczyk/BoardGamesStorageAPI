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
       
        public IActionResult EditBoardGame(int boardGameId, BoardGameDTO boardGameDto)
        {
            var gameDb = _boardGameRepository.GetSingleBoardGame(boardGameId);

            if (gameDb == null)
            {
                return NotFound();
            }

            _mapper.Map(boardGameDto, gameDb);

            if (_boardGameRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to update Board Game");
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
