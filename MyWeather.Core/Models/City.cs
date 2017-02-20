using Newtonsoft.Json;
using System;

namespace MyWeather.Core.Models
{
    public class City
    {
        [JsonProperty(PropertyName = "name")]
        public String CityName { get; set; }
        [JsonProperty(PropertyName = "country")]
        public String Country { get; set; }
    }
}
