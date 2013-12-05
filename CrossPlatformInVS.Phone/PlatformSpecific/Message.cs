using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CrossPlatformInVS.Portable.Interfaces;

namespace CrossPlatformInVS.Phone.PlatformSpecific
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
