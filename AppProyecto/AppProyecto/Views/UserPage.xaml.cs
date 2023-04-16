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
    public partial class UserPage : ContentPage
    {

        UserViewModel ViewModel;
        public User CurrentItem = null;

        public UserPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
        }

        //carga un usuario para editarlo o eliminarlo
        public UserPage(User currentItem)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();

            this.CurrentItem = currentItem;

            TxtIdentification.Text = currentItem.Identification;
            TxtName.Text = currentItem.Name;
            TxtPhoneNumber.Text = currentItem.PhoneNumber;
            TxtAddress.Text = currentItem.Address;
            TxtEmail.Text = currentItem.Email;
            SwitchIsAdmin.IsToggled = (bool)currentItem.IsAdmin;

            BtnActionDelete.IsVisible = true;

            if ((bool)currentItem.Active)
            {
                BtnActionDelete.Text = "Eliminar";
            }
            else
            {
                BtnActionDelete.Text = "Restaurar";
            }

        }

        //actualiza el usuario de la lista
        private async void BtnAction_Clicked(object sender, EventArgs e)
        {

            if (CurrentItem.UserId == Global.user.UserId)
            {
                await DisplayAlert("Atención", "Actualice su información desde la pestaña de mi perfil", "Aceptar");
            }
            else
            {

                if (
                  (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
                  (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
                  (TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim())) &&
                  (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim())) &&
                  (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()))

                  )
                {


                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        bool R = await ViewModel.UpdateProfile(
                            CurrentItem.UserId,
                            TxtIdentification.Text.Trim(),
                            CurrentItem.LoginPassword,
                            TxtName.Text.Trim(),
                            TxtPhoneNumber.Text.Trim(),
                            TxtAddress.Text.Trim(),
                            CurrentItem.Active,
                            SwitchIsAdmin.IsToggled,
                            TxtEmail.Text.Trim()
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

        //elimina o restaura un usuario
        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {

            if (CurrentItem.UserId == Global.user.UserId)
            {
                await DisplayAlert("Atención", "No puede auto eliminar su usuario", "Aceptar");
            }
            else
            {


                if (
                 (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
                 (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
                 (TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim())) &&
                 (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim())) &&
                 (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()))

                 )
                {


                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        bool R = await ViewModel.UpdateProfile(
                            CurrentItem.UserId,
                            TxtIdentification.Text.Trim(),
                            CurrentItem.LoginPassword,
                            TxtName.Text.Trim(),
                            TxtPhoneNumber.Text.Trim(),
                            TxtAddress.Text.Trim(),
                            !CurrentItem.Active,
                            SwitchIsAdmin.IsToggled,
                            TxtEmail.Text.Trim()
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

        private async void SwitchIsAdmin_Toggled(object sender, ToggledEventArgs e)
        {
           
        }
    }
}