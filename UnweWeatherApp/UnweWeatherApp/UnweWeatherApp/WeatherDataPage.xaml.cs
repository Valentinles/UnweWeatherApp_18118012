using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UnweWeatherApp
{
    public partial class WeatherDataPage : ContentPage
    {
        private readonly OpenWeatherService _openWeatherService;

        public WeatherDataPage()
        {
            InitializeComponent();
            _openWeatherService = new OpenWeatherService();
            GetWeatherWithGeoLoaction();
            if (Device.RuntimePlatform == Device.Android)
            {
                titleLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                titleLabel.Text = "Running on iOS.";
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
        public string GenerateRequestUriGeo(string endpoint, double lat, double lon)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={lat}&lon={lon}&units=metric&appid={Constants.OpenWeatherMapAPIKey}";

            return requestUri;
        }

        public async void GetWeatherWithGeoLoaction()
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                double lat = location.Latitude;
                double lon = location.Longitude;
                string query = GenerateRequestUriGeo(Constants.OpenWeatherMapEndpoint, lat, lon);
                WeatherData weatherData = await _openWeatherService.GetWeatherData(query);
                BindingContext = weatherData;

                GetDayDuration(weatherData.Sys.Sunrise, weatherData.Sys.Sunset);
            }
        }

        public void GetDayDuration(long sunriseUnix, long sunsetUnix)
        {
            var sunrise = DateTimeOffset.FromUnixTimeSeconds(sunriseUnix).DateTime;
            var sunset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnix).DateTime;
            dayDuration.Text = $"{sunset.Subtract(sunrise).TotalHours} hours";
        }
        public async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _openWeatherService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;

                GetDayDuration(weatherData.Sys.Sunrise, weatherData.Sys.Sunset);
            }

        }
    }
}
