using MyWeather.Core.Helpers;
using MyWeather.Core.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace MyWeather.Core.Design
{
    public class DesignWeatherViewModel : DTAppViewModelBaseWithParameter<string>, IWeatherViewModel
    {
        public CurrentWeatherViewModel CurrentWeather => new CurrentWeatherViewModel()
        {
            Icon = "http://openweathermap.org/img/w/04n.png",
            Temp = "15.2°C",
            Description = "Partly cloudy",
            TempRange = "8°C-12.1°C",
            Preassure = "121hPa",
            Humidity = "10%",
            WindSpeed = "1.2kmph NE"
        };
        public ObservableCollection<ForecastViewModel> Forecasts
        {
            get
            {
                var forecasts = new ObservableCollection<ForecastViewModel>();
                for(int i = 0; i < 5; i++)
                {
                    forecasts.Add(new ForecastViewModel()
                    {
                        DayOfWeek = "Wed",
                        Icon = "http://openweathermap.org/img/w/04n.png",
                        DayTemp = "15.2°C",
                        Description = "Some description"
                    });
                }
                return forecasts;                 
            }
        }
        public bool IsMetric
        {
            get { return true; }
            set { throw new NotImplementedException(); }
        }
        public string SearchLocation
        {
            get { return "Stockholm,SE"; }
            set { throw new NotImplementedException(); }
        }
    }
}
