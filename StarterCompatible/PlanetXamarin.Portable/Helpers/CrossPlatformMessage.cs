using PlanetXamarin.Portable.Interfaces;

namespace PlanetXamarin.Portable.Helpers
{
  /// <summary>
  /// Static helper class to set the instance of IMessage
  /// </summary>
  public static class CrossPlatformMessage
  {
    public static IMessage Instance { get; set; }
  }
}
