namespace SDLUWPModels
{
    public class Game : IItem
    {
        public Game()
        {

        }
        public string AliasName { get; set; }
        public string FullName { get; set; }

        public virtual Alias Alias { get; set; }
    }
}
