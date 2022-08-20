using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoviesApp.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
            = new ObservableCollection<Movie>();

        public override string ToString()
        {
            return Name;
        }
    }
}
