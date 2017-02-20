using Newtonsoft.Json;

namespace MyWeather.Core.Models
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }
        [JsonProperty(PropertyName = "deg")]
        public double Degrees { get; set; }
    }
}
