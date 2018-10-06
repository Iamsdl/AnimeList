using Newtonsoft.Json;
using SDLUWPModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
        private List<IItem>[] _categoriesList = new List<IItem>[9];
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
            for (int i = 0; i <= 8; i++)
            {
                _categoriesList[i] = new List<IItem>();
            }
            Category.ItemsSource = categories;
            Category.SelectedIndex = 0;
        }

        Categories result;

        private async void Category_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectedCategory = Category.SelectedValue.ToString();
                Enum.TryParse(selectedCategory, out result);
                if (!_categoriesList[Convert.ToInt32(result)].Any())
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (responseString.StartsWith("\"Success."))
                    {
                        string jsonResponse = responseString.Substring(10);
                        jsonResponse = jsonResponse.Replace("\\\"", "\"");
                        jsonResponse = jsonResponse.Replace("]\"", "]");
                        _categoriesList[Convert.ToInt32(result)] = JsonConvert.DeserializeObject<List<IItem>>(jsonResponse);
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
                NamesList.ItemsSource = _categoriesList[Convert.ToInt32(result)].Select(item =>
                (
                    Convert.ToInt32(result) == 0 ? (item as Alias).AliasName :
                    Convert.ToInt32(result) == 1 ? (item as Anime).AliasName :
                    Convert.ToInt32(result) == 2 ? (item as Book).AliasName :
                    Convert.ToInt32(result) == 3 ? (item as Game).AliasName :
                    Convert.ToInt32(result) == 4 ? (item as Manga).AliasName :
                    Convert.ToInt32(result) == 5 ? (item as Movie).AliasName :
                    Convert.ToInt32(result) == 6 ? (item as ONA).AliasName :
                    Convert.ToInt32(result) == 7 ? (item as OVA).AliasName :
                    Convert.ToInt32(result) == 8 ? (item as Special).AliasName : null
                )).Distinct();

                //
                {
                    //    switch (result)
                    //    {
                    //        case Categories.Aliases:
                    //            if (!_aliasesList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _aliasesList = JsonConvert.DeserializeObject<List<Alias>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _aliasesList.Select(alias => alias.AliasName).Distinct();
                    //            break;
                    //        case Categories.Animes:
                    //            if (!_animesList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _animesList = JsonConvert.DeserializeObject<List<Anime>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _animesList.Select(anime => anime.AliasName).Distinct();

                    //            break;
                    //        case Categories.Books:
                    //            if (!_booksList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _booksList = JsonConvert.DeserializeObject<List<Book>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _booksList.Select(book => book.AliasName).Distinct();
                    //            break;
                    //        case Categories.Games:
                    //            if (!_gamesList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _gamesList = JsonConvert.DeserializeObject<List<Game>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _gamesList.Select(game => game.AliasName).Distinct();
                    //            break;
                    //        case Categories.Mangas:
                    //            if (!_mangasList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _mangasList = JsonConvert.DeserializeObject<List<Manga>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _mangasList.Select(manga => manga.AliasName).Distinct();
                    //            break;
                    //        case Categories.Movies:
                    //            if (!_moviesList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _moviesList = JsonConvert.DeserializeObject<List<Movie>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _moviesList.Select(movie => movie.AliasName).Distinct();
                    //            break;
                    //        case Categories.ONAs:
                    //            if (!_ONAsList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _ONAsList = JsonConvert.DeserializeObject<List<ONA>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _ONAsList.Select(ona => ona.AliasName).Distinct();
                    //            break;
                    //        case Categories.OVAs:
                    //            if (!_OVAsList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _OVAsList = JsonConvert.DeserializeObject<List<OVA>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _OVAsList.Select(ova => ova.AliasName).Distinct();
                    //            break;
                    //        case Categories.Specials:
                    //            if (!_specialsList.Any())
                    //            {
                    //                HttpClient client = new HttpClient();
                    //                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(_baseAddress + "/api/" + selectedCategory + "/GetAll"));
                    //                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    //                if (responseString.StartsWith("\"Success."))
                    //                {
                    //                    string jsonResponse = responseString.Substring(10);
                    //                    jsonResponse = jsonResponse.Replace("\\\"", "\"");
                    //                    jsonResponse = jsonResponse.Replace("]\"", "]");
                    //                    _specialsList = JsonConvert.DeserializeObject<List<Special>>(jsonResponse);
                    //                }
                    //                else if (responseString.StartsWith("\"Failed."))
                    //                {
                    //                    string errorMessage = responseString.Substring(9);
                    //                    await ShowDialog("An exception occurred on the server.", errorMessage);
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    await ShowDialog(responseString, null);
                    //                    return;
                    //                }
                    //            }
                    //            NamesList.ItemsSource = _specialsList.Select(special => special.AliasName).Distinct();
                    //            break;
                    //        default:
                    //            NamesList.ItemsSource = null;
                    //            break;
                    //    }
                }
            }
            catch (HttpRequestException exception)
            {
                await ShowDialog("Cannot connect to the server.", exception.Message);
            }
            catch (Exception exception)
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

        private void AutoSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            switch (result)
            {
                case Categories.Aliases:
                    NamesList.ItemsSource = _aliasesList.Where(a => a.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.Animes:
                    NamesList.ItemsSource = _animesList.Where(a => a.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.Books:
                    NamesList.ItemsSource = _booksList.Where(b => b.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.Games:
                    NamesList.ItemsSource = _gamesList.Where(g => g.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.Mangas:
                    NamesList.ItemsSource = _mangasList.Where(m => m.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.Movies:
                    NamesList.ItemsSource = _moviesList.Where(m => m.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.ONAs:
                    NamesList.ItemsSource = _ONAsList.Where(o => o.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.OVAs:
                    NamesList.ItemsSource = _OVAsList.Where(o => o.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
                case Categories.Specials:
                    NamesList.ItemsSource = _specialsList.Where(s => s.AliasName.Contains(AutoSuggest.Text)).Select(x => x.AliasName).Distinct();
                    break;
            }
        }

        private void NamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Content.Children.Clear();
            Alias alias;
            try
            {
                alias = _aliasesList.Where(a => a.AliasName == NamesList.SelectedValue.ToString()).FirstOrDefault();
            }
            catch
            {
                return;
            }
            switch (result)
            {
                case Categories.Aliases:

                    break;
                case Categories.Animes:
                    int i = 0;
                    foreach (var anime in alias.Animes)
                    {
                        Image animeImage = new Image()
                        {
                            Height = 180,
                            Width = 320,
                            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                            Margin = new Windows.UI.Xaml.Thickness(10, 53 + i * 185, 0, 0),
#warning change this
                            Source = new BitmapImage() { UriSource = new Uri(BaseUri, "Assets/kurisu.png") }
                        };

                        TextBox animeAliasName = new TextBox()
                        {
                            Text = anime.AliasName,
                            IsEnabled = false,
                            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                            Margin = new Windows.UI.Xaml.Thickness(335, 53 + i * 185, 0, 0),
                            TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                            Width = 637

                        };

                        TextBox animeSeason = new TextBox()
                        {
                            Text = "Season: " + anime.Season.ToString(),
                            IsEnabled = false,
                            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                            Margin = new Windows.UI.Xaml.Thickness(977, 53 + i * 185, 0, 0),
                            TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                            Width = 637
                        };

                        TextBox animeFullName = new TextBox()
                        {
                            Text = anime.FullName,
                            IsEnabled = false,
                            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                            Margin = new Windows.UI.Xaml.Thickness(335, 90 + i * 185, 0, 0),
                            TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                            Width = 1279
                        };

                        TextBox animeSeenEpisodes = new TextBox()
                        {
                            Text = anime.SeenEpisodes.ToString(),
                            IsEnabled = false,
                            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                            Margin = new Windows.UI.Xaml.Thickness(335, 127 + i * 185, 0, 0),
                            TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                            Width = 637
                        };

                        TextBox animeTotalEpisodes = new TextBox()
                        {
                            Text = anime.TotalEpisodes.ToString(),
                            IsEnabled = false,
                            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                            Margin = new Windows.UI.Xaml.Thickness(977, 127 + i * 185, 0, 0),
                            TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                            Width = 637
                        };
                        Content.Children.Add(animeImage);
                        Content.Children.Add(animeAliasName);
                        Content.Children.Add(animeSeason);
                        Content.Children.Add(animeFullName);
                        Content.Children.Add(animeSeenEpisodes);
                        Content.Children.Add(animeTotalEpisodes);
                        i++;
                    }
                    break;
                case Categories.Books:
                    break;
                case Categories.Games:
                    break;
                case Categories.Mangas:
                    break;
                case Categories.Movies:
                    break;
                case Categories.ONAs:
                    break;
                case Categories.OVAs:
                    break;
                case Categories.Specials:
                    break;
            }
        }
    }
}
