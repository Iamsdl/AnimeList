using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SDLModels;
using System;
using System.Linq;

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
        /// Returns all the animes in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public string GetAll()
        {
            try
            {
                if (!_SDL.Animes.Any())
                {
                    return "There are no animes in the database.";
                }
                string jsonAnimes = JsonConvert.SerializeObject(_SDL.Animes);
                return "Success. " + jsonAnimes;
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Returns all the animes for a given alias in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFor/{name}")]
        public string GetAllFor(string name)
        {
            try
            {
                var animes = _SDL.Animes.Where(a => a.AliasName.Contains(name)||a.FullName.Contains(name));
                if (!animes.Any())
                {
                    return $"There are no animes with the {name} alias in the database.";
                }
                string jsonAnimes = JsonConvert.SerializeObject(animes);
                return "Success. " + jsonAnimes;
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Returns a single anime in JSON format.
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
                    return $"Could not find {aliasName}, season {season} anime.";
                }
                string jsonAnimes = JsonConvert.SerializeObject(anime);
                return "Success. " + jsonAnimes;
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Adds a new anime. If an alias for that anime does not exist, it first creates the alias.
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public string Add([FromBody] Anime anime)
        {
            try
            {
                Alias alias = _SDL.Aliases.Include(a => a.Animes).Where(a => a.AliasName== anime.AliasName).FirstOrDefault();

                if (alias == null)
                {
                    alias = new Alias() { AliasName = anime.AliasName };
                    _SDL.Aliases.Add(alias);
                }
                else
                {
                    if (alias.Animes.Any(a => a.Season == anime.Season))
                    {
                        return $"Failed. {anime.AliasName}, season {anime.Season} anime already exists.";
                    }
                }
                _SDL.Animes.Add(anime);
                _SDL.SaveChanges();
                return $"Success. {anime.AliasName}, season {anime.Season} anime has been added.";
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Edits an existing anime. If an alias for the editedAnime does not exist, it first creates the alias.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="season"></param>
        /// <param name="editedAnime"></param>
        /// <returns></returns>
        [HttpPut("Edit/{aliasName}, season {season}")]
        public string Edit(string aliasName, short season, [FromBody] Anime editedAnime)
        {
            try
            {
                var initialAnime = _SDL.Animes.Find(aliasName, season);
                if (initialAnime == null)
                {
                    return $"Failed.\nCould not find {aliasName}, season {season} anime to edit.";
                }
                Anime existingAnime = _SDL.Animes.Find(editedAnime.AliasName, editedAnime.Season);
                if (existingAnime != null)
                {
                    if (existingAnime != initialAnime)
                    {
                        return $"Failed. {editedAnime.AliasName}, season {editedAnime.Season} anime already exists.";
                    }
                    else
                    {
                        //if existingAnime==initialAnime then we know for sure that the alias exists so we skip the check.
                        goto Edit;
                    }
                }
                Alias alias = _SDL.Aliases.Find(editedAnime.AliasName);
                if (alias == null)
                {
                    alias = new Alias() { AliasName = editedAnime.AliasName };
                    _SDL.Aliases.Add(alias);
                }

                Edit:
                initialAnime.AliasName = editedAnime.AliasName;
                initialAnime.FullName = editedAnime.FullName;
                initialAnime.Season = editedAnime.Season;
                initialAnime.SeenEpisodes = editedAnime.SeenEpisodes;
                initialAnime.TotalEpisodes = editedAnime.TotalEpisodes;

                _SDL.Update(initialAnime);
                _SDL.SaveChanges();
                return $"Success.\nThe {aliasName}, season {season} anime has been changed.";
            }
            catch(Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Deletes all the animes.
        /// </summary>
        /// <returns></returns>
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
                _SDL.SaveChanges();
                return "Success.\nAll the animes in the database have been deleted.";
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Deletes all the animes for a given alias.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAllFor/{aliasName}")]
        public string DeleteAllFor(string aliasName)
        {
            try
            {
                var animes = _SDL.Animes.Where(a => a.AliasName == aliasName);
                if (!animes.Any())
                {
                    return $"There are no animes with the {aliasName} alias to delete.";
                }

                _SDL.Animes.RemoveRange(animes);
                _SDL.SaveChanges();
                return $"Success.\nAll the animes with the {aliasName} alias have been deleted.";
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }

        /// <summary>
        /// Deletes a single anime.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{aliasName}, season {season}")]
        public string Delete(string aliasName, short season)
        {
            try
            {
                Anime anime = _SDL.Animes.Find(aliasName, season);
                if (anime==null)
                {
                    return $"Could not find {aliasName}, season {season} anime to delete.";
                }

                _SDL.Animes.Remove(anime);
                _SDL.SaveChanges();
                return $"Success. {aliasName}, season {season} anime has been deleted.";
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }
    }
}
