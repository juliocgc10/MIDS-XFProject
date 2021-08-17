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
        #region Fields
        private string nickName;
        private string email;
        #endregion

        #region Properties
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
        #endregion

        public Command LoginCommand { get; }

        #region Constructor
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }
        #endregion

        #region Methods
        private void OnLoginClicked(object obj)
        {
            Preferences.Clear();
            Preferences.Set("PhotoUser_NickName", NickName);
            Preferences.Set("PhotoUser_Email", Email);
            App.Current.MainPage = new AppShell();
        } 
        #endregion
    }
}
