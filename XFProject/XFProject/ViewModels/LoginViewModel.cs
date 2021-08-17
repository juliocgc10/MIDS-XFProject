using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFProject.Views;

namespace XFProject.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string nickName;
        private string email;

        public string NickName
        {
            get => nickName;
            set
            {
                nickName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private void OnLoginClicked(object obj)
        {
            Preferences.Clear();
            Preferences.Set("PhotoUser_NickName", NickName);
            Preferences.Set("PhotoUser_Email", Email);
            App.Current.MainPage = new AppShell();
        }
    }
}
