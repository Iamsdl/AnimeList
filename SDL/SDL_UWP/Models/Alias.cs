using System.Collections.Generic;

namespace SDL_UWP.Models
{
    class Alias
    {
        public Alias()
        {
            Animes = new HashSet<Anime>();
            OVAs = new HashSet<OVA>();
            Mangas = new HashSet<Manga>();
            Games = new HashSet<Game>();
        }

        public string Name { get; set; }
        public virtual ICollection<Anime> Animes { get; set; }
        public virtual ICollection<OVA> OVAs { get; set; }
        public virtual ICollection<Manga> Mangas { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

    }
}
