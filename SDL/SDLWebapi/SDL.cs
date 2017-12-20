using Microsoft.EntityFrameworkCore;
using SDLModels;

namespace SDLWebapi
{
    public class SDL : DbContext
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
            modelBuilder.Entity<Alias>()
                .HasKey(a => a.Name);

            modelBuilder.Entity<Anime>()
                .HasKey(a => new { a.AliasName, a.Season });
            modelBuilder.Entity<Book>()
                .HasKey(b => new { b.AliasName, b.Volume });
            modelBuilder.Entity<Game>()
                .HasKey(g => g.FullName);
            modelBuilder.Entity<Manga>()
                .HasKey(m => new { m.AliasName, m.Volume });
            modelBuilder.Entity<Movie>()
                .HasKey(m => new { m.AliasName, m.Season });
            modelBuilder.Entity<ONA>()
                .HasKey(o => new { o.AliasName, o.Season });
            modelBuilder.Entity<OVA>()
                .HasKey(o => new { o.AliasName, o.Season });
            modelBuilder.Entity<Special>()
                .HasKey(s => new { s.AliasName, s.Season });

            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.Animes)
                .WithOne(anime => anime.Alias)
                .HasForeignKey(anime => anime.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.Movies)
                .WithOne(movie => movie.Alias)
                .HasForeignKey(movie => movie.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.ONAs)
                .WithOne(ona => ona.Alias)
                .HasForeignKey(ona => ona.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.OVAs)
                .WithOne(ova => ova.Alias)
                .HasForeignKey(ova => ova.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.Specials)
                .WithOne(special => special.Alias)
                .HasForeignKey(special => special.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.Mangas)
                .WithOne(manga => manga.Alias)
                .HasForeignKey(manga => manga.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.Books)
                .WithOne(book => book.Alias)
                .HasForeignKey(book => book.AliasName)
                .HasPrincipalKey(alias => alias.Name);
            modelBuilder.Entity<Alias>()
                .HasMany(alias => alias.Games)
                .WithOne(game => game.Alias)
                .HasForeignKey(game => game.AliasName)
                .HasPrincipalKey(alias => alias.Name);

        }
    }
}
