using Newtonsoft.Json;
using SDLUWPModels;
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
        private enum Categories
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
        private List<Alias> _aliasesList = new List<Alias>();
        private List<Anime> _animesList = new List<Anime>();
        private List<Book> _booksList = new List<Book>();
        private List<Game> _gamesList = new List<Game>();
        private List<Manga> _mangasList = new List<Manga>();
        private List<Movie> _moviesList = new List<Movie>();
        private List<ONA> _ONAsList = new List<ONA>();
        private List<OVA> _OVAsList = new List<OVA>();
        private List<Special> _specialsList = new List<Special>();

        public MainPage()
        {
            InitializeComponent();

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
            try
            {
                string selectedCategory = Category.SelectedValue.ToString();
                Enum.TryParse(selectedCategory, out Categories result);
                switch (result)
                {
                    case Categories.Aliases:
                        if (!_aliasesList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _aliasesList = JsonConvert.DeserializeObject<List<Alias>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _aliasesList.Select(alias => alias.Name).Distinct();
                        break;
                    case Categories.Animes:
                        if (!_animesList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _animesList = JsonConvert.DeserializeObject<List<Anime>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _animesList.Select(anime => anime.AliasName).Distinct();

                        break;
                    case Categories.Books:
                        if (!_booksList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _booksList = JsonConvert.DeserializeObject<List<Book>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _booksList.Select(book => book.AliasName).Distinct();
                        break;
                    case Categories.Games:
                        if (!_gamesList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _gamesList = JsonConvert.DeserializeObject<List<Game>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _gamesList.Select(game => game.AliasName).Distinct();
                        break;
                    case Categories.Mangas:
                        if (!_mangasList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _mangasList = JsonConvert.DeserializeObject<List<Manga>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _mangasList.Select(manga => manga.AliasName).Distinct();
                        break;
                    case Categories.Movies:
                        if (!_moviesList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _moviesList = JsonConvert.DeserializeObject<List<Movie>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _moviesList.Select(movie => movie.AliasName).Distinct();
                        break;
                    case Categories.ONAs:
                        if (!_ONAsList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _ONAsList = JsonConvert.DeserializeObject<List<ONA>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _ONAsList.Select(ona => ona.AliasName).Distinct();
                        break;
                    case Categories.OVAs:
                        if (!_OVAsList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _OVAsList = JsonConvert.DeserializeObject<List<OVA>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _OVAsList.Select(ova => ova.AliasName).Distinct();
                        break;
                    case Categories.Specials:
                        if (!_specialsList.Any())
                        {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                            string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                            if (responseString.StartsWith("\"Success."))
                            {
                                string jsonResponse = responseString.Substring(10);
                                jsonResponse = jsonResponse.Replace("\\\"", "\"");
                                jsonResponse = jsonResponse.Replace("]\"", "]");
                                _specialsList = JsonConvert.DeserializeObject<List<Special>>(jsonResponse);
                            }
                            else if (responseString.StartsWith("\"Failed."))
                            {
                                string errorMessage = responseString.Substring(9);
                                await ShowDialog("An exception occurred on the server.", errorMessage);
                                return;
                            }
                            else
                            {
                                await ShowDialog(responseString, null);
                                return;
                            }
                        }
                        NamesList.ItemsSource = _specialsList.Select(special => special.AliasName).Distinct();
                        break;
                    default:
                        NamesList.ItemsSource = null;
                        break;
                }
            }
            catch(HttpRequestException exception)
            {
                await ShowDialog("Cannot connect to the server.", exception.Message);
            }
            catch(Exception exception)
            {
                await ShowDialog("An exception occurred on the client.", exception.Message);
            }
        }
        private static async System.Threading.Tasks.Task ShowDialog(string title, string errorMessage)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = errorMessage,
                CloseButtonText = "Ok"
            };
            ContentDialogResult dialogResult = await dialog.ShowAsync();
        }
    }
}
