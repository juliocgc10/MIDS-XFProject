using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XFProject.Models
{
    public static class Extensions
    {
        public static string ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return Convert.ToBase64String(bytes);
           
        }
    }
}
