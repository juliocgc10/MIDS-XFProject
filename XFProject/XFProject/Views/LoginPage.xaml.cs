using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFProject.ViewModels;

namespace XFProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
          
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    //_ = CheckLogin();
        //}

        //private async Task CheckLogin()
        //{
        //    // should check for valid login instead
        //    await Task.Delay(2000);

        //    // only open Login page when no valid login
        //    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        //}

        //private async void ButtonLogin_Clicked(object sender, EventArgs e)
        //{
        //    Preferences.Clear();
        //    Preferences.Set("PhotoUser_NickName", txtNickName.Text);
        //    Preferences.Set("PhotoUser_Email", txtEmail.Text);
        //    App.Current.MainPage = new AppShell();
        //    //await this.DisplayAlert("Ingreso", "Los datos se guardarán", "Aceptar");
        //    //await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        //}
    }
}