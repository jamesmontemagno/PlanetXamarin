using Foundation;
using PlanetXamarin.Portable.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using UIKit;
using SDWebImage;

namespace PlantXamarin.iOS
{
  partial class MasterViewController : UITableViewController, IUITableViewDataSource, IUITableViewDelegate
  {
    private MasterViewModel viewModel;
    private UIActivityIndicatorView activityIndicator;


    public MasterViewController(IntPtr handle)
      : base(handle)
    {
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      viewModel = new MasterViewModel();

      viewModel.PropertyChanged += PropertyChanged;

      this.TableView.WeakDataSource = this;
      this.TableView.WeakDelegate = this;

      NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

      activityIndicator = new UIActivityIndicatorView(new System.Drawing.RectangleF(0, 0, 20, 20));
      activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
      activityIndicator.HidesWhenStopped = true;
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(activityIndicator);

      //Setup refresh control
      this.RefreshControl = new UIRefreshControl();

      RefreshControl.ValueChanged += async (sender, args) =>
      {
        if (viewModel.IsBusy)
          return;

        await LoadArticles();
      };


      LoadArticles();
    }

    private async Task LoadArticles()
    {
      await viewModel.ExecuteLoadItemsCommand();
      TableView.ReloadData();
    }


    #region TableView Delegates

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      var cell = tableView.DequeueReusableCell("rssitem", indexPath);
      var item = viewModel.FeedItems[indexPath.Row];

      cell.TextLabel.Text = item.Title;
      cell.DetailTextLabel.Text = item.Caption;

      if (item.ShowImage)
      {
        cell.ImageView.SetImage(
          url: new NSUrl(item.Image),
          placeholder: UIImage.FromBundle("default_person.png")
          );
      }
      return cell;
    }

    public override nint RowsInSection(UITableView tableview, nint section)
    {
      return viewModel.FeedItems.Count;
    }

    public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
    {
      NavigationController.PushViewController(new DetailViewController(viewModel.FeedItems[indexPath.Row]), true);
    }

    #endregion

    void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      InvokeOnMainThread(() =>
      {
        switch (e.PropertyName)
        {
          case "IsBusy":
            {
              if (viewModel.IsBusy)
              {
                RefreshControl.BeginRefreshing();
                activityIndicator.StartAnimating();
              }
              else
              {
                RefreshControl.EndRefreshing();
                activityIndicator.StopAnimating();
              }
            }
            break;
        }
      });
    }
  }
}
