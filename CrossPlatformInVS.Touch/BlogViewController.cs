using CrossPlatformInVS.Portable.Models;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CrossPlatformInVS.Touch
{
    public partial class BlogViewController : DialogViewController
    {

        public BlogViewController(RSSFeedItem item)
            : base(UITableViewStyle.Grouped, null, true)
        {
            Root = new RootElement(item.Title) {
                new Section{
                new StringElement(item.PublishDate),
				new MultilineElement(item.Description),
                new HtmlElement("Full Article", item.Link)
                }
			};
        }
    }
}