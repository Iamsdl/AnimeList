namespace SDLModels
{
    class Anime
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenEpisodes { get; set; }
        public ushort TotalEpisodes { get; set; }

        public virtual Alias Alias { get; set; }

    }
}
