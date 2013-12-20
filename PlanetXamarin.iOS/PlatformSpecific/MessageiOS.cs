using GCDiscreetNotification;
using MonoTouch.UIKit;
using PlanetXamarin.Portable.Interfaces;

namespace PlanetXamarin.iOS.PlatformSpecific
{
  public class MessageiOS : IMessage
  {

    public void SendMessage(string message, string title = null)
    {

      var notificationView = new GCDiscreetNotificationView(
        text: message,
        activity: false,
        presentationMode: GCDNPresentationMode.Bottom,
        view: UIApplication.SharedApplication.KeyWindow
      );

      notificationView.ShowAndDismissAfter(3);

    }
  }
}