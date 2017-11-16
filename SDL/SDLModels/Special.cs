namespace SDLModels
{
    class Special
    {
        public string FullName { get; set; }
        public ushort Season { get; set; }
        public ushort SeenSpecials { get; set; }
        public ushort TotalSpecials { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
