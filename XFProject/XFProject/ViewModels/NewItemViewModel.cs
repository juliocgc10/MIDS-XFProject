using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XFProject.Entities;
using XFProject.Models;

namespace XFProject.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        //private string text;
        //private string description;
        private PhotoUserDto photoUser;

        public PhotoUserDto PhotoUser
        {
            get => photoUser;
            set
            {
                photoUser = value;
                OnPropertyChanged();
            }
        }

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        //private bool ValidateSave()
        //{
        //    return !String.IsNullOrWhiteSpace(text)
        //        && !String.IsNullOrWhiteSpace(description);
        //}

        //public string Text
        //{
        //    get => text;
        //    set => SetProperty(ref text, value);
        //}

        //public string Description
        //{
        //    get => description;
        //    set => SetProperty(ref description, value);
        //}



        public Command SaveCommand { get; }
        public Command CancelCommand { get; }


        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            //Item newItem = new Item()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Text = Text,
            //    Description = Description
            //};

            //await DataStore.AddItemAsync(newItem);

            HttpClient httpClient = new HttpClient();

            HttpResponseMessage result = await httpClient.PostAsync(
                "https://dev-app-mids.azurewebsites.net/api/PhotoUser",
                new StringContent(JsonConvert.SerializeObject(this.PhotoUser),
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
            await Shell.Current.GoToAsync("..");
        }
    }
}
