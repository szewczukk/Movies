using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApp.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Genres = new List<Genre>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Director Director { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}
