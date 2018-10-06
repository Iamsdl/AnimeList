namespace SDLModels
{
    public class Special : IItem
    {
        public Special()
        {

        }
        public string AliasName { get { return ShortName; } set { ShortName = value; } }
        public string FullName { get { return LongName; } set { LongName = value; } }
        public short Season { get { return Part; } set { Part = value; } }
        public short SeenSpecials { get { return Seen; } set { Seen = value; } }
        public short TotalSpecials { get { return Total; } set { Total = value; } }

        public virtual Alias Alias { get; set; }
    }
}
