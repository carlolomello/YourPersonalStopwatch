﻿using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace YourPSW.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            //per inizializzare il popup
            Rg.Plugins.Popup.Popup.Init();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
