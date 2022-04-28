using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UnweWeatherApp.Models;

namespace UnweWeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastDetails : ContentPage
    {
        public ForecastDetails(List forecastDetails)
        {
            InitializeComponent();
            BindingContext = forecastDetails;
        }
    }
}