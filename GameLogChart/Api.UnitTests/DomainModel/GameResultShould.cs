using Api.ApiModels;
using FluentAssertions;
using Xunit;

namespace Api.UnitTests.DomainModel
{
    public class GameResultShould
    {
        [Fact]
        public void CalculateNegativeRunDifferential()
        {
            // Act

            var gameResult = new GameResult {HomeTeamScore = 8, AwayTeamScore = 10};

            // Assert

            gameResult.RunDifferential.Should().Be(-2);
        }

        [Fact]
        public void CalculatePositiveRunDifferential()
        {
            // Act

            var gameResult = new GameResult {HomeTeamScore = 10, AwayTeamScore = 8};

            // Assert 

            gameResult.RunDifferential.Should().Be(2);
        }
    }
}