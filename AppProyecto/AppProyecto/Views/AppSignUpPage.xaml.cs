using Acr.UserDialogs;
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

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
           
            if (
                (TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim())) ||
                (TxtIdentificación.Text != null && !string.IsNullOrEmpty(TxtIdentificación.Text.Trim())) ||
                (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) ||
                (TxtPhone.Text != null && !string.IsNullOrEmpty(TxtPhone.Text.Trim())) ||
                (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim()))

                )
            {


                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    await Task.Delay(1000);
                    bool R = await userViewModel.CreateAccount(
                        TxtIdentificación.Text.Trim(), 
                        TxtPassword.Text.Trim(),
                        TxtName.Text.Trim(),
                        TxtPhone.Text.Trim(),
                        TxtAddress.Text.Trim()
                        );

                    if (R)
                    {
                        await DisplayAlert("OK", "Usuario creado", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Upss", "Usuario NO creado", "OK");
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
                await DisplayAlert("Error", "Debe de ingresar todos los datos para continuar", "OK");
            }
           
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}