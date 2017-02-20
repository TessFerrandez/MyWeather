using System;

using UIKit;

namespace MyWeather.iOS
{
    public partial class WeatherViewController : UIViewController
    {
        public WeatherViewController() : base("WeatherViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}