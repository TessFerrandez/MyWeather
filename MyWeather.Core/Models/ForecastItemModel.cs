using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyWeather.Core.Models
{
    public class ForecastItemModel
    {
        [JsonProperty(PropertyName = "dt")]
        public int DT { get; set; }
        [JsonProperty(PropertyName = "temp")]
        public Temp Temp { get; set; }
        [JsonProperty(PropertyName = "weather")]
        public List<Weather> Weather { get; set; }
    }
}
