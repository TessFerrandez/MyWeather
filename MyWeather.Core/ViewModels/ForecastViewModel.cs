using MyWeather.Core.Models;
using System;

namespace MyWeather.Core.ViewModels
{
    public class ForecastViewModel
    {
        public static ForecastViewModel FromModel(ForecastItemModel model)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddSeconds(model.DT);

            return new ForecastViewModel()
            {
                DayOfWeek = time.DayOfWeek.ToString(),
                DayTemp = $"{model.Temp.Day:0.#}°C",
                Icon = $"http://openweathermap.org/img/w/{model.Weather[0].Icon}.png",
                Description = model.Weather[0].Description
            };
        }
        public string DayOfWeek { get; set; }
        public string Icon { get; set; }
        public string DayTemp { get; set; }
        public string Description { get; set; }
    }
}
