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

    
        ReservationViewModel ViewModel;

        public Item SelectedItem = null;
        public Reservation CurrentItem = null;

        public ReservationPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
            StartDate.DateTime = DateTime.Now;
            EndDate.DateTime = DateTime.Now;
        }

        public ReservationPage(Reservation CurrentItem)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ReservationViewModel();
            StartDate.DateTime = CurrentItem.StartDate;
            EndDate.DateTime = CurrentItem.EndDate;
            this.CurrentItem = CurrentItem;
            SelectedItem = new Item();
            SelectedItem.ItemId = CurrentItem.ItemId;
            SelectedItem.ItemName = CurrentItem.ItemName;
            BtnActionDelete.IsVisible = true;
            TxtDescription.Text = CurrentItem.Notes;
            BtnSearch.IsVisible = false;
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
                            R = await ViewModel.Update(
                               CurrentItem.ReservationId,
                               CurrentItem.UserId,
                               SelectedItem.ItemId,
                               StartDate.DateTime,
                               EndDate.DateTime,
                               TxtDescription.Text.Trim()
                           );
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

        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                bool R = false;

                //Elimina la reserva
                
                R = await ViewModel.Delete(
                    CurrentItem.ReservationId
                );
                
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

        private async void BtnCheckReservations_Clicked(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                await this.Navigation.PushAsync(new ReservationItemList(SelectedItem));
            }
            else
            {
                await DisplayAlert("Atención", "Artefacto no seleccionado", "Aceptar");

            }

        }
    }
}