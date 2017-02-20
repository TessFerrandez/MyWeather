using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyWeather.Core.Models
{
    public class CurrentWeatherDataModel
    {
        [JsonProperty(PropertyName = "sys")]
        public Sys Sys { get; set; }
        [JsonProperty(PropertyName = "weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty(PropertyName = "main")]
        public Main Main { get; set; }
        [JsonProperty(PropertyName = "wind")]
        public Wind Wind { get; set; }
        [JsonProperty(PropertyName = "dt")]
        public int DT { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string CityName { get; set; }
    }

}
