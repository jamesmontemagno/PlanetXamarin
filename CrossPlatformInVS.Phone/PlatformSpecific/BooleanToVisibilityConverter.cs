using System;
using System.Windows;
using System.Windows.Data;

namespace CrossPlatformInVS.Phone.PlatformSpecific
{
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return (value is bool && (bool) value) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return value is Visibility && (Visibility) value == Visibility.Visible;
    }
  }
}
