using System.Net;
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
        public ObjectResult Get() => Ok(_repository.Get());

        [HttpGet("{id:int}")]
        public ObjectResult Get(int id) => Ok(_repository.Get(id));

        [HttpPost]
        public HttpStatusCode Post(Team team) =>
            _repository.Save(team) ? 
                HttpStatusCode.Created : 
                HttpStatusCode.InternalServerError;

        [HttpPut]
        public HttpStatusCode Put([FromBody] Team team) => 
            _repository.Update(team) ? 
                HttpStatusCode.OK : 
                HttpStatusCode.InternalServerError;

        [HttpDelete("{id}")]
        public HttpStatusCode Delete(int id) => 
            _repository.Remove(id) ? 
                HttpStatusCode.OK : 
                HttpStatusCode.InternalServerError;
    }
}