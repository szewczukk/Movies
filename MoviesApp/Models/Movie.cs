using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoviesApp.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
            = new ObservableCollection<Review>();

        public override string ToString()
        {
            return Title;
        }
    }
}
