using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api.Services.DocumentDb
{
    public class GameResultRepository : IGameResultRepository
    {
        private readonly DocumentDbRepositorySettings _settings;

        public GameResultRepository(DocumentDbRepositorySettings settings)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<DataModels.GameResult>> GetGameResultsAsync(string team, short year)
        {
            using (var client = new DocumentClient(_settings.Endpoint, _settings.Key))
            {
                var queryString =
                    $"SELECT c.game_year, c.game_number, c.game_month, c.game_day, c.home_team, c.visiting_team, c.home_team_score, c.visting_team_score " +
                    $"FROM c WHERE (c.home_team = \"{team.ToUpperInvariant()}\" OR c.visiting_team = \"{team.ToUpperInvariant()}\") " +
                    $"AND c.game_year = {year}";

                var query = client.CreateDocumentQuery<DocumentDb.GameResult>(
                        UriFactory.CreateDocumentCollectionUri("gamelogs", "gamelog"),
                        queryString,
                        new FeedOptions {MaxItemCount = 200, EnableCrossPartitionQuery = false})
                    .AsDocumentQuery();
                
                var gameResults = new List<DataModels.GameResult>();

                while (query.HasMoreResults)
                {
                    var queryResults = await query.ExecuteNextAsync<DocumentDb.GameResult>();

                    foreach (var gameResult in queryResults.ToList())
                    {
                        gameResults.Add(new DataModels.GameResult
                        {
                            HomeTeamScore = gameResult.home_team_score,
                            AwayTeam = gameResult.visiting_team,
                            GameDay = gameResult.game_day,
                            AwayTeamScore = gameResult.visting_team_score,
                            GameMonth = gameResult.game_month,
                            GameNumber = gameResult.game_number,
                            GameYear = gameResult.game_year,
                            HomeTeam = gameResult.home_team
                        });
                    }

                }

                return gameResults;
            }
        }
    }
}