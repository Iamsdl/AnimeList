using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL_UWP.Models
{
    class Movie
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenMovies { get; set; }
        public ushort TotalMovies { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
