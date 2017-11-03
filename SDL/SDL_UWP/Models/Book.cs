namespace SDL_UWP.Models
{
    class Book
    {
        public string Title { get; set; }
        public ushort Volume { get; set; }
        public ushort ReadChapters { get; set; }
        public ushort TotalChapters { get; set; }

        public virtual Alias Alias { get; set; }
    }
}