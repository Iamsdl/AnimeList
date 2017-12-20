using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using SDLModels;

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
            try
            {
                if (_SDL.Aliases.Any(a => a.Name == aliasName))
                {
                    return "Failed.\n Alias already exists.";
                }
                Alias alias = new Alias() { Name = aliasName };
                _SDL.Aliases.Add(alias);
                _SDL.SaveChanges();
                return "Success.\nAlias has been added in the database.";
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }
        //public string Delete(SDL context)
        //{
        //    try
        //    {
        //        if (Animes.Any())
        //        {
        //            context.Animes.RemoveRange(Animes);
        //        }
        //        if (Books.Any())
        //        {
        //            context.Books.RemoveRange(Books);
        //        }
        //        if (Games.Any())
        //        {
        //            context.Games.RemoveRange(Games);
        //        }
        //        if (Mangas.Any())
        //        {
        //            context.Mangas.RemoveRange(Mangas);
        //        }
        //        if (Movies.Any())
        //        {
        //            context.Movies.RemoveRange(Movies);
        //        }
        //        if (ONAs.Any())
        //        {
        //            context.ONAs.RemoveRange(ONAs);
        //        }
        //        if (OVAs.Any())
        //        {
        //            context.OVAs.RemoveRange(OVAs);
        //        }
        //        if (Specials.Any())
        //        {
        //            context.Specials.RemoveRange(Specials);
        //        }

        //        context.Aliases.Remove(this);
        //        return $"Success.\nThe {Name} alias and all related entities have been deleted.";
        //    }
        //    catch (Exception e)
        //    {
        //        return "Failed.\n" + e.Message;
        //    }
        //}
    }
}
