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
                                                   Genres = m.Genres.Select(g => g.Name).ToList(),
                                               }).ToList();

                this.directorsGrid.ItemsSource = (from d in db.Directors
                                                  select new
                                                  {
                                                      Id = d.Id,
                                                      FirstName = d.FirstName,
                                                      LastName = d.LastName
                                                  }).ToList();

                this.genresGrid.ItemsSource = (from g in db.Genres
                                               select new
                                               {
                                                   Id = g.Id,
                                                   Genre = g.Name
                                               }).ToList();
            }
        }
    }
}
