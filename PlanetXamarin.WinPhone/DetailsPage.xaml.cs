using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using PlanetXamarin.Portable.Models;
using PlanetXamarin.WinPhone.PlatformSpecific;

namespace PlanetXamarin.WinPhone
{
    public partial class DetailsPage : PhoneApplicationPage
    {
      private RSSFeedItem item;
      public DetailsPage()
      {
          InitializeComponent();
        this.Loaded += OnLoaded;
      }

      private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
      {
        ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Click += (o, args) => {
          var task = new ShareLinkTask
          {
            LinkUri = new Uri(item.Link, UriKind.Absolute),
            Title = item.Title
          };
          task.Show();
        };

        ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Click += (o, args) => {
          Browser.Navigate(new Uri(item.Link, UriKind.Absolute));
        };
      }

      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
        base.OnNavigatedTo(e);
        var idString = string.Empty;
        //parse out id and get feed item from cache
        if (NavigationContext.QueryString.TryGetValue("id", out idString))
        {
          var id = 0;
          int.TryParse(idString, out id);
          item = MasterPage.ViewModel.GetFeedItem(id);
          DataContext = item;
          var fullHtml = WebBrowserHelper.WrapHtml(item.Description, Browser.ActualWidth);
          Browser.NavigateToString(fullHtml);
        }
      }



      protected override void OnBackKeyPress(CancelEventArgs e)
      {
        

        if (Browser.CanGoBack)
        {
          Browser.GoBack();
          e.Cancel = true;
        }

        base.OnBackKeyPress(e);
      }
    }
}