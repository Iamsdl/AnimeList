namespace SDLModels
{
    class OVA
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenOVAs { get; set; }
        public ushort TotalOVAs { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
