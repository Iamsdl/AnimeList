namespace SDLModels
{
    public class ONA
    {
        public ONA()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public short Season { get; set; }
        public short SeenONAs { get; set; }
        public short TotalONAs { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
