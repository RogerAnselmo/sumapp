using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SumApp.API.Repositories.Context;

namespace SumApp.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly SumAppContext _context;

        public HomeController(SumAppContext context) => _context = context;


        [HttpGet]
        [Route(nameof(ServerShouldExist))]
        public ActionResult ServerShouldExist() =>
            StatusCode(StatusCodes.Status200OK);

        [HttpGet]
        [Route(nameof(DataBaseShouldExist))]
        public ActionResult DataBaseShouldExist() =>
            ((RelationalDatabaseCreator)_context.Database.GetService<IDatabaseCreator>()).Exists()
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status500InternalServerError);
    }
}