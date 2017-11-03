using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL_UWP.Models
{
    class Manga
    {
        public string FullName { get; set; }
        public ushort Volume { get; set; }
        public ushort ReadChapters { get; set; }
        public ushort TotalChapters { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
