using Acr.UserDialogs;
using AppProyecto.Models;
using AppProyecto.ModelsDTOs;
using AppProyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProyecto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopPage : ContentPage
    {
        ReservationViewModel ViewModel;
        Item CurrentItem = null;

        public TopPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
        }


        // muestra la lista de reservas de articulos top
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<ReservationCount> list = new List<ReservationCount>();


                list = await ViewModel.TopItems();


                ListPage.ItemsSource = list;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}