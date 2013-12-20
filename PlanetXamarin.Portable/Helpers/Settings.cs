using PlanetXamarin.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetXamarin.Portable.Helpers
{
  public static class Settings
  {
    private static ISettings settings;
    public static ISettings PlanetXamarinSettings
    {
      get { return settings ?? (settings = ServiceContainer.Resolve<ISettings>()); }
    }

    private static string JamesOnlyKey = "james";
    public static bool JamesOnly
    {
      get { return PlanetXamarinSettings.GetValueOrDefault<bool>(JamesOnlyKey, true); }
      set
      {
        if (PlanetXamarinSettings.AddOrUpdateValue(JamesOnlyKey, value))
          PlanetXamarinSettings.Save();
      }
    }

  }
}
