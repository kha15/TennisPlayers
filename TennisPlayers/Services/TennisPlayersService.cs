using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TennisPlayers.Models;

namespace TennisPlayers.Services
{
    public class TennisPlayersService
    {
        private readonly HttpClient _httpClient;
        private const string DataUrl = "https://tenisu.latelier.co/resources/headtohead.json";

        public TennisPlayersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<players>> GetPlayersAsync()
        {
            var json = await _httpClient.GetStringAsync(DataUrl);
            var playersData = JsonSerializer.Deserialize<PlayerList>(json);
            return playersData?.players ?? new List<players>();
        }

        public async Task<players> GetPlayerByIdAsync(int id)
        {
            var players = await GetPlayersAsync();
            return players.FirstOrDefault(p => p.id == id);
        }

        public async Task<List<players>> GetSortedPlayersAsync()
        {
            var players = await GetPlayersAsync();
            return players.OrderBy(p => p.data.rank).ToList();
        }

        public async Task<object> GetStatisticsAsync()
        {
            var players = await GetPlayersAsync();

            var playersWithStats = players.Select(player => new
            {
                Player = player,
                WinRatio = CalculateWinRatio(player.data.last),
                Bmi = CalculateBmi(player.data.weight, player.data.height)
            }).ToList();

            var countryWinsRatio = playersWithStats
                .GroupBy(p => p.Player.country.code)
                .Select(g => new
                {
                    Country = g.Key
                })
                .FirstOrDefault();

            var averageBmi = playersWithStats.Average(p => p.Bmi);

            var heights = players.Select(p => p.data.height).OrderBy(h => h).ToList();
            double medianHeight;
            int count = heights.Count;

            if (count % 2 == 0)
            {
                medianHeight = (heights[count / 2 - 1] + heights[count / 2]) / 2.0;
            }
            else
            {
                medianHeight = heights[count / 2];
            }

            return new
            {
                CountryWithBestWinRatio = countryWinsRatio,
                AverageBmi = averageBmi,
                MedianHeight = medianHeight
            };
        }

        private double CalculateWinRatio(List<int> lastMatches)
        {
            if (lastMatches == null || lastMatches.Count == 0)
                return 0;

            int wins = lastMatches.Count(match => match == 1);
            return (double)wins / lastMatches.Count;
        }

        private double CalculateBmi(int weight, int height)
        {
            double heightInMeters = height / 100.0;
            return weight/(1000.0) / (heightInMeters * heightInMeters);
        }

        public async Task<List<players>> GetTopRankedPlayersAsync(int count)
        {
            var players = await GetPlayersAsync();
            return players.OrderBy(p => p.data.rank).Take(count).ToList();
        }

        public async Task<List<players>> GetPlayersByCountryAsync(string countryCode)
        {
            var players = await GetPlayersAsync();
            return players.Where(p => p.country.code.ToUpper() == countryCode.ToUpper()).ToList();
        }

        public class PlayerList
        {
            public List<players> players { get; set; }
        }
    }
}
