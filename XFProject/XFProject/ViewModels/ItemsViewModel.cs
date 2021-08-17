using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFProject.Entities;
using XFProject.Models;
using XFProject.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace XFProject.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Fields
        private PhotoUserDto photoUser;
        private ObservableCollection<PhotoUserDto> photoUsers;
        #endregion

        #region Properties
        public ObservableCollection<PhotoUserDto> PhotoUsers
        {
            get => photoUsers;
            set
            {
                photoUsers = value;
                OnPropertyChanged();
            }
        }
        public PhotoUserDto PhotoUser
        {
            get => photoUser;

            set
            {
                photoUser = value;
            }
        }
        #endregion

        #region Commands
        public Command AddItemCommand { get; }
        public Command LoadPhotoUsersCommand { get; }
        public Command ShowOtherPhotosCommand { get; }
        public Command ShowMyPhotosCommand { get; }
        public Command<PhotoUserDto> PhotoUserTapped { get; }

        #endregion

        #region Constructors
        public ItemsViewModel()
        {
            Title = "Mis Fotos";

            AddItemCommand = new Command(OnAddItem);
            PhotoUsers = new ObservableCollection<PhotoUserDto>();
            LoadPhotoUsersCommand = new Command(async (myPhotos) => await ExecuteLoadPhotoUsersCommand(true));
            PhotoUserTapped = new Command<PhotoUserDto>(OnTappedPhotoUser);
            ShowOtherPhotosCommand = new Command(async (myPhotos) => await ExecuteLoadPhotoUsersCommand(false));
            ShowMyPhotosCommand = new Command(async (myPhotos) => await ExecuteLoadPhotoUsersCommand(true));
        }
        #endregion

        #region Methods
        private async Task ExecuteLoadPhotoUsersCommand(bool myPhotos)
        {
            IsBusy = true;

            try
            {
                PhotoUsers.Clear();

                HttpClient httpClient = new HttpClient();

                Title = myPhotos ? "Mis Fotos" : "Otras Fotos";

                string nickName = Preferences.Get("PhotoUser_NickName", string.Empty);

                HttpResponseMessage result = await httpClient.GetAsync($"{AppConstants.ServiceEndpoint}/api/PhotoUser?isMyPhotos={myPhotos}&autor={nickName}");

                if (result.IsSuccessStatusCode)
                {
                    string webPhotoUsers = await result.Content.ReadAsStringAsync();
                    PhotoUsers = JsonConvert.DeserializeObject<ObservableCollection<PhotoUserDto>>(webPhotoUsers);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnTappedPhotoUser(PhotoUserDto obj)
        {
            if (obj == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.PhotoUserId)}={obj.PhotoUserId}");
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        #endregion


    }
}