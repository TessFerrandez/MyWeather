using Newtonsoft.Json;

namespace MyWeather.Core.Models
{
    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public double Temp { get; set; }
        [JsonProperty(PropertyName = "temp_min")]
        public double MinTemp { get; set; }
        [JsonProperty(PropertyName = "temp_max")]
        public double MaxTemp { get; set; }
        [JsonProperty(PropertyName = "pressure")]
        public double Pressure { get; set; }
        [JsonProperty(PropertyName = "humidity")]
        public double Humidity { get; set; }
    }
}
