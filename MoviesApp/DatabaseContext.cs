using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            var dbFile = ConfigurationManager.AppSettings["dbFile"];
            if (dbFile != null)
            {
                optionsBuilder.UseSqlite(dbFile);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Movie>()
                .HasMany<Genre>(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity(e => e.HasData(
                    new
                    {
                        MoviesId = -1,
                        GenresId = -1,
                    },
                    new
                    {
                        MoviesId = -1,
                        GenresId = -2,
                    },
                    new
                    {
                        MoviesId = -2,
                        GenresId = -3,
                    }
                ));

            modelBuilder.Entity<Genre>().HasData(
                new
                {
                    Id = -1,
                    Name = "Adventure"
                },
                new
                {
                    Id = -2,
                    Name = "Action"
                },
                new
                {
                    Id = -3,
                    Name = "Comedy"
                }
            );
            modelBuilder.Entity<Director>().HasData(
                new
                {
                    Id = -1,
                    FirstName = "Quentin",
                    LastName = "Tarantino",
                },
                new
                {
                    Id = -2,
                    FirstName = "Ricky",
                    LastName = "Gervais",
                }
            );
            modelBuilder.Entity<Movie>().HasData(
                new
                {
                    Id = -1,
                    Name = "Kill Bill",
                    DirectorId = -1,
                },
                new
                {
                    Id = -2,
                    Name = "The Office",
                    DirectorId = -2,
                }
            );
            modelBuilder.Entity<Review>().HasData(
                new
                {
                    Id = -1,
                    Content = "I liked it very much",
                    MovieId = -1,
                },
                new
                {
                    Id = -2,
                    Content = "Noice",
                    MovieId = -2,
                }
            );
        }
    }
}
