using Newtonsoft.Json;

namespace MyWeather.Core.Models
{
    public class Temp
    {
        [JsonProperty(PropertyName = "day")]
        public double Day { get; set; }
        [JsonProperty(PropertyName = "min")]
        public double Min { get; set; }
        [JsonProperty(PropertyName = "max")]
        public double Max { get; set; }
    }
}
