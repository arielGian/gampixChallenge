using Microsoft.AspNetCore.Mvc;
using gampix.api.Domain;
using gampix.api.Services;

namespace gampix.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BetsController : ControllerBase
    {
        private readonly IBetService _betService;
        private readonly ILogger<BetsController> _logger;

        public BetsController(IBetService betService, ILogger<BetsController> logger)
        {
            _betService = betService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bet>>> GetAllBets()
        {
            try
            {
                var bets = await _betService.GetAllBetsAsync();
                return Ok(bets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bets");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bet>> GetBetById(int id)
        {
            try
            {
                var bet = await _betService.GetBetByIdAsync(id);
                if (bet == null)
                    return NotFound();

                return Ok(bet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bet {BetId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Bet>>> GetBetsByUserId(int userId)
        {
            try
            {
                var bets = await _betService.GetBetsByUserIdAsync(userId);
                return Ok(bets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bets for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("game/{gameName}")]
        public async Task<ActionResult<List<Bet>>> GetBetsByGameName(string gameName)
        {
            try
            {
                var bets = await _betService.GetBetsByGameNameAsync(gameName);
                return Ok(bets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bets for game {GameName}", gameName);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Bet>> CreateBet([FromBody] CreateBetRequest request)
        {
            try
            {
                if (request.Stake <= 0)
                    return BadRequest("Stake must be greater than 0");

                var bet = await _betService.CreateBetAsync(request);
                return CreatedAtAction(nameof(GetBetById), new { id = bet.Id }, bet);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bet");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<Bet>> UpdateBetStatus(int id, [FromBody] UpdateBetStatusRequest request)
        {
            try
            {
                var bet = await _betService.UpdateBetStatusAsync(id, request.Status, request.Result);
                return Ok(bet);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bet status {BetId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("rtp")]
        public async Task<ActionResult<decimal>> GetRTP()
        {
            try
            {
                var rtp = await _betService.GetRTPAsync();
                return Ok(rtp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating RTP");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class UpdateBetStatusRequest
    {
        public BetStatus Status { get; set; }
        public string? Result { get; set; }
    }
}
