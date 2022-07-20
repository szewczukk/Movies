using System.Collections.Generic;
using System.Data.Entity;
using MoviesApp.Models;

namespace MoviesApp.Initializers
{
    public class MoviesInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Adventure"
                },
                new Genre
                {
                    Name = "Action"
                },
                new Genre
                {
                    Name = "Comedy"
                },
            };

            var directors = new List<Director>
            {
                new Director
                {
                    FirstName = "Quentin",
                    LastName = "Tarantino",
                },
                new Director
                {
                    FirstName = "Ricky",
                    LastName = "Gervais",
                }
            };

            var movies = new List<Movie>
            {
                new Movie
                {
                    Name = "Kill Bill",
                    Director = directors[0],
                    Genres = new List<Genre> { genres[0], genres[1] }
                },
                new Movie
                {
                    Name = "The Office",
                    Director = directors[1],
                    Genres = new List<Genre> { genres[2] }
                },
            };

            var reviews = new List<Review>
            {
                new Review
                {
                    Content = "I liked it very much",
                    Movie = movies[0],
                },
                new Review
                {
                    Content = "Noice",
                    Movie = movies[1],
                }
            };

            context.Genres.AddRange(genres);
            context.Directors.AddRange(directors);
            context.Movies.AddRange(movies);
            context.Reviews.AddRange(reviews);

            context.SaveChanges();
        }
    }
}
