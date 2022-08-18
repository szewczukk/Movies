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

        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            databaseContext.SaveChanges();

            moviesGrid.Items.Refresh();
            directorsGrid.Items.Refresh();
            genresGrid.Items.Refresh();
        }
    }
}
