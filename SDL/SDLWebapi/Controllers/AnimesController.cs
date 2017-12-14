using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SDLModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SDLWebapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Animes")]
    public class AnimesController : Controller
    {
        private SDL _SDL = new SDL(new DbContextOptions<SDL>());
        public AnimesController(SDL context)
        {
            _SDL = context;
        }

        /// <summary>
        /// Returns all the animes in the database in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public string GetAll()
        {
            try
            {
                if (!_SDL.Animes.Any())
                {
                    return "Failed.\nThere are no animes in the database.";
                }
                string jsonAnimes = JsonConvert.SerializeObject(_SDL.Animes);
                return "Success.\n" + jsonAnimes;
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }

        /// <summary>
        /// Returns all the animes in the database for a given alias in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFor/{aliasName}")]
        public string GetAllFor(string aliasName)
        {
            try
            {
                var animes = _SDL.Animes.Where(a => a.AliasName == aliasName);
                if (!animes.Any())
                {
                    return "Failed.\nThere are no animes with this alias in the database.";
                }
                string jsonAnimes = JsonConvert.SerializeObject(animes);
                return "Success.\n" + jsonAnimes;
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }

        /// <summary>
        /// Returns the anime in the database for the given aliasName and season in JSON format.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        [HttpGet("Get/{aliasName}, season {season}")]
        public string Get(string aliasName, short season)
        {
            try
            {
                var anime = _SDL.Animes.Find(aliasName, season);
                if (anime == null)
                {
                    return "Failed.\nCould not find this anime.";
                }
                string jsonAnimes = JsonConvert.SerializeObject(anime);
                return "Success.\n" + jsonAnimes;
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }

        /// <summary>
        /// Adds a new anime in the database. If an alias for that anime does not exist, it first creates the alias.
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public string Add([FromBody] Anime anime)
        {
            try
            {
                Alias alias = _SDL.Aliases.Find(anime.AliasName);

                if (alias == null)
                {
#warning use AliasController to add a new alias.
                    //alias = new Alias() { Name = anime.AliasName };
                    //_SDL.Aliases.Add(alias);
                }
                else
                {
                    if (alias.Animes.Any(a => a.Season == anime.Season))
                    {
                        return "Failed.\nAnime already exists.";
                    }
                }
                _SDL.Animes.Add(anime);
                _SDL.SaveChanges();
                return "Success.\nAnime has been added in the database.";
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }

        [HttpPut("Edit/{aliasName}, season {season}")]
        public string Edit(string aliasName, short season, [FromBody] Anime modifiedAnime)
        {
            var initialAnime = _SDL.Animes.Find(modifiedAnime.AliasName, modifiedAnime.Season);
            if (initialAnime != null)
            {
                initialAnime.AliasName = modifiedAnime.AliasName;
                initialAnime.FullName = modifiedAnime.FullName;
                initialAnime.Season = modifiedAnime.Season;
                initialAnime.SeenEpisodes = modifiedAnime.SeenEpisodes;
                initialAnime.TotalEpisodes = modifiedAnime.TotalEpisodes;

                _SDL.Update(initialAnime);
                _SDL.SaveChanges();
                return "Success.";
            }
            return "Failed.";
        }

        [HttpDelete("DeleteAll")]
        public string DeleteAll()
        {
            try
            {
                if (!_SDL.Animes.Any())
                {
                    return "There are no animes in the database to delete.";
                }
                _SDL.Animes.RemoveRange(_SDL.Animes);
                return "Success. All the animes in the database have been deleted.";
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }

        [HttpDelete("DeleteAllFor/{aliasName}")]
        public string DeleteAllFor(string aliasName)
        {
            return "";
        }

        [HttpDelete("Delete/{aliasName}, season {season}")]
        public string Delete(string aliasName, short season)
        {

            return "";
        }
    }
}
