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
    public partial class AppSignUpPage : ContentPage
    {

        UserViewModel userViewModel;

        public AppSignUpPage()
        {
            InitializeComponent();
            this.BindingContext = userViewModel = new UserViewModel();
        }

        //crea la cuenta del usuario
        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
           
            if (
                (TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim())) &&
                (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
                (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
                (TxtPhone.Text != null && !string.IsNullOrEmpty(TxtPhone.Text.Trim())) &&
                (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim())) &&
                (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()))
                )
            {


                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    User checkUser = await userViewModel.CheckIdentification(TxtIdentification.Text.Trim());
                    if ( checkUser.UserId == 0)
                    {
                        bool R = await userViewModel.CreateAccount(
                                                TxtIdentification.Text.Trim(),
                                                TxtPassword.Text.Trim(),
                                                TxtName.Text.Trim(),
                                                TxtPhone.Text.Trim(),
                                                TxtAddress.Text.Trim(),
                                                TxtEmail.Text.Trim()
                                                );

                        if (R)
                        {
                            await DisplayAlert("Atención", "Usuario creado", "Aceptar");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Atención", "Usuario no creado", "Aceptar");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Ya existe un usuario con esta identificación", "Aceptar");
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

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}