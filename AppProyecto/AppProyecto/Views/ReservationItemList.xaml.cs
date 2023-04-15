using Acr.UserDialogs;
using AppProyecto.Models;
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
	public partial class ReservationItemList : ContentPage
	{
        ReservationViewModel ViewModel;
        Item CurrentItem = null;

        public ReservationItemList ()
		{
			InitializeComponent ();
		}


        //constructor que recibe un articulo
        public ReservationItemList(Item currentItem)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
            this.CurrentItem = currentItem;
        }


        // muestra la lista de reservas de un articulo especifico para poder comprobar los horarios
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Reservation> list = new List<Reservation>();

                
                list = await ViewModel.SearchByItemId(CurrentItem.ItemId);
                

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