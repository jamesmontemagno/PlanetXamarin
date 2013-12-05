using System;
using System.Drawing;
using CrossPlatformInVS.Portable.ViewModels;
using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CrossPlatformInVS.Touch
{


  [Register("MasterView")]
  public class MasterViewController : UITableViewController, IUITableViewDataSource
  {
    private MasterViewModel viewModel;

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

    public async override void ViewDidLoad()
    {

      base.ViewDidLoad();
      this.TableView.WeakDataSource = this;

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, async delegate
      {
        LoadArticles();
      });

      LoadArticles();
    }

    private async void LoadArticles()
    {
      BigTed.BTProgressHUD.Show("Loading...");
      await viewModel.ExecuteLoadItemsCommand();
      TableView.ReloadData();
      BigTed.BTProgressHUD.Dismiss();
    }

    UITableViewCell IUITableViewDataSource.GetCell(UITableView tableView, NSIndexPath indexPath)
    {

      var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "rssitem");
      cell.TextLabel.Text = viewModel.FeedItems[indexPath.Row].Title;
      cell.DetailTextLabel.Text = viewModel.FeedItems[indexPath.Row].Caption;
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
  }
}