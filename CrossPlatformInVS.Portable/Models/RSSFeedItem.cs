using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CrossPlatformInVS.Portable.Models
{
  public class RSSFeedItem
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public string PublishDate { get; set; }
    public int Id { get; set; }
    private string caption;

    public string Caption
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(caption))
          return caption;

        //get rid of HTML tags
        caption = Regex.Replace(Description, "<[^>]*>", string.Empty);

        //get rid of multiple blank lines
        caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

        return caption;
      }
    }
    
  }
}
