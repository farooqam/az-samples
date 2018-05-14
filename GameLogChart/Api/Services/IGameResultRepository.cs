using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataModels;

namespace Api.Services
{
    public interface IGameResultRepository
    {
        Task<IEnumerable<GameResult>> GetGameResultsAsync(string team, short year);
    }
}