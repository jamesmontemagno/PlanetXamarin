using System;
using Windows.UI.Popups;
using PlanetXamarin.Portable.Interfaces;

namespace PlanetXamarin.WinStore.PlatformSpecific
{
  public class Message : IMessage
  {
    public async void SendMessage(string message, string title = null)
    {
      var dialog = new MessageDialog(message, title ?? string.Empty);
      dialog.Commands.Add(new UICommand("OK"));
      await dialog.ShowAsync();
    }
  }
}
