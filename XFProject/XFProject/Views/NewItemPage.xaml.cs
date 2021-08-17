using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using XFProject.Models;
using XFProject.ViewModels;

namespace XFProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        Plugin.Geolocator.Abstractions.Position position = default;

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();

        }

        private async void MostrarMapa_Clicked(object sender, EventArgs e)
        {
            //MapLaunchOptions options = new MapLaunchOptions() { Name = "Mi posición" };
            //await Map.OpenAsync(position.Latitude, position.Longitude, options);

            try
            {

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    await DisplayAlert("Geolocalización no disponible o habilitada", "Consulte la habilitación o que se encuentre disponible la geolocalización en su dispositivo", "Aceptar");
                }
                else
                {
                    lat.Text = position.Latitude.ToString();
                    lon.Text = position.Longitude.ToString();

                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));
                    Pin customPin = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Mi ubicación",
                        Address = "Aquí",
                        Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude)
                    };

                    customPin.MarkerClicked += async (s, args) =>
                    {
                        args.HideInfoWindow = true;
                        await DisplayAlert("Click en el pin", $"{((Pin)s).Label} fue oprimido", "Aceptar");
                    };

                    map.Pins.Add(customPin);
                }
            }
            catch (Exception ex)
            {
                //will catch any general exception and set message in a string
                string msg = ex.Message;
                await DisplayAlert("error", msg, "ok");
            }
        }
    }
}