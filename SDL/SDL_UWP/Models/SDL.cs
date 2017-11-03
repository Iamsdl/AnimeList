using Microsoft.EntityFrameworkCore;

namespace SDL_UWP.Models
{
    class SDL : DbContext
    {
        public SDL(DbContextOptions<SDL> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DSCHMIDT\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public virtual DbSet<Alias> Names { get; set; }
        public virtual DbSet<Anime> Animes { get; set; }
        public virtual DbSet<OVA> OVAs{ get; set; }
        public virtual DbSet<Manga> Mangas { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
    }
}
