using System.Windows;
using PlanetXamarin.Portable.Interfaces;

namespace PlanetXamarin.WinPhone.PlatformSpecific
{
  public class Message : IMessage
  {
    public void SendMessage(string message, string title = null)
    {
      if (string.IsNullOrWhiteSpace(title))
        MessageBox.Show(message);
      else
        MessageBox.Show(message, title, MessageBoxButton.OK);
    }
  }
}
