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
        private readonly IBoardGameRepository _boardGameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(IConfiguration configuration, IBoardGameRepository boardGameRepository, IMapper mapper, ILogger<BoardGamesController> logger)
        {
            _boardGameRepository = boardGameRepository ?? throw new ArgumentNullException(nameof(boardGameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("GetBoardGames")]
        public async Task<IEnumerable<BoardGame>> GetBoardGames()
        {
            IEnumerable<BoardGame> boardGames = await _boardGameRepository.GetBoardGamesAsync();
            return boardGames;
        }

        [HttpGet("GetSingleBoardGame/{boardGameId}")]
        public async Task<BoardGame> GetSingleBoardGame(int boardGameId)
        {
            return await _boardGameRepository.GetSingleBoardGameAsync(boardGameId);
        }

        [HttpPut("EditBoardGame/{boardGameId}")]
        public async Task<IActionResult> EditBoardGame(int boardGameId, BoardGameDTO boardGameDto)
        {
            var gameDb = await _boardGameRepository.GetSingleBoardGameAsync(boardGameId);

            if (gameDb == null)
            {
                return NotFound();
            }

            _mapper.Map(boardGameDto, gameDb);

            if (await _boardGameRepository.SaveChangesAsync())
            {
                return Ok();
            }

            throw new Exception("Failed to update Board Game");
        }

        [HttpPost("AddBoardGame")]
        public async Task<IActionResult> AddBoardGame(BoardGameDTO boardGame)
        {
            BoardGame gameDb = _mapper.Map<BoardGame>(boardGame);

            _boardGameRepository.AddEntity<BoardGame>(gameDb);
            if (await _boardGameRepository.SaveChangesAsync())
            {
                return Ok();
            }
            throw new Exception("Failed to Add Board Game");
        }

        [HttpDelete("DeleteBoardGame/{boardGameId}")]
        public async Task<IActionResult> DeleteBoardGame(int boardGameId)
        {
            BoardGame? gameDb = await _boardGameRepository.GetSingleBoardGameAsync(boardGameId);

            if (gameDb != null)
            {
                _boardGameRepository.RemoveEntity<BoardGame>(gameDb);

                if (await _boardGameRepository.SaveChangesAsync())
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete Board Game");
            }
            throw new Exception("Failed to Delete Board Game");
        }
    }
}
