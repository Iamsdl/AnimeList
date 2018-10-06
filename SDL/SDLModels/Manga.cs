namespace SDLModels
{
    public class Manga :IItem
    {
        public Manga()
        {

        }
        public string AliasName { get { return ShortName; } set { ShortName = value; } }
        public string FullName { get { return LongName; } set { LongName = value; } }
        public short Volume { get { return Part; } set { Part = value; } }
        public short ReadChapters { get { return Seen; } set { Seen = value; } }
        public short TotalChapters { get { return Total; } set { Total = value; } }

        public virtual Alias Alias { get; set; }
    }
}
