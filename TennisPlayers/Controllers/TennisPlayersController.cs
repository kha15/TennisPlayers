using Microsoft.AspNetCore.Mvc;
using TennisPlayers.Models;
using TennisPlayers.Services;

namespace TennisPlayers.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class TennisPlayersController : ControllerBase
    {
        private readonly TennisPlayersService _playerService;

        public TennisPlayersController(TennisPlayersService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<players>> GetPlayerById(int id)
        {
            try
            {
                var player = await _playerService.GetPlayerByIdAsync(id);
                if (player == null)
                {
                    return NotFound("Player not found");
                }
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the player: {ex.Message}");
            }
        }

        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<players>>> GetSortedPlayers()
        {
            try
            {
                var players = await _playerService.GetSortedPlayersAsync();
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving sorted players: {ex.Message}");
            }
        }

        [HttpGet("stats")]
        public async Task<ActionResult<object>> GetStats()
        {
            try
            {
                var stats = await _playerService.GetStatisticsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving statistics: {ex.Message}");
            }
        }

        [HttpGet("top/{x}")]
        public async Task<ActionResult<IEnumerable<players>>> GetTopRankedPlayers(int x)
        {
            try
            {
                var topPlayers = await _playerService.GetTopRankedPlayersAsync(x);
                return Ok(topPlayers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving top ranked players: {ex.Message}");
            }
        }

        [HttpGet("country/{code}")]
        public async Task<ActionResult<IEnumerable<players>>> GetPlayersByCountry(string code)
        {
            try
            {
                var players = await _playerService.GetPlayersByCountryAsync(code);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving players by country: {ex.Message}");
            }
        }
    }
}
