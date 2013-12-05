using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using AndroidHUD;
using CrossPlatformInVS.Droid.Adapters;
using CrossPlatformInVS.Portable.ViewModels;

namespace CrossPlatformInVS.Droid
{
  [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@android:style/Theme.Holo.Light")]
  public class MasterActivity : ListActivity
  {
    private static MasterViewModel viewModel;
    public static MasterViewModel ViewModel
    {
      get { return viewModel ?? (viewModel = new MasterViewModel()); }
    }

    protected async override void OnCreate(Bundle bundle)
    {
      Portable.Helpers.CrossPlatformMessage.Instance = new PlatformSpecific.Message();
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

