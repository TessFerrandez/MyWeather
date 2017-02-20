using MyWeather.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWeather.Core.Models;

namespace MyWeather.Core.Design
{
    public class DesignDataAccessService : IDataAccessService
    {
        public async Task<CurrentWeatherDataModel> GetCurrentWeatherAsync(string city, bool isMetric)
        {
            return CreateNewCurrentWeatherData();
        }
        public async Task<CurrentWeatherDataModel> GetCurrentWeatherAsync(double lon, double lat, bool isMetric)
        {
            return CreateNewCurrentWeatherData();
        }
        public async Task<ForecastModel> GetForecastAsync(string city, bool isMetric)
        {
            return CreateNewForecasts();
        }
        public async Task<ForecastModel> GetForecastAsync(double lon, double lat, bool isMetric)
        {
            return CreateNewForecasts();
        }
        private CurrentWeatherDataModel CreateNewCurrentWeatherData()
        {
            return new CurrentWeatherDataModel()
            {
                CityName = "London",
                DT = 2643743,
                Wind = new Wind()
                {
                    Speed = 4.1,
                    Degrees = 80
                },
                Main = new Main()
                {
                    Temp = 280.32,
                    Pressure = 1012,
                    Humidity = 81,
                    MaxTemp = 281.15,
                    MinTemp = 279.15
                },
                Weather = new List<Weather>() {
                    new Weather(){
                        Id = 300,
                        Main = "Drizzle",
                        Description = "Light intensity drizzle",
                        Icon = "09d"
                    }
                },
                Sys = new Sys()
                {
                    Country = "GB"
                }
            };
        }
        private ForecastModel CreateNewForecasts()
        {
            return new ForecastModel()
            {
                City = new City() { CityName = "London", Country = "GB" },
                Forecasts = GenerateForecastList()
            };
        }
        private List<ForecastItemModel> GenerateForecastList()
        {
            var forecasts = new List<ForecastItemModel>();
            for (int i = 0; i < 5; i++)
                forecasts.Add(new ForecastItemModel()
                {
                    DT = 1485766800,
                    Temp = new Temp() { Day = 262.65, Max = 262.65, Min = 261.41 },
                    Weather = new List<Weather>() { new Weather() { Id = 800, Description = "sky is clear", Icon = "01d", Main = "Clear" } }
                });
            return forecasts;
        }
    }
}
