using Newtonsoft.Json;
namespace UnweWeatherApp.Models
{

    public class Sys
    {
        [JsonProperty("pod")]
        public string Pod { get; set; }
    }

}