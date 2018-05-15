using System.Collections.Generic;
using AutoMapper;

namespace Api.ApiModels
{
    public class ApiResultTypeConverter : ITypeConverter<IEnumerable<GameResult>, ApiResult<GameResult>>
    {
        public ApiResult<GameResult> Convert(IEnumerable<GameResult> source, ApiResult<GameResult> destination,
            ResolutionContext context)
        {
            var apiResult = new ApiResult<GameResult>();
            apiResult.Results.AddRange(source);
            return apiResult;
        }
    }
}