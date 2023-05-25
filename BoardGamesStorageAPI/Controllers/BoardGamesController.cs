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

        public BoardGamesController(IBoardGameRepository boardGameRepository, IMapper mapper, ILogger<BoardGamesController> logger)
        {
            _boardGameRepository = boardGameRepository ?? throw new ArgumentNullException(nameof(boardGameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetBoardGames()
        {
            try
            {
                IEnumerable<BoardGame> boardGames = await _boardGameRepository.GetBoardGamesAsync();
                return Ok(boardGames);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving board games.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{boardGameId}")]
        public async Task<IActionResult> GetSingleBoardGame(int boardGameId)
        {
            try
            {
                BoardGame game = await _boardGameRepository.GetSingleBoardGameAsync(boardGameId);

                if (game == null)
                {
                    return NotFound();
                }

                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a board game.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{boardGameId}")]
        public async Task<IActionResult> EditBoardGame(int boardGameId, BoardGameDTO boardGameDto)
        {
            try
            {
                BoardGame gameDb = await _boardGameRepository.GetSingleBoardGameAsync(boardGameId);

                if (gameDb == null)
                {
                    return NotFound();
                }

                _mapper.Map(boardGameDto, gameDb);

                if (await _boardGameRepository.SaveChangesAsync())
                {
                    return Ok();
                }

                return StatusCode(500, "Failed to update Board Game");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a board game.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBoardGame(BoardGameDTO boardGame)
        {
            try
            {
                BoardGame gameDb = _mapper.Map<BoardGame>(boardGame);

                _boardGameRepository.AddEntity(gameDb);
                if (await _boardGameRepository.SaveChangesAsync())
                {
                    return Ok();
                }

                return StatusCode(422, "Failed to add Board Game");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a board game.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{boardGameId}")]
        public async Task<IActionResult> DeleteBoardGame(int boardGameId)
        {
            try
            {
                BoardGame gameDb = await _boardGameRepository.GetSingleBoardGameAsync(boardGameId);

                if (gameDb == null)
                {
                    return NotFound();
                }

                _boardGameRepository.RemoveEntity(gameDb);

                if (await _boardGameRepository.SaveChangesAsync())
                {
                    return NoContent();
                }

                return StatusCode(500, "Failed to delete Board Game");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a board game.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
