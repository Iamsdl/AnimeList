namespace SDLModels
{
    public class Movie
    {
        public Movie()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public short Season { get; set; }
        public short SeenMovies { get; set; }
        public short TotalMovies { get; set; }

        public virtual Alias Alias { get; set; }
        
    }
}
