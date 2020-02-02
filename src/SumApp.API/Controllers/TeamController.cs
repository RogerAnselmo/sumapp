using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SumApp.API.Models;
using SumApp.API.Repositories;

namespace SumApp.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamRepository _repository;
        public TeamController(TeamRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<ObjectResult> Get() => Ok(await _repository.Get());

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> Get(int id) => Ok(await _repository.Get(id));

        [HttpPost]
        public async Task<HttpStatusCode> Post(Team team) =>
            await _repository.SaveAsync(team) ? 
                HttpStatusCode.Created : 
                HttpStatusCode.InternalServerError;

        [HttpPut]
        public async Task<HttpStatusCode> Put([FromBody] Team team) => 
            await _repository.UpdateAsync(team) ? 
                HttpStatusCode.OK : 
                HttpStatusCode.InternalServerError;

        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> Delete(int id) => 
            await _repository.RemoveAsync(id) ? 
                HttpStatusCode.OK : 
                HttpStatusCode.InternalServerError;
    }
}