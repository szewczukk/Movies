namespace MoviesApp.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string Content { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
