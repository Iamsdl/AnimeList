namespace SDLModels
{
    public class Game :IItem
    {
        public Game()
        {

        }
        public string AliasName { get { return ShortName; } set { ShortName = value; } }
        public string FullName { get { return LongName; } set { LongName = value; } }

        public virtual Alias Alias { get; set; }
    }
}
