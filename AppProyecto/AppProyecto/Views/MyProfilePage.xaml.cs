using Acr.UserDialogs;
using AppProyecto.Models;
using AppProyecto.ViewModels;
using AppProyecto.Tools;
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
    public partial class MyProfilePage : ContentPage
    {

        UserViewModel userViewModel;

        public MyProfilePage()
        {
            InitializeComponent();
            this.BindingContext = userViewModel = new UserViewModel();

            //carga la info del usuario
            TxtIdentification.Text = Global.user.Identification;
            TxtName.Text = Global.user.Name;
            TxtPhoneNumber.Text = Global.user.PhoneNumber;
            TxtAddress.Text = Global.user.Address;
            TxtEmail.Text = Global.user.Email;
        }

      
        //actualiza el usuario actual
        private async void BtnAction_Clicked(object sender, EventArgs e)
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
                    bool R = await userViewModel.UpdateProfile(
                        Global.user.UserId,
                        TxtIdentification.Text.Trim(),
                        Global.user.LoginPassword,
                        TxtName.Text.Trim(),
                        TxtPhoneNumber.Text.Trim(),
                        TxtAddress.Text.Trim(),
                        Global.user.Active,
                        Global.user.IsAdmin,
                        TxtEmail.Text.Trim()
                        );

                    if (R)
                    {
                        Global.user.Name = TxtName.Text.Trim();
                        Global.user.PhoneNumber = TxtPhoneNumber.Text.Trim();
                        Global.user.Address = TxtAddress.Text.Trim();
                        await DisplayAlert("Atención", "Usuario Modificado", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Usuario no Modificado", "Aceptar");
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


        //actualizala contraseña del usaurio
        private async void BtnUpdatePassword_Clicked(object sender, EventArgs e)
        {
            if (
               (TxtPassword.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
               (TxtPasswordRepeat.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()))
            )
            {
                if (
                   (new Crypto().EncriptarEnUnSentido(TxtPassword.Text) == Global.user.LoginPassword)
                  )
                {

                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        bool R = await userViewModel.UpdateProfilePassword(
                            Global.user.UserId,
                            TxtIdentification.Text.Trim(),
                            TxtPasswordRepeat.Text.Trim(),
                            Global.user.Name,
                            Global.user.PhoneNumber,
                            Global.user.Address,
                            Global.user.Active,
                            Global.user.IsAdmin,
                            Global.user.Email
                            );

                        if (R)
                        {
                            TxtPassword.Text = "";
                            TxtPasswordRepeat.Text = "";
                            await DisplayAlert("Atención", "Contraseña Modificada", "Aceptar");
                        }
                        else
                        {
                            await DisplayAlert("Atención", "Contraseña no Modificada", "Aceptar");
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
                    await DisplayAlert("Atención", "La contraseña antigua no coincide", "Aceptar");
                }

                    

            }
            else
            {
                await DisplayAlert("Atención", "Debe de ingresar todos los datos de contaseña para continuar", "Aceptar");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            TxtPassword.IsPassword = !TxtPassword.IsPassword;
            TxtPasswordRepeat.IsPassword = !TxtPasswordRepeat.IsPassword;
        }
    }
}