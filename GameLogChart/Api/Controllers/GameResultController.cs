using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/gamelogs")]
    public class GameResultController : Controller
    {
        private readonly IGameResultRepository _repository;
        private readonly IMapper _mapper;

        public GameResultController(
            IGameResultRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet("{team}/{year}")]
        public async Task<IActionResult> Get(string team, short year)
        {
            var gameResultsDataModel = (await _repository.GetGameResultsAsync(team, year)).ToList();

            if (!gameResultsDataModel.Any())
            {
                return NotFound();
            }

            var gamerResultDomainModel = _mapper.Map<IEnumerable<DomainModel.GameResult>>(gameResultsDataModel);
            return Ok(gamerResultDomainModel);
        }
    }
}