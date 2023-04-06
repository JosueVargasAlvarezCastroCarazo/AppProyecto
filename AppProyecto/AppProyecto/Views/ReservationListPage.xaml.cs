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

        bool MyReservations = false;

        public ReservationListPage()
        {
            InitializeComponent();
        }

        public ReservationListPage(bool MyReservations)
        {
            InitializeComponent();
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

        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReservationPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}