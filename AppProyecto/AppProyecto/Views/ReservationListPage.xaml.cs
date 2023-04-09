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
    public partial class ReservationListPage : ContentPage
    {
        ReservationViewModel ViewModel;
        bool MyReservations = false;

        public ReservationListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
        }

        public ReservationListPage(bool MyReservations)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
            this.MyReservations = MyReservations;


            if (MyReservations)
            {
                LblReservas.Text = "Mis Reservas";
            }
            else
            {
                BtnCreate.IsVisible = false;
            }
        }

        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Reservation> list = new List<Reservation>();

                if (MyReservations)
                {
                    list = await ViewModel.GetList(Global.user.UserId, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
                }
                else
                {
                    list = await ViewModel.GetList(0, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
                }

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

        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReservationPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Reservation> list = new List<Reservation>();

                if (MyReservations)
                {
                    list = await ViewModel.GetList(Global.user.UserId,StartDate.Date,EndDate.Date);
                }
                else
                {
                    list = await ViewModel.GetList(0, StartDate.Date, EndDate.Date);
                }
                
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

        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new ReservationPage((ListPage.SelectedItem as Reservation)));
        }
    }
}