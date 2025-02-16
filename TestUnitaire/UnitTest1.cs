using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TennisPlayers.Models;
using TennisPlayers.Services;
using Xunit;

namespace TennisPlayers.Tests
{
    public class TennisPlayersServiceTests
    {
        private readonly Mock<HttpClient> _mockHttpClient;
        private readonly TennisPlayersService _service;

        public TennisPlayersServiceTests()
        {
            _mockHttpClient = new Mock<HttpClient>();
            _service = new TennisPlayersService(_mockHttpClient.Object);
        }

        [Fact]
        public async Task GetPlayersAsync_ReturnsListOfPlayers()
        {

            var expected = new List<players>
            {
                new players { id = 52, firstname = "Novak", lastname = "Djokovic" }
            };

            var players = await _service.GetPlayersAsync();

            Assert.NotEmpty(players);
            Assert.Equal(expected.First().firstname, players.First().firstname);
            Assert.Equal(expected.First().id, players.First().id);
        }

        [Fact]
        public async Task GetPlayerByIdAsync_ReturnsPlayer_WhenPlayerExists()
        {

            var expected = new players { id = 52, firstname = "Novak", lastname = "Djokovic" };

            var player = await _service.GetPlayerByIdAsync(52);

            Assert.NotNull(player);
            Assert.Equal(expected.id, player.id);
        }

        [Fact]
        public async Task GetPlayerByIdAsync_ReturnsNull_WhenPlayerDoesNotExist()
        {

            var player = await _service.GetPlayerByIdAsync(99);
            Assert.Null(player);
        }

        [Fact]
        public async Task GetSortedPlayersAsync_ReturnsSortedListOfPlayers()
        {
            var expected = new List<players>
            {
                new players { id = 2, firstname = "Rafael", lastname = "Nadal", data = new playerData { rank = 1 }},
                new players { id = 92, firstname = "Venus", lastname = "Williams", data = new playerData { rank = 52 }}
            };

            var sortedPlayers = await _service.GetSortedPlayersAsync();

            Assert.Equal(expected.First().data.rank, sortedPlayers.First().data.rank);
            Assert.Equal(expected.Last().data.rank, sortedPlayers.Last().data.rank);
        }

        [Fact]
        public async Task GetStatisticsAsync_ReturnsValidStats()
        {

            var stats = await _service.GetStatisticsAsync();
            Assert.NotNull(stats);
        }

        [Fact]
        public async Task GetTopRankedPlayersAsync_ReturnsTopRankedPlayers()
        {
            var expected = new List<players>
            {
                new players { id = 2, firstname = "Rafael", lastname = "Nadal", data = new playerData { rank = 1 }}
            };

            var topPlayers = await _service.GetTopRankedPlayersAsync(1);

            Assert.Single(topPlayers);
            Assert.Equal(expected.First().firstname, topPlayers.First().firstname);
        }

        [Fact]
        public async Task GetPlayersByCountryAsync_ReturnsPlayersFromGivenCountry()
        {
            var expected = new List<players>
            {
                new players { id = 1, firstname = "Novak", lastname = "Djokovic", country = new country { code = "SRB" }}
            };

            var playersByCountry = await _service.GetPlayersByCountryAsync("SRB");

            Assert.Single(playersByCountry);
            Assert.Equal(expected.First().firstname, playersByCountry.First().firstname);
        }
    }
}