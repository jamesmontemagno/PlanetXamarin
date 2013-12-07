using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using PlanetXamarin.Portable.Models;

namespace PlanetXamarin.Droid
{
  [Activity(Label = "Details Activity", Icon = "@drawable/ic_launcher", Theme = "@style/Theme")]
  public class DetailActivity : Activity
  {
    private WebView webView;
    private RSSFeedItem feedItem;
    private Button readFullButton;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.Detail);

      webView = FindViewById<WebView>(Resource.Id.webview);
      var id = Intent.GetIntExtra("id", 0);
      feedItem = MasterActivity.ViewModel.GetFeedItem(id);
      webView.LoadData(feedItem.Description, "text/html", "charset=UTF-8");
      webView.Settings.JavaScriptEnabled = true;

      ActionBar.Title = feedItem.Title;

      readFullButton = FindViewById<Button>(Resource.Id.button_read_full);
      readFullButton.Click += (sender, args) =>
      {
        webView.LoadUrl(feedItem.Link);
        readFullButton.Visibility = ViewStates.Gone;
      };
    }


    public override void OnBackPressed()
    {
      if (webView.CanGoBack())
      {
        webView.GoBack();
        return;
      }

      if (readFullButton.Visibility == ViewStates.Gone)
      {
        readFullButton.Visibility = ViewStates.Visible;
        return;
      }

      base.OnBackPressed();
    }

    ShareActionProvider actionProvider;
    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      var item = menu.Add("Share");
      item.SetShowAsAction(ShowAsAction.Always);
      if (actionProvider == null)
        actionProvider = new ShareActionProvider(this);
      item.SetActionProvider(actionProvider);
      actionProvider.SetShareIntent(GetShareIntent());

      return base.OnCreateOptionsMenu(menu);
    }

    private Intent GetShareIntent()
    {
      var intent = new Intent(Intent.ActionSend);
      intent.SetType("text/plain");
      intent.PutExtra(Intent.ExtraText, feedItem.Title + " " + feedItem.Link);
      return intent;
    }


  }
}