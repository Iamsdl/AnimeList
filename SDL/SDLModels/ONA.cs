namespace SDLModels
{
    public class ONA : IItem
    {
        public ONA()
        {

        }
        public string AliasName { get { return ShortName; } set { ShortName = value; } }
        public string FullName { get { return LongName; } set { LongName = value; } }
        public short Season { get { return Part; } set { Part = value; } }
        public short SeenONAs { get { return Seen; } set { Seen = value; } }
        public short TotalONAs { get { return Total; } set { Total = value; } }

        public virtual Alias Alias { get; set; }
    }
}
