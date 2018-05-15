using AutoMapper;

namespace Api.Services.DocumentDb
{
    public class GameResultTypeConverter : ITypeConverter<GameResult, DataModels.GameResult>
    {
        public DataModels.GameResult Convert(
            GameResult source, 
            DataModels.GameResult destination,
            ResolutionContext context)
        {
            return new DataModels.GameResult
            {
                HomeTeamScore = source.home_team_score,
                AwayTeam = source.visiting_team,
                GameDay = source.game_day,
                AwayTeamScore = source.visiting_team_score,
                GameMonth = source.game_month,
                GameNumber = source.game_number,
                GameYear = source.game_year,
                HomeTeam = source.home_team
            };
        }
    }
}