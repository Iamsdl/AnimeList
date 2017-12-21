using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SDLWebapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private SDL _SDL = new SDL(new DbContextOptions<SDL>());
        public BooksController(SDL context)
        {
            _SDL = context;
        }

        /// <summary>
        /// Returns all the books in JSON format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public string GetAll()
        {
            try
            {
                if (!_SDL.Books.Any())
                {
                    return "There are no books in the database.";
                }
                string jsonBooks = JsonConvert.SerializeObject(_SDL.Books);
                return "Success. " + jsonBooks;
            }
            catch (Exception e)
            {
                return "Failed. " + e.Message;
            }
        }
    }
}