using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFProject.Services;
using XFProject.Views;

namespace XFProject
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = string.IsNullOrWhiteSpace(Preferences.Get("PhotoUser_NickName", string.Empty)) ? new LoginPage() : (Page)new AppShell();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
