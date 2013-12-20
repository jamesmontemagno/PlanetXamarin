using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PlanetXamarin.Portable;
using PlanetXamarin.Portable.Interfaces;
using PlanetXamarin.Droid.PlatformSpecific;

namespace PlanetXamarin.Droid
{
  [Application(Icon = "@drawable/ic_launcher", Label = "@string/app_name")]
  public class PlanetXamarinApp : Application
  {
    public PlanetXamarinApp(IntPtr handle, JniHandleOwnership transfer)
      : base(handle, transfer)
    {
    }

    public override void OnCreate()
    {
      base.OnCreate();
      ServiceContainer.Register<IMessage>(() => new MessageDroid());
      ServiceContainer.Register<ISettings>(() => new SettingsDroid());
    }
  }
}