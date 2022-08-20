using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.EntityFrameworkCore;

namespace MoviesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext databaseContext = new();

        private readonly CollectionViewSource genresViewSource;
        private readonly CollectionViewSource moviesViewSource;
        private readonly CollectionViewSource directorsViewSource;

        public MainWindow()
        {
            InitializeComponent();

            genresViewSource = (CollectionViewSource)FindResource(nameof(genresViewSource));
            moviesViewSource = (CollectionViewSource)FindResource(nameof(moviesViewSource));
            directorsViewSource = (CollectionViewSource)FindResource(nameof(directorsViewSource));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            databaseContext.Database.EnsureCreated();

            databaseContext.Genres.Load();
            databaseContext.Movies.Load();
            databaseContext.Directors.Load();

            genresViewSource.Source = databaseContext.Genres.Local.ToObservableCollection();
            moviesViewSource.Source = databaseContext.Movies.Local.ToObservableCollection();
            directorsViewSource.Source = databaseContext.Directors.Local.ToObservableCollection();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            databaseContext.Dispose();

            base.OnClosing(e);
        }

        private void SaveMenuItem_Clicked(object sender, RoutedEventArgs e)
        {
            databaseContext.SaveChanges();

            moviesGrid.Items.Refresh();
            genresGrid.Items.Refresh();
            directorsGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var movieId = ((Button)sender).DataContext as int?;

            if (movieId.HasValue)
            {
                var reviewsWindow = new ReviewsWindow(movieId.Value);

                reviewsWindow.Show();
            }
        }

        private T? GetSelectedItemsProperty<T>(object selectedItem, string property)
        {
            return (T)selectedItem.GetType().GetProperty(property).GetValue(selectedItem, null);
        }

        private void NewMovieButton_Clicked(object sender, RoutedEventArgs e)
        {
            var title = this.NewMovieTitlesComboBox.Text;
            var directorsId = GetSelectedItemsProperty<int>(this.NewMovieDirectorsComboBox.SelectedItem, "DirectorId");
            var genresId = GetSelectedItemsProperty<int>(this.NewMovieGenresComboBox.SelectedItem, "GenreId");


            databaseContext.Movies.Add(
                new Movie
                {
                    Title = title,
                    DirectorId = directorsId,
                    GenreId = genresId,
                }
            );

            databaseContext.SaveChanges();
            this.moviesGrid.Items.Refresh();
        }
    }
}
