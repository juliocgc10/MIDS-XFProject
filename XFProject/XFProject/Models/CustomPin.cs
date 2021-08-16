using System;
using System.Collections.Generic;
using System.Text;

namespace XFProject.Models
{
    public class CustomPin
    {
        public string Label { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }
}
