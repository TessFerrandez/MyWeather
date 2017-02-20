using Newtonsoft.Json;

namespace MyWeather.Core.Models
{
    public class Sys
    {
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
