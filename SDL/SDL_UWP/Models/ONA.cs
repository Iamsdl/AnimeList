using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL_UWP.Models
{
    class ONA
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenONAs { get; set; }
        public ushort TotalONAs { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
