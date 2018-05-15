using System.Collections.Generic;

namespace Api.ApiModels
{
    public class ApiResult<T> where T : class, new()
    {
        public ApiResult()
        {
            Results = new List<T>();
        }

        public List<T> Results { get; }
        public int ResultCount => Results.Count;
    }
}