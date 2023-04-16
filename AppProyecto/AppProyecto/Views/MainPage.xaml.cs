using Acr.UserDialogs;
using AppProyecto.Models;
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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //muestra un menu segun si es admin o no
            if (!(bool)Global.user.IsAdmin)
            {
                BtnItems.IsVisible = false;
                BtnReservations.IsVisible = false;
                BtnUsers.IsVisible = false;
                BtnTopReservation.IsVisible = false;
                BtnHelp.IsVisible = false;
            }

        }

        //muestra el texto del usuario
        protected override void OnAppearing()
        {
            TxtUserWelcome.Text = "Bienvenido " + Global.user.Name;
        }

        //my profile
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new MyProfilePage());
        }

        private async void BtnUsers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserListPage());
        }

        private async void BtnItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemListPage());
        }

        private async void BtnReservations_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReservationListPage(false));
        }

        private async void BtnMyReservations_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReservationListPage(true));
        }

        private async void BtnTopReservation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TopPage());
        }

        //help requests
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpRequestCreatePage());
        }

        //abre la lista de soporte
        private async void BtnHelp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpRequestListPage());
        }
    }
}