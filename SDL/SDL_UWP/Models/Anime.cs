using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL_UWP.Models
{
    class Anime
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenEpisodes { get; set; }
        public ushort TotalEpisodes { get; set; }

        public virtual Alias Alias { get; set; }

    }
}
