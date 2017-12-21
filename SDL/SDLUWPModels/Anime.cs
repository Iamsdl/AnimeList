namespace SDLUWPModels
{
    public class Anime
    {
        public Anime()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public short Season { get; set; }
        public short SeenEpisodes { get; set; }
        public short TotalEpisodes { get; set; }

        public virtual Alias Alias { get; set; }

    }
}
