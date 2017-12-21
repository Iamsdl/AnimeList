using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using SDLModels;
using Newtonsoft.Json;

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

        /// <summary>
        /// Returns all the aliases in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public string GetAll()
        {
            try
            {
                if (!_SDL.Aliases.Any())
                {
                    return "There are no aliases in the database.";
                }
                string jsonAliases = JsonConvert.SerializeObject(
                    _SDL.Aliases
                    .Include(a => a.Animes)
                    .Include(a => a.Books)
                    .Include(a => a.Games)
                    .Include(a => a.Mangas)
                    .Include(a => a.Movies)
                    .Include(a => a.ONAs)
                    .Include(a => a.OVAs)
                    .Include(a => a.Specials)
                , new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return "Success. " + jsonAliases;
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
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
