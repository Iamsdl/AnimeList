using Microsoft.AspNetCore.Mvc;
using SDLModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SDLWebapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Aliases")]
    public class AliasesController : Controller
    {
        private SDL _SDL = new SDL(new DbContextOptions<SDL>());
        public AliasesController(SDL context)
        {
            _SDL = context;
        }

        [HttpPost("Add/{aliasName}")]
        public string Add(string aliasName)
        {
#warning normalize names?
            if (!_SDL.Aliases.Any(a=>a.Name==aliasName))
            {
                Alias alias = new Alias() { Name = aliasName };
                _SDL.Aliases.Add(alias);
                return "Success.\nAlias has been added in the database.";
            }
        }
    }
}
