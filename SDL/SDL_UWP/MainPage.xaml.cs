using Newtonsoft.Json;
using SDLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SDL_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string _baseAddress = "http://localhost:55084";
        private enum Entity
        {
            Aliases = 0,
            Animes = 1,
            Books = 2,
            Games = 3,
            Mangas = 4,
            Movies = 5,
            ONAs = 6,
            OVAs = 7,
            Specials = 8
        }
        List<List<dynamic>> testing = new List<List<dynamic>>();
        
        private List<Alias> _aliasesList =      new List<Alias>();
        private List<Anime> _animesList =       new List<Anime>();
        private List<Book> _booksList =         new List<Book>();
        private List<Game> _gamesList =         new List<Game>();
        private List<Manga> _mangasList =       new List<Manga>();
        private List<Movie> _moviesList =       new List<Movie>();
        private List<ONA> _ONAsList =           new List<ONA>();
        private List<OVA> _OVAsList =           new List<OVA>();
        private List<Special> _SpecialsList =   new List<Special>();

        public MainPage()
        {
            InitializeComponent();
            testing.Add(new List<Alias>());
            testing.Add(new List<Anime>());
            testing.Add(new List<Book>());
            testing.Add(new List<Game>());
            testing.Add(new List<Manga>());
            testing.Add(new List<Movie>());
            testing.Add(new List<ONA>());
            testing.Add(new List<OVA>());
            testing.Add(new List<Special>());

            List<string> categories = new List<string>()
            {
                "Aliases",
                "Animes",
                "Books",
                "Games",
                "Mangas",
                "Movies",
                "ONAs",
                "OVAs",
                "Specials"
            };
            Category.ItemsSource = categories;
            Category.SelectedIndex = 0;
        }

        private async void Category_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
#warning send request only if list is empty;

            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + Category.SelectedValue.ToString() + "/GetAll"));
            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            if (responseString.StartsWith("\"Success."))
            {
                string jsonResponse = responseString.Substring(10);
                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                jsonResponse = jsonResponse.Replace("]\"", "]");
                _animesList = JsonConvert.DeserializeObject<List<Anime>>(jsonResponse);
                var namesList = _animesList.Select(anime => anime.AliasName).Distinct();
                NamesList.ItemsSource = namesList;
            }
            if (responseString.StartsWith("\"Failed."))
            {
                string errorMessage = responseString.Substring(9);


            }
        }
    }
}
