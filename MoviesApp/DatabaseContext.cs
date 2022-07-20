using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoviesApp.Models;
using MoviesApp.Initializers;

namespace MoviesApp
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DatabaseContext() : base("DatabaseContext")
        {
            Database.SetInitializer<DatabaseContext>(new MoviesInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Movie>()
                        .HasMany<Genre>(m => m.Genres)
                        .WithMany(g => g.Movies)
                        .Map(mg =>
                        {
                            mg.MapLeftKey("MovieRefId");
                            mg.MapLeftKey("GenreRefId");
                            mg.ToTable("MovieGenre");
                        });
        }
    }
}
