using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFProject.Entities;
using XFProject.Models;
using XFProject.Views;

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
        private PhotoUserDto photoUser;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //PhotoUsers = new Command(ExecuteLoadPhotoUsersCommand);

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        private Task ExecuteLoadPhotoUsersCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                PhotoUsers = new ObservableCollection<PhotoUserDto>{
                new PhotoUserDto() { PhotoTitle = "Prueba 1", PhotoName = "Nombre 1", PhotoDescription = "Desripción 1", NickNameAutor = "Autor 1", Latitude = "19.6157681", Longitude = "-99.0239452" },
                new PhotoUserDto() { PhotoTitle = "Prueba 2", PhotoName = "Nombre 2", PhotoDescription = "Desripción 2", NickNameAutor = "Autor 2", Latitude = "19.6157681", Longitude = "-99.0239452" },
                new PhotoUserDto() { PhotoTitle = "Prueba 3", PhotoName = "Nombre 3", PhotoDescription = "Desripción 3", NickNameAutor = "Autor 3", Latitude = "19.6157681", Longitude = "-99.0239452" },
                new PhotoUserDto() { PhotoTitle = "Prueba 4", PhotoName = "Nombre 4", PhotoDescription = "Desripción 4", NickNameAutor = "Autor 4", Latitude = "19.6157681", Longitude = "-99.0239452" },
                new PhotoUserDto() { PhotoTitle = "Prueba 5", PhotoName = "Nombre 5", PhotoDescription = "Desripción 5", NickNameAutor = "Autor 5", Latitude = "19.6157681", Longitude = "-99.0239452" }
                };
                ;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            return Task.Run(() => PhotoUsers);
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