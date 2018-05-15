using System.Collections.Generic;
using AutoMapper;

namespace Api.ApiModels
{
    public class GameResultTypeConverter : ITypeConverter<IEnumerable<DomainModel.GameResult>, GameResult>
    {
        public GameResult Convert(IEnumerable<DomainModel.GameResult> source, GameResult destination,
            ResolutionContext context)
        {
            var gameResultFromApi = new GameResult();
            gameResultFromApi.GameResults.AddRange(source);

            return gameResultFromApi;
        }
    }
}