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
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            //this.Children.Add(new WeatherDataPage());
        }
    }
}
