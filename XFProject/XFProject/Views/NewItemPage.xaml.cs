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
        public Item Item { get; set; }
        Plugin.Geolocator.Abstractions.Position position = default;

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();

            Task.Run(async () =>
            {
                //try
                //{
                    
                //    var locator = CrossGeolocator.Current;
                //    locator.DesiredAccuracy = 50;

                //    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
                //    if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                //    {
                //        //not available or enabled
                //        await DisplayAlert("Geolocalización no disponible o habilitada", "Consulte la habilitación o que se encuentre disponible la geolocalización en su dispositivo", "Aceptar");
                //    }
                //    else
                //    {
                //        lat.Text = position.Latitude.ToString();
                //        lon.Text = position.Longitude.ToString();

                //        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));
                //        Pin customPin = new Pin()
                //        {
                //            Type = PinType.Place,
                //            Label = "Mi ubicación",
                //            Address = "Aquí",
                //            Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude)
                //        };

                //        customPin.MarkerClicked += async (s, args) =>
                //        {
                //            args.HideInfoWindow = true;
                //            await DisplayAlert("Click en el pin", $"{((Pin)s).Label} fue oprimido", "Aceptar");
                //        };

                //        map.Pins.Add(customPin);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //will catch any general exception and set message in a string
                //    string msg = ex.Message;
                //    await DisplayAlert("error", msg, "ok");
                //}
            });

            //Task.Run(async () =>
            //{
            //    position = await GetCurrentPosition();
            //});
        }

        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentPosition()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    await this.DisplayAlert("Localización ni disponibles", "Location not available", "Aceptar");
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            lat.Text = position.Latitude.ToString();
            lon.Text = position.Longitude.ToString();

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            Debug.WriteLine(output);

            return position;
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

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromKilometers(1)));
            //Pin customPin = new Pin()
            //{
            //    Type = PinType.Place,
            //    Label = "Mi ubicación",
            //    Address = "Aquí",
            //    Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude)
            //};

            //customPin.MarkerClicked += async (s, args) =>
            //{
            //    args.HideInfoWindow = true;
            //    await DisplayAlert("Click en el pin", $"{((Pin)s).Label} fue oprimido", "Aceptar");
            //};

            //map.Pins.Add(customPin);
        }
        //private async void PickPhoto_Clicked(object sender, EventArgs e)
        //{
        //    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        //    {
        //        Title = "Please pick a photo"
        //    });

        //    if (result != null)
        //    {
        //        var stream = await result.OpenReadAsync();

        //        resultImage.Source = ImageSource.FromStream(() => stream);
        //    }
        //}


        //private async void TakePhoto_Clicked(object sender, EventArgs e)
        //{
        //    var result = await MediaPicker.CapturePhotoAsync();

        //    if (result != null)
        //    {
        //        var stream = await result.OpenReadAsync();

        //        resultImage.Source = ImageSource.FromStream(() => stream);
        //    }
        //}
    }
}