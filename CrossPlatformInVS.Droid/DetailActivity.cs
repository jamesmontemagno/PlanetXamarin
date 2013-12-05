using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using CrossPlatformInVS.Portable.Models;

namespace CrossPlatformInVS.Droid
{
  [Activity(Label = "Details Activity", Icon = "@drawable/ic_launcher", Theme = "@android:style/Theme.Holo.Light")]
  public class DetailActivity : Activity
  {
    private WebView webView;
    private RSSFeedItem feedItem;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.Detail);

      webView = FindViewById<WebView>(Resource.Id.webview);
      var id = Intent.GetIntExtra("id", 0);
      feedItem = MasterActivity.ViewModel.GetFeedItem(id);
      webView.LoadData(feedItem.Description, "text/html", "charset=UTF-8");

      ActionBar.Title = feedItem.Title;
    }


    public override void OnBackPressed()
    {
      if (webView.CanGoBack())
      {
        webView.GoBack();
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