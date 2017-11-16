using Microsoft.EntityFrameworkCore;

namespace SDLModels
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
            optionsBuilder.UseSqlServer("Data Source=DSCHMIDT\\SQLEXPRESS;Initial Catalog=SDL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public virtual DbSet<Alias> Aliases { get; set; }
        public virtual DbSet<Anime> Animes { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Manga> Mangas { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ONA> ONAs { get; set; }
        public virtual DbSet<OVA> OVAs { get; set; }
        public virtual DbSet<Special> Specials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alias>().
                HasKey(a => a.Name);

            modelBuilder.Entity<Alias>()
                .HasMany(a => a.Animes)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.Movies)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.ONAs)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.OVAs)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.Specials)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.Mangas)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.Books)
                .WithOne(a => a.Alias);
            modelBuilder.Entity<Alias>()
                .HasMany(a => a.Games)
                .WithOne(a => a.Alias);

            modelBuilder.Entity<Anime>()
                .HasKey(a => new { a.Alias, a.Season });
        }
    }
}
