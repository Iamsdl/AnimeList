﻿namespace SDLUWPModels
{
    public class IItem
    {
        public string ShortName { get; set; }
        protected string LongName { get; set; }
        protected short Part { get; set; }
        protected short Seen { get; set; }
        protected short Total { get; set; }
    }
}
