using Android.App;
using Android.Widget;
using PlanetXamarin.Portable.Interfaces;

namespace PlanetXamarin.Droid.PlatformSpecific
{
  public class MessageDroid : IMessage
  {
    public void SendMessage(string message, string title = null)
    {
      Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
    }
  }
}