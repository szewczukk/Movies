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
            optionsBuilder.UseSqlite("Filename=database.sqlite");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new
                {
                    GenreId = -1,
                    Name = "Action"
                },
                new
                {
                    GenreId = -2,
                    Name = "Comedy",
                }
            );
            modelBuilder.Entity<Director>().HasData(
                new
                {
                    DirectorId = -1,
                    FirstName = "Quentin",
                    LastName = "Tarantino",
                },
                new
                {
                    DirectorId = -2,
                    FirstName = "Ricky",
                    LastName = "Gervais",
                }
            );
            modelBuilder.Entity<Movie>().HasData(
                new
                {
                    MovieId = -1,
                    Title = "Kill Bill",
                    DirectorId = -1,
                    GenreId = -1,
                },
                new
                {
                    MovieId = -2,
                    Title = "The Office",
                    DirectorId = -2,
                    GenreId = -2,
                }
            );
            modelBuilder.Entity<Review>().HasData(
                new
                {
                    ReviewId = -1,
                    Content = "I liked it very much",
                    MovieId = -1,
                },
                new
                {
                    ReviewId = -2,
                    Content = "Noice",
                    MovieId = -2,
                }
            );
        }
    }
}
