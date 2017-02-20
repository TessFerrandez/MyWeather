using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MyWeather.Core.Helpers;
using MyWeather.Core.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;
using SDWebImage;
using Foundation;

namespace MyWeather.iOS
{
    public partial class WeatherViewController : UIViewController
    {
        private const string ReuseId = "ReuseId";

        private readonly List<Binding> _bindings = new List<Binding>();
        private ObservableTableViewSource<ForecastViewModel> _source;
        private NavigationService Nav => ServiceLocator.Current.GetInstance<INavigationService>() as NavigationService;
        private IWeatherViewModel Vm { get { return Application.Locator.WeatherViewModel; } }
        public WeatherViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            try
            {
                var searchLocation = (string)Nav.GetAndRemoveParameter(this);
                var vmBase = Vm as IContentViewModelBase;
                vmBase.OnNavigatedTo(searchLocation, NavigationMode.New);

                #region Hidden
                // See https://developer.xamarin.com/guides/android/advanced_topics/linking/#falseflag
                var falseFlag = false;
                //if (falseFlag) { GetWeatherButton.TouchUpInside += (s, e) => { }; }
                #endregion

                _bindings.Add(this.SetBinding(() => Vm.SearchLocation, () => LocationText.Text, BindingMode.TwoWay));
                _bindings.Add(this.SetBinding(() => Vm.CurrentWeather.Description, () => DescriptionText.Text));
                _bindings.Add(this.SetBinding(() => Vm.CurrentWeather.Temp, () => TempText.Text));
                IconImage.SetImage(new NSUrl(Vm.CurrentWeather.Icon), UIImage.FromBundle("01d.png"));

                _source = Vm.Forecasts.GetTableViewSource(BindForecastCell, ReuseId);
                ForecastsTableView.RegisterClassForCellReuse(typeof(UITableViewCell), new NSString(ReuseId));
                ForecastsTableView.Source = _source;

                //_bindings.Add(this.SetBinding(() => Vm.SearchLocation, () => LocationText.Text, BindingMode.TwoWay));
                //GetWeatherButton.SetCommand(Vm.ToWeatherCommand);

            }
            catch (Exception ex) { }
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void BindForecastCell(UITableViewCell cell, ForecastViewModel forecast, NSIndexPath path)
        {
            cell.TextLabel.Text = forecast.DayTemp;
            //cell.DetailTextLabel.Text = forecast.DayOfWeek;
            //cell.ImageView.SetImage(new NSUrl(forecast.Icon), UIImage.FromBundle("01d.png"));
            cell.ImageView.SetImage(new NSUrl("http://openweathermap.org/img/w/02d.png"), UIImage.FromBundle("01d.png"));
        }
    }
}