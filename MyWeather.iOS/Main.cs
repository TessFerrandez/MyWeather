﻿using MyWeather.Core.ViewModels;
using UIKit;

namespace MyWeather.iOS
{
    public class Application
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get { return _locator ?? (_locator = new ViewModelLocator()); }
        }

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}