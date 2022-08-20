using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoviesApp.Models
{
    public class Director
    {
        public int DirectorId { get; set; }

        public string FirstName { get; set; }

        public string LastName{ get; set; }
        
        public virtual ICollection<Movie> Movies { get; set; }
            = new ObservableCollection<Movie>();

        public override string ToString()
        {
            return $"{ FirstName } { LastName }";
        }
    }
}
