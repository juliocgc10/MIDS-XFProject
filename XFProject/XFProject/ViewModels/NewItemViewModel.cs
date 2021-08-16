using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFProject.Entities;
using XFProject.Models;

namespace XFProject.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {

        #region Fields
        private PhotoUserDto photoUserDto;
        private Stream streamImage;
        string photoPath;
        #endregion

        #region Properties
        public PhotoUserDto PhotoUserDto
        {
            get => photoUserDto;
            set
            {
                photoUserDto = value;
                OnPropertyChanged();
            }
        }

        public string PhotoPath
        {
            get => photoPath;
            set
            {
                if (photoPath == value)
                    return;
                photoPath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public NewItemViewModel()
        {
            Guid guidImage = Guid.NewGuid();
            PhotoUserDto = new PhotoUserDto()
            {
                NickNameAutor = Preferences.Get("PhotoUser_NickName", string.Empty),
                Email = Preferences.Get("PhotoUser_Email", string.Empty),
                PhotoID = guidImage,
                PathUrl = $"{AppConstants.PathUrlFiles}/",
                PhotoName = $"{guidImage}.jpg"
            };

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            TakePhotoCommand = new Command(OnTakePhoto);
            PickPhotoCommand = new Command(OnPickPhoto);
        }       

        #endregion


        #region Commands
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command TakePhotoCommand { get; }
        public Command PickPhotoCommand { get; }
        #endregion


        #region Methods
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
             await Shell.Current.GoToAsync(nameof(Views.ItemsPage));
        }

        private async void OnSave()
        {
            HttpClient httpClient = new HttpClient();

            PhotoUserDto.PhotoBase64 = streamImage.ConvertToBase64();


            HttpResponseMessage result = await httpClient.PostAsync($"{AppConstants.ServiceEndpoint}/api/PhotoUser",
                new StringContent(JsonConvert.SerializeObject(this.PhotoUserDto),
                encoding: Encoding.UTF8, mediaType: "application/json"));

            if (result.IsSuccessStatusCode)
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    Message = "Se agregaron los datos de la foto correctamente",
                    OkText = "Aceptar",
                    Title = "Foto agregada"
                });

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    Message = "No se agregaron los datos de la foto correctamente",
                    OkText = "Aceptar",
                    Title = "Foto no agregado"
                });
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(nameof(Views.ItemsPage));
        }

        async void OnTakePhoto()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            await LoadPhotoAsync(photo);
        }        

        private async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            streamImage = await photo.OpenReadAsync();
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
            }
            PhotoPath = newFile;
        }

        private async void OnPickPhoto()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            await LoadPickPhotoAsync(result);

            
        }


        private async Task LoadPickPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            streamImage = await photo.OpenReadAsync();
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
            }

            //if (result != null)
            //{
            //    var stream = await result.OpenReadAsync();

            //    resultImage.Source = ImageSource.FromStream(() => stream);
            //}
            PhotoPath = newFile;
        }

        //private bool ValidateSave()
        //{
        //    return !String.IsNullOrWhiteSpace(text)
        //        && !String.IsNullOrWhiteSpace(description);
        //}       
        #endregion


    }
}
