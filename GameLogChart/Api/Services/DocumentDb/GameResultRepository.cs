using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DomainModel;
using AutoMapper;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Api.Services.DocumentDb
{
    public class GameResultRepository : IGameResultRepository
    {
        private readonly IMapper _mapper;
        private readonly DocumentDbRepositorySettings _settings;

        public GameResultRepository(
            DocumentDbRepositorySettings settings,
            IMapper mapper)
        {
            _settings = settings;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DataModels.GameResult>> GetGameResultsAsync(string team, short year)
        {
            using (var client = new DocumentClient(_settings.Endpoint, _settings.Key))
            {
                var queryString =
                    $"SELECT c.game_year, c.game_number, c.game_month, c.game_day, c.home_team, c.visiting_team, c.home_team_score, c.visting_team_score AS visiting_team_score " +
                    $"FROM c WHERE (c.home_team = @team OR c.visiting_team = @team) " +
                    $"AND c.game_year = @year";

                var queryParameters = new SqlParameterCollection
                {
                    new SqlParameter("@team", team.ToUpperInvariant()),
                    new SqlParameter("@year", year)
                };


                var query = client.CreateDocumentQuery<GameResult>(
                        UriFactory.CreateDocumentCollectionUri("gamelogs", "gamelog"),
                        new SqlQuerySpec(queryString, queryParameters),
                        new FeedOptions {MaxItemCount = 200, EnableCrossPartitionQuery = false})
                    .AsDocumentQuery();

                var gameResults = new List<DataModels.GameResult>();

                while (query.HasMoreResults)
                {
                    foreach (var gameResult in await query.ExecuteNextAsync<GameResult>())
                    {
                        gameResults.Add(_mapper.Map<DataModels.GameResult>(gameResult));
                    }
                }

                return gameResults.AsEnumerable();
            }
        }
    }
}