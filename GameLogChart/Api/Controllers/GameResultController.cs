using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DomainModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/results")]
    public class GameResultController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGameResultRepository _repository;
        
        public GameResultController(
            IGameResultRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a team's game results for the specified year.
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        /// GET /results/nya/2017
        /// </remarks>
        /// 
        ///<param name="team">The three letter team code.</param>
        /// <param name="year">The four digit year.</param>
        /// <returns>A list of game results.</returns>
        /// <response code="200">Returns the list of game results.</response>
        /// <response code="404">There are no results for the specified team and year.</response>
        [HttpGet("{team}/{year}")]
        [ProducesResponseType(typeof(ApiModels.GameResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string team, short year)
        {
            var gameResultsDataModel = (await _repository.GetGameResultsAsync(team, year)).ToList();

            if (!gameResultsDataModel.Any())
            {
                return NotFound();
            }

            var gameResultDomainModels = _mapper.Map<IEnumerable<GameResult>>(gameResultsDataModel);
            var gameResultApiModel = _mapper.Map<ApiModels.GameResult>(gameResultDomainModels);

            return Ok(gameResultApiModel);
        }
    }
}