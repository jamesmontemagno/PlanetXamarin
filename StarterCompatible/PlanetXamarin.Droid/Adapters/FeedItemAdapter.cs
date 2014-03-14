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
      public TextView Author { get; set; }
      public TextView Caption { get; set; }
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
        helper.Title = convertView.FindViewById<TextView>(Resource.Id.text_title);
        helper.Author = convertView.FindViewById<TextView>(Resource.Id.text_author);
        helper.Caption = convertView.FindViewById<TextView>(Resource.Id.text_caption);
        helper.Image = convertView.FindViewById<ImageView>(Resource.Id.image);
        helper.Title.SetMaxLines(2);
        helper.Caption.SetMaxLines(2);
        convertView.Tag = helper;
			} else {
				helper = convertView.Tag as FeedItemAdapterHelper;
			}

		  var item = items.ElementAt(position);
			helper.Title.Text = item.Title;
		  helper.Caption.Text = item.Caption;
      helper.Author.Text = item.Author;
      helper.Image.Visibility = item.ShowImage ? ViewStates.Visible : ViewStates.Gone;
      if(item.ShowImage)
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