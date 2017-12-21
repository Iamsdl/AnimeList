namespace SDLUWPModels
{
    public class Book
    {
        public Book()
        {

        }
        public string AliasName { get; set; }

        public string Title { get; set; }
        public short Volume { get; set; }
        public short ReadChapters { get; set; }
        public short TotalChapters { get; set; }

        public virtual Alias Alias { get; set; }
    }
}