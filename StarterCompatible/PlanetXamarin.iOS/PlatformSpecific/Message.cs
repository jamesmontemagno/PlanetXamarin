using MonoTouch.UIKit;
using PlanetXamarin.Portable.Interfaces;

namespace PlantXamarin.iOS.PlatformSpecific
{
  public class Message : IMessage
  {

    public void SendMessage(string message, string title = null)
    {

			var alert = new UIAlertView (title ?? string.Empty, message, null, "OK", null);
			alert.Show ();
    }
  }
}