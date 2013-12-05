using System;
using System.Drawing;
using CrossPlatformInVS.Portable.Models;
using MonoTouch.CoreText;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CrossPlatformInVS.Touch
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
                new StringElement(item.PublishDate),
				        new StyledMultilineElement(htmlString),
                new HtmlElement("Full Article", item.Link)
              }
			};
    }



    public class StyledMultilineElement : StyledStringElement, IElementSizing
    {
      public StyledMultilineElement(NSAttributedString attributedString)
        : base(null)
      {
        this.AttributedText = attributedString;
      }
      public NSAttributedString AttributedText { get; set; }

      public virtual float GetHeight(UITableView tableView, NSIndexPath indexPath)
      {

          var sizeAttr = this.AttributedText.GetBoundingRect(new SizeF(UIApplication.SharedApplication.KeyWindow.Frame.Width, int.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin, new NSStringDrawingContext());
          return sizeAttr.Height;

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