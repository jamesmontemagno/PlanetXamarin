using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using CrossPlatformInVS.Portable.Helpers;
using CrossPlatformInVS.Portable.Models;

namespace CrossPlatformInVS.Portable.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        private ObservableCollection<RSSFeedItem> feedItems = new ObservableCollection<RSSFeedItem>();

        public ObservableCollection<RSSFeedItem> FeedItems
        {
            get { return feedItems; }
            set { feedItems = value; OnPropertyChanged("FeedItems"); }
        }

        private RelayCommand loadItemsCommand;

        public ICommand LoadItemsCommand
        {
            get { return loadItemsCommand ?? (loadItemsCommand = new RelayCommand(async () => await ExecuteLoadItemsCommand())); }
        }

        public async Task ExecuteLoadItemsCommand()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync("http://blog.xamarin.com/feed/");

            FeedItems.Clear();
            var items = await ParseFeed(responseString);
            foreach (var item in items)
            {
                FeedItems.Add(item);
            }
        }

        private async Task<List<RSSFeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);

                return (from item in xdoc.Descendants("item")
                    select new RSSFeedItem
                    {
                        Title = (string) item.Element("title"),
                        Description = (string) item.Element("description"),
                        Link = (string) item.Element("link"),
                        PublishDate = (string) item.Element("pubDate")
                    }).ToList();
            });
        }

    }
}
