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
    public partial class ItemPage : ContentPage
    {
        ItemViewModel ViewModel;
        public Item CurrentItem = null;

        public ItemPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ItemViewModel();
        }

        //carga un item para editarlo o eliminarlo
        public ItemPage(Item currentItem)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ItemViewModel();

            this.CurrentItem = currentItem;

            TxtCode.Text = currentItem.Code;
            TxtName.Text = currentItem.ItemName;
            TxtDescription.Text = currentItem.ItemDescription;
            BtnActionDelete.IsVisible = true;

            if (currentItem.Active)
            {
                BtnActionDelete.Text = "Eliminar";
            }
            else
            {
                BtnActionDelete.Text = "Restaurar";
            }

        }

        //crea o actualiza items
        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (
               (TxtCode.Text != null && !string.IsNullOrEmpty(TxtCode.Text.Trim())) &&
               (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
               (TxtDescription.Text != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim()))

               )
            {


                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = false;

                    //actualiza o crea el item
                    if (CurrentItem == null)
                    {
                        R = await ViewModel.Create(
                        TxtName.Text.Trim(),
                        TxtCode.Text.Trim(),
                        TxtDescription.Text.Trim()
                        );
                    }
                    else
                    {
                        R = await ViewModel.Update(
                            CurrentItem.ItemId,
                            TxtName.Text.Trim(),
                            TxtCode.Text.Trim(),
                            TxtDescription.Text.Trim(),
                            CurrentItem.Active
                        );
                    }

                    

                    if (R)
                    {
                        await DisplayAlert("Atención", "Proceso finalizado correctamente", "Aceptar");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Ocurrió un error realizando el proceso", "Aceptar");
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

        //elimina o restraura el item actual
        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {
            if (
              (TxtCode.Text != null && !string.IsNullOrEmpty(TxtCode.Text.Trim())) &&
              (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
              (TxtDescription.Text != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim()))
              )
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = await ViewModel.Update(
                        CurrentItem.ItemId,
                        TxtName.Text.Trim(),
                        TxtCode.Text.Trim(),
                        TxtDescription.Text.Trim(),
                        !CurrentItem.Active
                        );

                    if (R)
                    {
                        await DisplayAlert("Atención", "Proceso finalizado correctamente", "Aceptar");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Ocurrió un error realizando el proceso", "Aceptar");
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
}