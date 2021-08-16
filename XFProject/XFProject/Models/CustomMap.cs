using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XFProject.Models
{
    public class CustomMap : Xamarin.Forms.Maps.Map
    {
        private static IList<Pin> AllPins;

        public Location CenterRegion
        {
            get { return (Location)GetValue(CenterRegionProperty); }
            set { SetValue(CenterRegionProperty, value); }
        }


        public static readonly BindableProperty CenterRegionProperty =
           BindableProperty.Create(propertyName: nameof(CenterRegion), returnType:
           typeof(Location), declaringType: typeof(CustomMap), defaultValue: null,
           propertyChanged: (sender, oldValue, newValue) =>
           {
               CustomMap map = (CustomMap)sender;

               if (newValue is Location location)
               {
                   map.MoveToRegion(MapSpan.FromCenterAndRadius(new
                   Position(location.Latitude, location.Longitude),
                   Distance.FromMiles(3)));

               }
           });


        public CustomPin CustomPins
        {
            get { return (CustomPin)GetValue(CustomPinsProperty); }
            set { SetValue(CustomPinsProperty, value); }
        }

        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create(propertyName: nameof(CustomPins), returnType:
            typeof(CustomPin), declaringType: typeof(CustomMap), defaultValue: null,
            propertyChanged: (sender, oldValue, newValue) =>
            {
                CustomMap map = (CustomMap)sender;

                AllPins = new List<Pin>();

                
                if (newValue is CustomPin location)
                {
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new
                  Position(location.Latitude, location.Longitude),
                  Distance.FromMiles(3)));

                    Position position = new Position(location.Latitude, location.Longitude);
                    Pin pin = new Pin
                    {
                        Position = position,
                        Label = location.Label
                    };

                    map.Pins.Add(pin);
                }

                AllPins = map.Pins;
                map.OnPropertyChanged("Pins");
            });


        //public static List<Pin> ConvertSpacesToPins(IEnumerable spaces)
        //{
        //    if (spaces == null)
        //        return null;

        //    List<Pin> result = new List<Pin>();

        //    foreach (PinCustom space in spaces.OfType<PinCustom>())
        //    {
        //        double latitude = space.Latitude;
        //        double longitude = space.Longitude;
        //        string spaceTitle = space.Label;
        //        Position position = new Position(latitude, longitude);

        //        Pin pin = new Pin
        //        {
        //            Position = position,
        //            Label = spaceTitle
        //        };
        //        result.Add(pin);
        //    }
        //    return result;
        //}
    }
}
