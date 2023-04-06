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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace AppProyecto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationPage : ContentPage
    {

        UserViewModel UserViewModel;
        ItemViewModel ItemViewModel;
        ReservationViewModel ViewModel;

        public User SelectedUser = null;
        public Item SelectedItem = null;
        public Reservation CurrentItem = null;

        public ReservationPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
            UserViewModel = new UserViewModel();
            ItemViewModel = new ItemViewModel();

            StartDate.DateTime = DateTime.Now;
            EndDate.DateTime = DateTime.Now;

        }


        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            bool check = await CheckDateTime();
            if (check)
            {
                if (
                    (TxtDescription.Text != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim())) &&
                    (TxtNameItem.Text != null && !string.IsNullOrEmpty(TxtNameItem.Text.Trim()))
                )
                {

                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        bool R = false;

                        //actualiza o crea la reservacion
                        if (CurrentItem == null)
                        {
                            
                            R = await ViewModel.Create(
                                Global.user.UserId,
                                SelectedItem.ItemId,
                                StartDate.DateTime,
                                EndDate.DateTime,
                                TxtDescription.Text.Trim()
                            );
                        }
                        else
                        {
                            /*R = await ViewModel.Update(
                                CurrentItem.ItemId,
                                TxtName.Text.Trim(),
                                TxtCode.Text.Trim(),
                                TxtDescription.Text.Trim(),
                                CurrentItem.Active
                            );*/
                        }



                        if (R)
                        {
                            await DisplayAlert("Atención", "Proceso finalizado correctamente", "Aceptar");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Atención", "Ocurrió un error realizando el proceso es posible que las fechas no esten disponibles", "Aceptar");
                        }

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
                else
                {
                    await DisplayAlert("Atención", "Debe de ingresar todos los datos para continuar", "Aceptar");
                }
            }
        }

        private void BtnActionDelete_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnSearch_Clicked(object sender, EventArgs e)
        {
           
            await this.Navigation.PushAsync(new SelectItemPage(this));

        }

        protected override void OnAppearing()
        {
            if (SelectedItem != null)
            {
                TxtNameItem.Text = SelectedItem.ItemName;
            }
        }

        private async Task<bool> CheckDateTime()
        {
            if (StartDate.DateTime >= EndDate.DateTime || StartDate.DateTime <= DateTime.Now)
            {
                await DisplayAlert("Atención", "La fecha de inicio debe de ser mayor a la actual y la fecha de fin debe ser mayor a la fecha de inicio", "Aceptar");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}