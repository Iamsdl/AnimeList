namespace SDLModels
{
    public class Manga
    {
        public Manga()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public short Volume { get; set; }
        public short ReadChapters { get; set; }
        public short TotalChapters { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
