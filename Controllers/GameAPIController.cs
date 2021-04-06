using Microsoft.AspNetCore.Mvc;
using MineweeperWebApplication.Models;
using MineweeperWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace MineweeperWebApplication.Controllers
{
    [ApiController]
    [Route("api/showSavedGames")]
    public class GameAPIController : ControllerBase
    {
        GameDataService service = new GameDataService();

        [HttpGet]
        [ResponseType(typeof(List<Game>))]
        public List<Game> Index()
        {
            return service.ViewAll();
        }

        [HttpGet("{Id}")]
        [ResponseType(typeof(Game))]
        public Game ShowDetails(int id)
        {
            return service.ViewOne(id);
        }

        [HttpDelete("{gameId}")]
        public List<Game> Delete(int gameId)
        {
            service.Delete(gameId);

            return Index();
        }
    }
}
