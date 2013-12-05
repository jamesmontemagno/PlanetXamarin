using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using CrossPlatformInVS.Portable.Models;

namespace CrossPlatformInVS.Droid.Adapters
{
  public class FeedItemAdapter : BaseAdapter
  {
    private Activity activity;
		IEnumerable<RSSFeedItem> items;
    public FeedItemAdapter(Activity activity, IEnumerable<RSSFeedItem> items)
		{
			this.activity = activity;
			this.items = items;
		}

		//Wrapper class for adapter for cell re-use
		private class FeedItemAdapterHelper : Java.Lang.Object
		{
			public TextView Title { get; set; }
      public TextView Description { get; set; }
		}




		#region implemented abstract members of BaseAdapter
		public override Java.Lang.Object GetItem (int position)
		{
			return position;
		}

		public override long GetItemId (int position)
    {
      var item = items.ElementAt(position);
			return item.Id;
		}

    public override bool HasStableIds
    {
      get { return true; }
    }

    public override View GetView (int position, View convertView, ViewGroup parent)
		{
			FeedItemAdapterHelper helper = null;
			if (convertView == null) {
				convertView = activity.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem2, null);
				helper = new FeedItemAdapterHelper ();
        helper.Title = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
        helper.Description = convertView.FindViewById<TextView>(Android.Resource.Id.Text2);
        helper.Title.SetMaxLines(2);
        helper.Description.SetMaxLines(2);
        convertView.Tag = helper;
			} else {
				helper = convertView.Tag as FeedItemAdapterHelper;
			}

		  var item = items.ElementAt(position);
			helper.Title.Text = item.Title;
		  helper.Description.Text = item.Description;
			return convertView;
		}

		public override int Count {
			get {
				return items.Count();
			}
		}
		#endregion
  }
}