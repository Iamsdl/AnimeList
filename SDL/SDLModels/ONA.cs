namespace SDLModels
{
    class ONA
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenONAs { get; set; }
        public ushort TotalONAs { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
