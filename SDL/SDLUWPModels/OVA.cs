namespace SDLUWPModels
{
    public class OVA
    {
        public OVA()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public short Season { get; set; }
        public short SeenOVAs { get; set; }
        public short TotalOVAs { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
