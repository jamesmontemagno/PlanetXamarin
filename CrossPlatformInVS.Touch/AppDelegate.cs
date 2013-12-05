using System;
using System.Collections.Generic;
using System.Linq;
using CrossPlatformInVS.Touch.PlatformSpecific;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CrossPlatformInVS.Touch
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        private UINavigationController navigationController;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            Portable.Helpers.CrossPlatfromSettings.Instance = new TouchSettings();
            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            navigationController = new UINavigationController(new MasterViewController());
            // If you have defined a view, add it here:
             window.RootViewController  = navigationController;

            // make the window visible
            window.MakeKeyAndVisible();

            return true;
        }
    }
}