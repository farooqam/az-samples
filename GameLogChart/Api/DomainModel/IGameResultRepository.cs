using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.DomainModel
{
    public interface IGameResultRepository
    {
        Task<IEnumerable<DataModels.GameResult>> GetGameResultsAsync(string team, short year);
    }
}