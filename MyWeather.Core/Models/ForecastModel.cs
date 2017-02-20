using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyWeather.Core.Models
{
    public class ForecastModel
    {
        [JsonProperty(PropertyName = "city")]
        public City City { get; set; }
        [JsonProperty(PropertyName = "list")]
        public List<ForecastItemModel> Forecasts { get; set; }
    }
}
