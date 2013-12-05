using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossPlatformInVS.Portable.Models
{
    public class RSSFeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PublishDate { get; set; }
        public int Id { get; set; }
    }
}
