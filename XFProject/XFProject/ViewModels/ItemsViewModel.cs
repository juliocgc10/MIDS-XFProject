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
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }


        private ObservableCollection<PhotoUserDto> photoUsers;
        public Command LoadPhotoUsersCommand { get; }
        public Command ShowOtherPhotosCommand { get; }
        public Command ShowMyPhotosCommand { get; }
        
        public Command<PhotoUserDto> PhotoUserTapped { get; }

        private PhotoUserDto photoUser;

        public ItemsViewModel()
        {
            Title = "Mis Fotos";

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);

            PhotoUsers = new ObservableCollection<PhotoUserDto>();
            LoadPhotoUsersCommand = new Command(async (myPhotos) => await ExecuteLoadPhotoUsersCommand(true));
            PhotoUserTapped = new Command<PhotoUserDto>(OnTappedPhotoUser);
            ShowOtherPhotosCommand = new Command(async (myPhotos) => await ExecuteLoadPhotoUsersCommand(false));
            ShowMyPhotosCommand = new Command(async (myPhotos) => await ExecuteLoadPhotoUsersCommand(true));
        }



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

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

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
                //OnItemSelected(value);
            }
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

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}