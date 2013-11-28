using System;
using System.Drawing;
using CrossPlatformInVS.Portable.ViewModels;
using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CrossPlatformInVS.Touch
{
   

    [Register("MasterView")]
    public class MasterView : UITableViewController, IUITableViewDataSource
    {
        private MasterViewModel viewModel;
        public MasterView()
        {
            viewModel = new MasterViewModel();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            this.TableView.WeakDataSource = this;

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, async delegate
            {
                await viewModel.ExecuteLoadItemsCommand();
                TableView.ReloadData();
            });
            // Perform any additional setup after loading the view
        }

        UITableViewCell IUITableViewDataSource.GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            
            var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "rssitem");
            cell.TextLabel.Text = viewModel.FeedItems[indexPath.Row].Title;
            cell.DetailTextLabel.Text = viewModel.FeedItems[indexPath.Row].Description;
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
            NavigationController.PushViewController(new BlogViewController(viewModel.FeedItems[indexPath.Row]), true);
        }
    }
}