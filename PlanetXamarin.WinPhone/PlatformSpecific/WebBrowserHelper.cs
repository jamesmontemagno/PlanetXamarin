using System.Text;
using System.Windows;
using System.Windows.Media;

//Styling Code from: http://www.ben.geek.nz/2010/07/integrated-links-and-styling-for-windows-phone-7-webbrowser-control/

namespace PlanetXamarin.WinPhone.PlatformSpecific
{
  public static class WebBrowserHelper
  {

    public static string HtmlHeader(double viewportWidth)
    {
      var head = new StringBuilder();

      head.Append("<head>");
      head.Append(string.Format(
      "<meta name=\"viewport\" value=\"width={0}\" user-scalable=\"no\">",
      viewportWidth));
      head.Append("<style>");
      head.Append("html { -ms-text-size-adjust:150% }");
      head.Append(string.Format(
      "body {{background:{0};color:{1};font-family:'Segoe WP';font-size:{2}pt;margin:0;padding:0 }}",
      GetBrowserColor("PhoneBackgroundColor"),
      GetBrowserColor("PhoneForegroundColor"),
      (double)Application.Current.Resources["PhoneFontSizeNormal"]));
      head.Append(string.Format(
      "a {{color:{0}}}",
      GetBrowserColor("PhoneAccentColor")));
      head.Append("</style>");
      head.Append("</head>");

      return head.ToString();
    }

    private static string GetBrowserColor(string sourceResource)
    {
      var color = (Color)Application.Current.Resources[sourceResource];
      return "#" + color.ToString().Substring(3, 6);
    }
    public static string WrapHtml(string htmlSubString, double viewportWidth)
    {
      var html = new StringBuilder();
      html.Append("<html>");
      html.Append(HtmlHeader(viewportWidth));
      html.Append("<body>");
      html.Append(htmlSubString);
      html.Append("</body>");
      html.Append("</html>");
      return html.ToString();
    }


  }
}
