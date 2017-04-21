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
    public partial class WeatherViewController : MvvmViewControllerBase
    {
        private const string ReuseId = "ReuseId";

        private ObservableTableViewSource<ForecastViewModel> _source;
        private IWeatherViewModel Vm { get { return ViewModel as IWeatherViewModel; } }
        public WeatherViewController(IntPtr handle) : base(handle)
        {
            ViewModel = Application.Locator.WeatherViewModel as IContentViewModelBase;
        }

        public override void SetupBindings()
        {
            try
            {
                _bindings.Add(this.SetBinding(() => Vm.SearchLocation, () => LocationText.Text, BindingMode.TwoWay));
                _bindings.Add(this.SetBinding(() => Vm.CurrentWeather.Description, () => DescriptionText.Text));
                _bindings.Add(this.SetBinding(() => Vm.CurrentWeather.Temp, () => TempText.Text));
                IconImage.SetImage(new NSUrl(Vm.CurrentWeather.Icon), UIImage.FromBundle("01d.png"));

                _source = Vm.Forecasts.GetTableViewSource(BindForecastCell, ReuseId);
                ForecastsTableView.RegisterClassForCellReuse(typeof(UITableViewCell), new NSString(ReuseId));
                ForecastsTableView.Source = _source;
            }
            catch (Exception)
            {
            }
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