using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace PlanetXamarin.Portable.Helpers
{
  namespace PlanetX.Utilities
  {
    /// <summary>
    /// Gravatar interaction.
    /// </summary>
    public class Gravatar
    {
      const string HttpUrl = "http://www.gravatar.com/avatar.php?gravatar_id=";
      const string HttpsUrl = "https://secure.gravatar.com/avatar.php?gravatar_id=";
      private const string defaultImage = @"http%3a%2f%2fd1iqk4d73cu9hh.cloudfront.net%2fcomponents%2fimg%2fuser-icon.png";
      private static Dictionary<string, string> BloggerDictionary = new Dictionary<string, string>();

      /// <summary>
      /// Gets the Gravatar URL.
      /// </summary>
      /// <param name="author">The author.</param>
      /// <param name="secure">Use HTTPS?</param>
      /// <param name="size">The Gravatar size.</param>
      /// <param name="rating">The Gravatar rating.</param>
      /// <returns>A gravatar URL.</returns>
      public static string GetUrl(string author, bool secure = true, int size = 64, string rating = "x")
      {
        var email = GetMD5(FindEmail(author.ToLower()));
        var url = (secure) ? HttpsUrl : HttpUrl;
        return string.Format("{0}{1}&s={2}&r={3}&d={4}", url, email, size.ToString(CultureInfo.InvariantCulture), rating, defaultImage);
      }

      static string FindEmail(string name)
      {
        if (BloggerDictionary.Count == 0)
        {
          var bloggerStream =
						ResourceLoader.GetEmbeddedResourceString(Assembly.GetAssembly(typeof(ResourceLoader)),
              "Bloggers.xml");

							var xdoc = XDocument.Parse(bloggerStream);
          var bloggers = (from item in xdoc.Descendants("Blogger")
                         select new
                         {
                           Name = (string)item.Attribute("Name"),
                           Email = (string)item.Attribute("Email"),
                         }).ToList();

          foreach (var b in bloggers)
          {
            BloggerDictionary.Add(b.Name.ToLower(), b.Email.ToLower());
          }
        }

        if (BloggerDictionary.ContainsKey(name))
          return BloggerDictionary[name];

        return string.Empty;
      }

      /// <summary>
      /// Gets the MD5 of the given string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>The MD5 hash.</returns>
      static string GetMD5(string input)
      {

        var bytes = Encoding.UTF8.GetBytes(input);
        var data = MD5Core.GetHash(bytes);
        var builder = new StringBuilder();

        for (int i = 0; i < data.Length; i++) builder.Append(data[i].ToString("x2"));
        return builder.ToString();
      }
    }
  }
}
