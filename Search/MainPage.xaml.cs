using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Search
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Article> Articles = new List<Article>();
        private string gender = "";
        private Filter filter = new Filter();
        private uint currentPage = 1;
        public MainPage()
        {
            this.InitializeComponent();

            articlesGrid.SizeChanged += ArticlesGrid_SizeChanged;
        }

        private void ArticlesGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsWrapGrid itemsPanel = (ItemsWrapGrid)articlesGrid.ItemsPanelRoot;

            double optimizedWidth = 240.0;
            double margin = 0.0;
            var number = (int)e.NewSize.Width / (int)optimizedWidth;
            itemsPanel.ItemWidth = (e.NewSize.Width - margin) / (double)number;
        }

        private async Task<List<Article>> getArticles(Filter filter)
        {
            Article article = new Article();
            Task<List<Article>> task = article.readAPIAsync<Article>(filter);
                //"https://api.zalando.com/articles?brandFamily=nike&color=white&color=red&category=womens-shoes&page=1&pageSize=2");
            List<Article> list = await task;

            return list;
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            Article article = new Article();
            Task<List<Article>> task = article.readAPIAsync<Article>(filter);
            List<Article> list = await task;

            Articles = list;

            articlesGrid.ItemsSource = Articles;
        }

        private async void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == 1)
                return;
            currentPage--;
            string item = this.SearchText.Text;

            Task<List<Article>> task = getArticles(item);
            Articles = await task;

            articlesGrid.ItemsSource = Articles;
        }

        private async void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            string item = this.SearchText.Text;

            Task<List<Article>> task = getArticles(item);
            List<Article> list = await task;
            if (list.Count == 0)
            {
                currentPage--;
                return;
            }

            articlesGrid.ItemsSource = Articles;
        }

        private async void SearchText_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            String value = sender.Text;
            if (value.Length < 2)
                return;
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                Task<List<Facet>> listTask = FacetGroup.getFacets(value, gender);
                List<Facet>  list = await listTask;
                sender.ItemsSource = list;
            }
        }

        private async void SearchText_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            //this is the click of serach button
            if (args.ChosenSuggestion == null)
            {
                string item = args.QueryText as string;
                Task<List<Article>> task = getArticles(item);
                Articles = await task;

                articlesGrid.ItemsSource = Articles;
            }
        }

        private async void SearchText_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            //this is the selection of item
            string item = ((Facet)args.SelectedItem).DisplayName;
            Task<List<Article>> task = getArticles(item);
            Articles = await task;

            articlesGrid.ItemsSource = Articles;
        }
        private async Task<List<Article>> getArticles(string fullText)
        {
            Filter filter = new Filter().addGender(gender).addFullText(fullText).addPaging(currentPage);
            Article article = new Article();
            Task<List<Article>> task = article.readAPIAsync<Article>(filter);
            List<Article> list = await task;

            Articles = list;
            return list;
            
        }

        private async void MaleButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            string name = ((string)button.Content).ToLower();
            if (name.Equals("both"))
            {
                gender = "";
            }
            else
            {
                gender = name;

            }

            string item = this.SearchText.Text;
            Task<List<Article>> task = getArticles(item);
            Articles = await task;

            articlesGrid.ItemsSource = Articles;
        }
    }
}
