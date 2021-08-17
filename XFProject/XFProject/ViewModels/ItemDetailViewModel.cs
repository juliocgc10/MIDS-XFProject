using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XFProject.Entities;
using XFProject.Models;


namespace XFProject.ViewModels
{
    [QueryProperty(nameof(PhotoUserId), nameof(PhotoUserId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Fields                
        private CustomPin pinLocation;
        private int photoUserId;
        private PhotoUserDto photoUserDto;
        #endregion

        #region Properties            
        public int PhotoUserId
        {
            get => photoUserId;
            set
            {
                photoUserId = value;
                OnPropertyChanged();
                LoadPhotoUser(value);
            }
        }

        public PhotoUserDto PhotoUserDto
        {
            get => photoUserDto;

            set
            {
                photoUserDto = value;
                OnPropertyChanged();
            }
        }

        public CustomPin PinLocation
        {
            get => pinLocation;
            set
            {
                pinLocation = value;
                OnPropertyChanged();
            }

        }
        #endregion

        #region Constructors
        public ItemDetailViewModel()
        {
            base.Title = "Detalle de la foto";
        }
        #endregion

        #region Methods        
        public async void LoadPhotoUser(int photoUserId)
        {
            try
            {

                HttpClient httpClient = new HttpClient();
                var result = await httpClient.GetAsync($"{AppConstants.ServiceEndpoint}/api/PhotoUser/{photoUserId}");

                if (result.IsSuccessStatusCode)
                {
                    var webPhotoUser = await result.Content.ReadAsStringAsync();
                    PhotoUserDto = JsonConvert.DeserializeObject<PhotoUserDto>(webPhotoUser);

                    PinLocation = new CustomPin() { Label = PhotoUserDto.PhotoTitle, Latitude = Convert.ToDouble(PhotoUserDto.Latitude), Longitude = Convert.ToDouble(PhotoUserDto.Longitude) };
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        #endregion
    }
}
