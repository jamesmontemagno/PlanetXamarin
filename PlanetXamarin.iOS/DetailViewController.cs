using System;
using System;
using CoreGraphics;
using MonoTouch.Dialog;
using Foundation;
using UIKit;
using PlanetXamarin.Portable.Models;

namespace PlantXamarin.iOS
{
  public partial class DetailViewController : DialogViewController
  {

    public DetailViewController(RSSFeedItem item)
      : base(UITableViewStyle.Grouped, null, true)
    {
      var attributes = new NSAttributedStringDocumentAttributes();
      attributes.DocumentType = NSDocumentType.HTML;
      attributes.StringEncoding = NSStringEncoding.UTF8;
      var error = new NSError();
      var htmlString = new NSAttributedString(item.Description, attributes, ref error);
     
     
      Root = new RootElement(item.Title) {
                new Section{
                new StringElement(item.Author),
                new StringElement(item.PublishDate),
				        new StyledMultilineElement(htmlString),
                new HtmlElement("Full Article", item.Link)
              }
			};

      NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Action, async delegate
      {
        var message = item.Title + " " + item.Link + " #PlanetXamarin";
        var social = new UIActivityViewController(new NSObject[] { new NSString(message) },
          new UIActivity[] { new UIActivity() });
        PresentViewController(social, true, null);
      });
    }



    public class StyledMultilineElement : StyledStringElement, IElementSizing
    {
      public StyledMultilineElement(NSAttributedString attributedString)
        : base(null)
      {
        this.AttributedText = attributedString;
      }
      public NSAttributedString AttributedText { get; set; }

      public virtual nfloat GetHeight(UITableView tableView, NSIndexPath indexPath)
      {

          var sizeAttr = (CGRect)this.AttributedText.GetBoundingRect((CGSize)new CGSize(UIApplication.SharedApplication.KeyWindow.Frame.Width, int.MaxValue), (NSStringDrawingOptions)NSStringDrawingOptions.UsesLineFragmentOrigin, (NSStringDrawingContext)new NSStringDrawingContext());
          return (nfloat)sizeAttr.Height;

      }

      public override UITableViewCell GetCell(UITableView tv)
      {
        var cell = base.GetCell(tv);
        cell.TextLabel.AttributedText = this.AttributedText;
        return cell;
      }
    }
  }
  
}