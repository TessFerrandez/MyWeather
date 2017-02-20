using GalaSoft.MvvmLight.Helpers;
using MyWeather.Core.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyWeather.iOS
{
    public partial class HomeViewController : UIViewController
    {
        private readonly List<Binding> _bindings = new List<Binding>();
        private IHomeViewModel Vm { get { return Application.Locator.HomeViewModel; } }

        //public HomeViewController() : base("HomeViewController", null)
        //{
        //}

        public HomeViewController(IntPtr handle) : base(handle) { }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}