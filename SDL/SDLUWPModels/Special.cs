namespace SDLUWPModels
{
    public class Special
    {
        public Special()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public short Season { get; set; }
        public short SeenSpecials { get; set; }
        public short TotalSpecials { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
