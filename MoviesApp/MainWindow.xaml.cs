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
using MoviesApp.Models;

namespace MoviesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var db = new DatabaseContext())
            {
                this.moviesGrid.ItemsSource = (from m in db.Movies
                                               select new
                                               {
                                                   Id = m.Id,
                                                   Name = m.Name,
                                                   Director = m.Director,
                                                   Genres = m.Genres.Select(g => new
                                                   {
                                                       Name = g.Name
                                                   }).ToList(),
                                               }).ToList();

                var directors = (from d in db.Directors
                                 select new
                                 {
                                     Id = d.Id,
                                     FirstName = d.FirstName,
                                     LastName = d.LastName
                                 }).ToList();

                var genres = (from g in db.Genres
                              select new
                              {
                                  Id = g.Id,
                                  Name = g.Name
                              }).ToList();

                this.directorsGrid.ItemsSource = directors;
                this.directorsComboBox.ItemsSource = directors;
                this.genresGrid.ItemsSource = genres;
                this.genresListBox.ItemsSource = genres;
            }
        }

        private void CreateMovieButtonClicked(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var rawDirector = (int)this.directorsComboBox.SelectedValue;
                var director = (from d in db.Directors where d.Id == rawDirector select d).ToList()[0];

                var rawGenres = this.genresListBox.SelectedItems;

                var genres = new List<Genre>();

                foreach (var g in this.genresListBox.SelectedItems)
                {
                    genres.Add(new Genre()
                    {
                        Id = (int)g.GetType().GetProperty("Id").GetValue(g, null),
                        Name = (string)g.GetType().GetProperty("Name").GetValue(g, null)
                    });
                }

                var movie = new Movie()
                {
                    Name = this.movieName.Text,
                    Director = new Director()
                    {
                        Id = rawDirector,
                        FirstName = director.FirstName,
                        LastName = director.LastName
                    },
                    Genres = genres
                };

                db.Movies.Add(movie);
                db.SaveChanges();

                this.moviesGrid.ItemsSource = (from m in db.Movies
                                               select new
                                               {
                                                   Id = m.Id,
                                                   Name = m.Name,
                                                   Director = m.Director,
                                                   Genres = m.Genres.Select(g => new
                                                   {
                                                       Name = g.Name
                                                   }).ToList(),
                                               }).ToList();
            }
        }
    }
}
