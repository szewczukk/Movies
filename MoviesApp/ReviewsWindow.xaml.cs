using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {
        private readonly DatabaseContext databaseContext = new();

        private readonly CollectionViewSource reviewsViewSource;

        private readonly int MovieId;

        public ReviewsWindow(int movieId)
        {
            InitializeComponent();

            this.MovieId = movieId;

            reviewsViewSource = (CollectionViewSource)FindResource(nameof(reviewsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            databaseContext.Database.EnsureCreated();

            databaseContext.Reviews.Load();
            databaseContext.Movies.Load();

            reviewsViewSource.Source = databaseContext.Reviews.Local
                .Where(r => r.MovieId == this.MovieId).ToList();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            databaseContext.Dispose();

            base.OnClosing(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var review = this.textBox.Text;

            databaseContext.Reviews.Add(
                new Review
                {
                    Content = review,
                    MovieId = this.MovieId
                }
            );

            databaseContext.SaveChanges();

            reviewsViewSource.Source = databaseContext.Reviews.Local
                .Where(r => r.MovieId == this.MovieId).ToList();
            reviewsGrid.Items.Refresh();
        }
    }
}
