using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using CrossPlatformInVS.Portable.Interfaces;

namespace CrossPlatformInVS.Store.PlatformSpecific
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
