using Api.Services.DocumentDb;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Api.UnitTests.Services.DocumentDb
{
    public class GameResultTypeConverterShould
    {
        public GameResultTypeConverterShould()
        {
            // Arrange

            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<GameResult, DataModels.GameResult>()
                    .ConvertUsing<GameResultTypeConverter>();
            }).CreateMapper();
        }

        private readonly IMapper _mapper;

        [Fact]
        public void ConvertFromDocumentDbModelToDataModel()
        {
            // Arrange

            var documentDbModel = new GameResult
            {
                game_day = 1,
                game_month = 10,
                game_number = 3,
                game_year = 2017,
                home_team = "nya",
                home_team_score = 10,
                visiting_team = "bos",
                visiting_team_score = 8
            };

            // Act

            var dataModel = _mapper.Map<Api.DataModels.GameResult>(documentDbModel);

            // Assert

            dataModel.AwayTeamScore.Should().Be(documentDbModel.visiting_team_score);
            dataModel.AwayTeam.Should().Be(documentDbModel.visiting_team);
            dataModel.GameDay.Should().Be(documentDbModel.game_day);
            dataModel.GameNumber.Should().Be(documentDbModel.game_number);
            dataModel.GameDay.Should().Be(documentDbModel.game_day);
            dataModel.GameMonth.Should().Be(documentDbModel.game_month);

        }
    }
}