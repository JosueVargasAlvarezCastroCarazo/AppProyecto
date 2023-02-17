using AppProyecto.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppProyecto.Views
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