using MonoTouch.Foundation;
using MonoTouch.UIKit;
using PlanetXamarin.Portable.ViewModels;
using PlantXamarin.iOS.PlatformSpecific;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System;
using PlanetXamarin.Portable.Models;
using System.Linq;

namespace PlantXamarin.iOS
{


  [Register("MasterView")]
	public class MasterViewController : UITableViewController, IUITableViewDataSource
  {
    private MasterViewModel viewModel;
		private UIImage defaultImage;
    public MasterViewController()
    {
      viewModel = new MasterViewModel();
      this.Title = "Planet Xamarin";
    }

    public override void DidReceiveMemoryWarning()
    {
      // Releases the view if it doesn't have a superview.
      base.DidReceiveMemoryWarning();

      // Release any cached data, images, etc that aren't in use.
    }

		Task DownloadTask;
    public async override void ViewDidLoad()
		{

      base.ViewDidLoad();
			defaultImage = UIImage.FromBundle("default_person.png");

			DownloadTask = Task.Factory.StartNew (() => {
			});
			TaskScheduler.UnobservedTaskException += delegate(object sender, UnobservedTaskExceptionEventArgs e) {
				e.SetObserved ();
			};
      this.TableView.WeakDataSource = this;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, async delegate
      {
        LoadArticles();
      });

      LoadArticles();
    }
		LoadingOverlay loadingOverlay;
		private async void LoadArticles()
    {
			loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			View.Add (loadingOverlay);
      await viewModel.ExecuteLoadItemsCommand();
      TableView.ReloadData();
			loadingOverlay.Hide ();
    }
		 




    UITableViewCell IUITableViewDataSource.GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      var item = viewModel.FeedItems[indexPath.Row];
			item.Tag = indexPath.Row;
      var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "rssitem");
      cell.TextLabel.Text = item.Title;
      cell.DetailTextLabel.Text = item.Caption;
			if (item.ShowImage) {
				if (item.TheImage == null) {
					item.TheImage = defaultImage;
					BeginDownloadingImage (item, indexPath);
				}
				cell.ImageView.Image = item.TheImage;
			} else {
				cell.ImageView.Image = defaultImage;
			}
      return cell;
    }

    int IUITableViewDataSource.RowsInSection(UITableView tableView, int section)
    {
      return viewModel.FeedItems.Count;
    }

    public override UITableViewCellAccessory AccessoryForRow(UITableView tableView, NSIndexPath indexPath)
    {
      return UITableViewCellAccessory.DisclosureIndicator;
    }

    public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
    {
      NavigationController.PushViewController(new DetailViewController(viewModel.FeedItems[indexPath.Row]), true);
    }

		void BeginDownloadingImage (RSSFeedItem app, NSIndexPath path)
		{
			// Queue the image to be downloaded. This task will execute
			// as soon as the existing ones have finished.
			byte[] data = null;
			DownloadTask = DownloadTask.ContinueWith (prevTask => {
				try {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
					using (var c = new GzipWebClient ())
						data = c.DownloadData (app.Image);
				} finally {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				}
			});

			// When the download task is finished, queue another task to update the UI.
			// Note that this task will run only if the download is successful and it
			// uses the CurrentSyncronisationContext, which on MonoTouch causes the task
			// to be run on the main UI thread. This allows us to safely access the UI.
			DownloadTask = DownloadTask.ContinueWith (t => {
				// Load the image from the byte array.
				app.TheImage = UIImage.LoadFromData (NSData.FromArray (data));

				// Retrieve the cell which corresponds to the current App. If the cell is null, it means the user
				// has already scrolled that app off-screen.
				var cell = TableView.VisibleCells.Where (c => c.Tag == viewModel.FeedItems.IndexOf (app)).FirstOrDefault ();
				if (cell != null)
					cell.ImageView.Image = app.TheImage;
			}, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext ());
		}

		public class GzipWebClient : WebClient
		{
			protected override WebRequest GetWebRequest (Uri address)
			{
				var request = base.GetWebRequest (address);
				if (request is HttpWebRequest)
					((HttpWebRequest) request).AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				return request;
			}
		}
  }
}