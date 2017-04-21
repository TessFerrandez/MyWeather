using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using MyWeather.Core.Helpers;
using MyWeather.Core.ViewModels;
using MyWeather.Droid.Helpers;
using System.Collections.Generic;

namespace MyWeather.Droid
{
    [Activity(Label = "Weather")]
    public partial class WeatherActivity 
    {
        private ObservableRecyclerAdapter<ForecastViewModel, CachingViewHolder> _adapter;
        public IWeatherViewModel Vm { get { return ViewModel as IWeatherViewModel; } }

        public WeatherActivity()
        {
            ViewModel = App.Locator.WeatherViewModel as IContentViewModelBase;
        }
        public override void SetupBindings()
        {
            SetContentView(Resource.Layout.Weather);
            _bindings.Add(this.SetBinding(
                () => Vm.SearchLocation,
                () => LocationTextView.Text));
            _bindings.Add(this.SetBinding(
                () => Vm.CurrentWeather.Description,
                () => DescriptionTextView.Text));
            _bindings.Add(this.SetBinding(
                () => Vm.CurrentWeather.Temp,
                () => TempTextView.Text));

            //TODO: This needs to happen as a binding
            ImageDownloader.AssignImageAsync(IconImageView, Vm.CurrentWeather.Icon, this);

            _adapter = Vm.Forecasts.GetRecyclerAdapter(
                BindViewHolder,
                Resource.Layout.ForecastTemplate);

            ForecastList.SetLayoutManager(new LinearLayoutManager(this));
            ForecastList.SetAdapter(_adapter);
        }

        private void BindViewHolder(CachingViewHolder holder, ForecastViewModel forecast, int position)
        {
            var image = holder.FindCachedViewById<ImageView>(Resource.Id.FCT_IconImageView);
            ImageDownloader.AssignImageAsync(image, forecast.Icon, this);

            var dayOfWeek = holder.FindCachedViewById<TextView>(Resource.Id.FCT_DayOfWeekTextView);
            dayOfWeek.Text = forecast.DayOfWeek;

            var description = holder.FindCachedViewById<TextView>(Resource.Id.FCT_DescriptionTextView);
            description.Text = forecast.Description;

            var dayTemp = holder.FindCachedViewById<TextView>(Resource.Id.FCT_DayTempTextView);
            dayTemp.Text = forecast.DayTemp;
        }
    }
}