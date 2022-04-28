using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForecastPage : ContentPage
    {

        private readonly OpenWeatherService _openWeatherService;
        public WeatherForecastPage()
        {
            InitializeComponent();
            _openWeatherService = new OpenWeatherService();
            GetWeatherForecastWithGeoLoaction();
        }

        public string GenerateRequestUriFiveDaysForecast(string endpoint, double lat, double lon)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={lat}&lon={lon}&units=metric&appid={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
        public async void GetWeatherForecastWithGeoLoaction()
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                double lat = location.Latitude;
                double lon = location.Longitude;
                string query = GenerateRequestUriFiveDaysForecast(Constants.OpenWeatherMapForecastEndpoint, lat, lon);
                Root weatherForecast = await _openWeatherService.GetWeatherForecast(query);
                BindingContext = weatherForecast;
                foreach (var forecast in weatherForecast.List)
                {
                    DateTime dateValue = DateTime.Parse(forecast.DtTxt);
                    forecast.DtTxt = dateValue.ToString("dddd") + " " + dateValue.ToString("HH:mm");
                }
                ForecastsList.ItemsSource = weatherForecast.List;
            }
        }

        private async void ForecastsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var forecastDetails = e.Item as List;
            await Navigation.PushAsync(new ForecastDetails(forecastDetails));
        }
    }
}