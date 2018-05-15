using System.Collections.Generic;

namespace Api.ApiModels
{
    public class GameResult
    {
        public GameResult()
        {
            GameResults = new List<DomainModel.GameResult>();
        }

        public int Count => GameResults.Count;


        public List<DomainModel.GameResult> GameResults { get; }
    }
}