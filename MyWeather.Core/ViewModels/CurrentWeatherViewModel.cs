using GalaSoft.MvvmLight;
using MyWeather.Core.Helpers;
using MyWeather.Core.Models;

namespace MyWeather.Core.ViewModels
{
    public class CurrentWeatherViewModel : ViewModelBase
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }
        private string _humidity;
        public string Humidity
        {
            get { return _humidity; }
            set { Set(ref _humidity, value); }
        }
        private string _icon = "";
        public string Icon
        {
            get { return _icon; }
            set { Set(ref _icon, value); }
        }
        private string _preassure;
        public string Preassure
        {
            get { return _preassure; }
            set { Set(ref _preassure, value); }
        }
        private string _temp;
        public string Temp
        {
            get { return _temp; }
            set { Set(ref _temp, value); }
        }
        private string _tempRange;
        public string TempRange
        {
            get { return _tempRange; }
            set { Set(ref _tempRange, value); }
        }
        private string _windSpeed;
        public string WindSpeed
        {
            get { return _windSpeed; }
            set { Set(ref _windSpeed, value); }
        }

        public static CurrentWeatherViewModel FromModel(CurrentWeatherDataModel model)
        {
            var weather = model.Weather[0];
            return new CurrentWeatherViewModel()
            {
                Description = weather.Description,
                Humidity = $"{model.Main.Humidity}%",
                Icon = $"http://openweathermap.org/img/w/{weather.Icon}.png",
                Preassure = $"{model.Main.Pressure}hPa",
                Temp = $"{model.Main.Temp}°C",
                TempRange = $"{model.Main.MinTemp}°C - {model.Main.MaxTemp}°C",
                WindSpeed = $"{model.Wind.Speed}kmph {DegreesToDirection(model.Wind.Degrees)}"
            };
        }
        public static string DegreesToDirection(double deg)
        {
            if (deg >= 348.75 || deg < 11.25)
                return "N";
            else if (deg >= 11.25 && deg < 33.75)
                return "NNE";
            else if (deg >= 33.75 && deg < 56.25)
                return "NE";
            else if (deg >= 56.25 && deg < 78.75)
                return "ENE";
            else if (deg >= 78.75 && deg < 101.25)
                return "E";
            else if (deg >= 101.25 && deg < 123.75)
                return "ESE";
            else if (deg >= 123.75 && deg < 146.25)
                return "SE";
            else if (deg >= 146.25 && deg < 168.75)
                return "SSE";
            else if (deg >= 168.75 && deg < 191.25)
                return "S";
            else if (deg >= 191.25 && deg < 213.75)
                return "SSW";
            else if (deg >= 213.75 && deg < 236.25)
                return "SW";
            else if (deg >= 236.25 && deg < 258.75)
                return "WSW";
            else if (deg >= 258.75 && deg < 281.25)
                return "W";
            else if (deg >= 281.25 && deg < 303.75)
                return "WNW";
            else if (deg >= 303.75 && deg < 326.25)
                return "NW";
            else
                return "NNW";
        }
    }
}
