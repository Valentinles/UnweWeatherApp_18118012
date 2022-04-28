using Newtonsoft.Json;
namespace UnweWeatherApp.Models
{

    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }

}