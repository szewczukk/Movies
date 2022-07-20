using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoviesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class MovieViewModel
        {
            public string Name { get; set; }
            public string Director { get; set; }
            public string Genres { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            using (var db = new DatabaseContext())
            {
                var movies = (from m in db.Movies
                             select new
                             {
                                 Name = m.Name,
                                 DirectorFirstName = m.Director.FirstName,
                                 DirectorLastName = m.Director.LastName,
                                 Genres = m.Genres.Select(g => g.Name).ToList(),
                             }).ToList();

                var moviesList = new List<MovieViewModel>();
                foreach (var movie in movies)
                {
                    moviesList = moviesList.Append(new MovieViewModel
                    {
                        Name = movie.Name,
                        Director = $"{movie.DirectorFirstName} {movie.DirectorLastName}",
                        Genres = string.Join(", ", movie.Genres)
                    }).ToList();
                }

                this.grid.ItemsSource = moviesList;
            }
        }
    }
}
