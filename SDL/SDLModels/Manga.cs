namespace SDLModels
{
    class Manga
    {
        public string FullName { get; set; }
        public ushort Volume { get; set; }
        public ushort ReadChapters { get; set; }
        public ushort TotalChapters { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
