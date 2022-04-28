using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Models;

namespace UnweWeatherApp
{
    public class OpenWeatherService
    {
        HttpClient _client;
        public OpenWeatherService()
        {
            _client = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }
            return weatherData;
        }

        public async Task<Root> GetWeatherForecast(string query)
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            Root weatherForecast = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherForecast = JsonConvert.DeserializeObject<Root>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }
            return weatherForecast;
        }
    }
}
