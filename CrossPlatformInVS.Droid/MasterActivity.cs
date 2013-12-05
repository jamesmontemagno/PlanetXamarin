using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AndroidHUD;
using CrossPlatformInVS.Droid.Adapters;
using CrossPlatformInVS.Droid.PlatformSpecific;
using CrossPlatformInVS.Portable.ViewModels;

namespace CrossPlatformInVS.Droid
{
  [Activity(Label = "CrossPlatformInVS.Droid", MainLauncher = true, Icon = "@drawable/icon")]
  public class MasterActivity : ListActivity
  {
    private static MasterViewModel viewModel;
    public static MasterViewModel ViewModel
    {
      get { return viewModel ?? (viewModel = new MasterViewModel()); }
    }

    protected async override void OnCreate(Bundle bundle)
    {
      Portable.Helpers.CrossPlatfromSettings.Instance = new DroidSettings();
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Master);

      viewModel = new MasterViewModel();
      AndHUD.Shared.Show(this, "Loading...");
      await viewModel.ExecuteLoadItemsCommand();
      ListAdapter = new FeedItemAdapter(this, viewModel.FeedItems);
      AndHUD.Shared.Dismiss(this);

    }
  }
}

