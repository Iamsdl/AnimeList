﻿using Microsoft.AspNetCore.Mvc;
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
            string jsonAnimes = JsonConvert.SerializeObject(_SDL.Animes);
            return jsonAnimes;
        }

        /// <summary>
        /// Returns all the animes in the database for a given alias in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFor/{aliasName}")]
        public string Get(string aliasName)
        {
            var alias = _SDL.Aliases.Include(a => a.Animes).Where(a => a.Name == aliasName).FirstOrDefault();
            var aliasAnimes = alias.Animes.Select(anime => new { anime.AliasName, anime.FullName, anime.Season, anime.SeenEpisodes, anime.TotalEpisodes });
            string jsonAnimes = JsonConvert.SerializeObject(aliasAnimes);
            return jsonAnimes;
        }

        [HttpGet("Get/{aliasName}, season {season}")]
        public string Get(string aliasName,short season)
        {
            string jsonAnimes = JsonConvert.SerializeObject(_SDL.Animes.Find(aliasName,season));
            return jsonAnimes;
        }

        [HttpPost]
        public string Post([FromBody] Anime anime)
        {
            Alias alias = _SDL.Aliases.Find(anime.AliasName);

            if (alias == null)
            {
                alias = new Alias() { Name = anime.AliasName };
                _SDL.Aliases.Add(alias);
            }
            _SDL.Animes.Add(anime);
            _SDL.SaveChanges();

            return "Success.";
        }

        [HttpPut]
        public string Put([FromBody] Anime modifiedAnime)
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
    }
}
