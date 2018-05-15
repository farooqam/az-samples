using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.DataModels;
using Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Api.IntegrationTests
{
    public class GameLogControllerShould
    {
        private readonly HttpClient _client;
        private readonly Mock<IGameResultRepository> _mockRepository = new Mock<IGameResultRepository>();

        public GameLogControllerShould()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(
                    collection => collection.AddTransient(provider => _mockRepository.Object))
            );
            
            _client = server.CreateClient();
        }

        [Fact]
        public async Task ReturnNotFoundWhenNoGameResult()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetGameResultsAsync(It.IsAny<string>(), It.IsAny<short>())).ReturnsAsync(new List<GameResult>());

            // Act
            var response = await _client.GetAsync("api/gamelogs/nya/2017");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ReturnGameLogsAndOkStatusCode()
        {
            // Arrange
            var team = "nya";
            short year = 2017;

            var gameResultsFromRepository = new List<GameResult>
            {
                new GameResult
                {
                    GameYear = year,
                    AwayTeam = "bos",
                    AwayTeamScore = 5,
                    GameDay = 11,
                    GameMonth = 5,
                    GameNumber = 12,
                    HomeTeam = team,
                    HomeTeamScore = 7
                }
            };

            _mockRepository.Setup(r => r.GetGameResultsAsync(team, year)).ReturnsAsync(gameResultsFromRepository);
            
            // Act
            var response = await _client.GetAsync($"api/gamelogs/{team}/{year}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var gameResultsFromApi = JsonConvert.DeserializeObject<IEnumerable<DomainModel.GameResult>>(responseContent);

            var gameResultFromApi = gameResultsFromApi.First();
            var gameResultFromRepository = gameResultsFromRepository.First();

            gameResultFromApi.GameYear.Should().Be(year);
            gameResultFromApi.AwayTeam.Should().Be(gameResultFromRepository.AwayTeam);
            gameResultFromApi.AwayTeamScore.Should().Be(gameResultFromRepository.AwayTeamScore);
            gameResultFromApi.GameDay.Should().Be(gameResultFromRepository.GameDay);
            gameResultFromApi.GameMonth.Should().Be(gameResultFromRepository.GameMonth);
            gameResultFromApi.GameNumber.Should().Be(gameResultFromRepository.GameNumber);
            gameResultFromApi.HomeTeam.Should().Be(gameResultFromRepository.HomeTeam);
            gameResultFromApi.HomeTeamScore.Should().Be(gameResultFromRepository.HomeTeamScore);
            gameResultFromApi.RunDifferential.Should().Be((byte)(gameResultFromRepository.HomeTeamScore - gameResultFromRepository.AwayTeamScore));
        }
    }
}
