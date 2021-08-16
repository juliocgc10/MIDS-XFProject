using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFProject.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            txtNickName.Text = Preferences.Get("PhotoUser_NickName", string.Empty);
            txtEmail.Text = Preferences.Get("PhotoUser_Email", string.Empty);

            if (!string.IsNullOrWhiteSpace(txtNickName.Text))
            {
                Shell.Current.GoToAsync(nameof(ItemsPage));
            }
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Preferences.Set("PhotoUser_NickName", txtNickName.Text);
            Preferences.Set("PhotoUser_Email", txtEmail.Text);
            await this.DisplayAlert("Ingreso", "Los datos se guardarán", "Aceptar");
        }

    }
}