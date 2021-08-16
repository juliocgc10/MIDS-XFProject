using System.ComponentModel;
using Xamarin.Forms;
using XFProject.ViewModels;

namespace XFProject.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();            
        }
    }
}