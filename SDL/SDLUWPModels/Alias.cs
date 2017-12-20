using System.Collections.Generic;

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

        
    }
}
