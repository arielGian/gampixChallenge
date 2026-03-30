using Microsoft.AspNetCore.Mvc;
using gampix.api.Services;

namespace gampix.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> GetAllGames()
        {
            try
            {
                var games = await _gameService.GetAllGamesAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting games");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetGameById(int id)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(id);
                if (game == null)
                    return NotFound();

                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting game {GameId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateGame([FromBody] CreateGameRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                    return BadRequest("Game name is required");

                if (request.EndDate <= request.StartDate)
                    return BadRequest("End date must be after start date");

                var game = await _gameService.CreateGameAsync(request);
                return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating game");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<object>> UpdateGame(int id, [FromBody] UpdateGameRequest request)
        {
            try
            {
                var game = await _gameService.UpdateGameAsync(id, request);
                return Ok(game);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating game {GameId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                var result = await _gameService.DeleteGameAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting game {GameId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
