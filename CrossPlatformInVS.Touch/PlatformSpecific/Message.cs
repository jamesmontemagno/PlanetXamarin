using CrossPlatformInVS.Portable.Interfaces;
using GCDiscreetNotification;
using MonoTouch.UIKit;

namespace CrossPlatformInVS.Touch.PlatformSpecific
{
  public class Message : IMessage
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