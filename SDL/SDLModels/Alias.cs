using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDLModels
{
    public class Alias
    {
        public Alias()
        {
            Animes = new HashSet<Anime>();
            Books = new HashSet<Book>();
            Games = new HashSet<Game>();
            Mangas = new HashSet<Manga>();
            Movies = new HashSet<Movie>();
            ONAs = new HashSet<ONA>();
            OVAs = new HashSet<OVA>();
            Specials = new HashSet<Special>();
        }

        public string Name { get; set; }
        public virtual ICollection<Anime> Animes { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Manga> Mangas { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<ONA> ONAs { get; set; }
        public virtual ICollection<OVA> OVAs { get; set; }
        public virtual ICollection<Special> Specials { get; set; }

        public string Delete(SDL context)
        {
            try
            {
                if (Animes.Any())
                {
                    context.Animes.RemoveRange(Animes);
                }
                if (Books.Any())
                {
                    context.Books.RemoveRange(Books);
                }
                if (Games.Any())
                {
                    context.Games.RemoveRange(Games);
                }
                if (Mangas.Any())
                {
                    context.Mangas.RemoveRange(Mangas);
                }
                if (Movies.Any())
                {
                    context.Movies.RemoveRange(Movies);
                }
                if (ONAs.Any())
                {
                    context.ONAs.RemoveRange(ONAs);
                }
                if (OVAs.Any())
                {
                    context.OVAs.RemoveRange(OVAs);
                }
                if (Specials.Any())
                {
                    context.Specials.RemoveRange(Specials);
                }

                context.Aliases.Remove(this);
                return $"Success.\nThe {Name} alias and all related entities have been deleted.";
            }
            catch (Exception e)
            {
                return "Failed.\n" + e.Message;
            }
        }
    }
}
