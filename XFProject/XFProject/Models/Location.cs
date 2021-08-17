using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
using XFProject.ViewModels;

namespace XFProject.Models
{
    public class LocationCustom : BaseViewModel
    {
        Position _position;

        public string Address { get; }
        public string Description { get; }

        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public LocationCustom(string address, string description, Position position)
        {
            Address = address;
            Description = description;
            Position = position;
        }
    }
   
}
