using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using PlanetXamarin.Droid.Adapters;
using PlanetXamarin.Portable.Helpers;
using PlanetXamarin.Portable.ViewModels;
using Message = PlanetXamarin.Droid.PlatformSpecific.MessageDroid;

namespace PlanetXamarin.Droid
{
  [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/Theme")]
  public class MasterActivity : ListActivity
  {
    private static MasterViewModel viewModel;
    public static MasterViewModel ViewModel
    {
      get { return viewModel ?? (viewModel = new MasterViewModel()); }
    }

    protected async override void OnCreate(Bundle bundle)
    {
      
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Master);

      viewModel = new MasterViewModel();
      AndHUD.Shared.Show(this, "Loading...");
      await viewModel.ExecuteLoadItemsCommand();
      ListAdapter = new FeedItemAdapter(this, viewModel.FeedItems);
      AndHUD.Shared.Dismiss(this);

    }

    protected override void OnListItemClick(ListView l, View v, int position, long id)
    {
      base.OnListItemClick(l, v, position, id);

      var intent = new Intent(this, typeof (DetailActivity));
      intent.PutExtra("id", (int)id);
      StartActivity(intent);
    }
  }
}

