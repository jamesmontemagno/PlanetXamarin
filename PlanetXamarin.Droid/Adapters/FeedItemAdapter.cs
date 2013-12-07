using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using PlanetXamarin.Droid.PlatformSpecific;
using PlanetXamarin.Portable.Models;

namespace PlanetXamarin.Droid.Adapters
{
  public class FeedItemAdapter : BaseAdapter
  {
    private ImageLoader imageLoader;
    private Activity activity;
		IEnumerable<RSSFeedItem> items;
    public FeedItemAdapter(Activity activity, IEnumerable<RSSFeedItem> items)
		{
      this.imageLoader = new ImageLoader(activity);
			this.activity = activity;
			this.items = items;
		}

		//Wrapper class for adapter for cell re-use
		private class FeedItemAdapterHelper : Java.Lang.Object
		{
			public TextView Title { get; set; }
      public TextView Description { get; set; }
      public ImageView Image { get; set; }
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
				convertView = activity.LayoutInflater.Inflate (Resource.Layout.RSSItem, null);
				helper = new FeedItemAdapterHelper ();
        helper.Title = convertView.FindViewById<TextView>(Resource.Id.textView1);
        helper.Description = convertView.FindViewById<TextView>(Resource.Id.textView2);
        helper.Image = convertView.FindViewById<ImageView>(Resource.Id.image);
        helper.Title.SetMaxLines(2);
        helper.Description.SetMaxLines(2);
        convertView.Tag = helper;
			} else {
				helper = convertView.Tag as FeedItemAdapterHelper;
			}

		  var item = items.ElementAt(position);
			helper.Title.Text = item.Title;
		  helper.Description.Text = item.Caption;
      imageLoader.DisplayImage(item.Image, helper.Image, Resource.Drawable.default_person);
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