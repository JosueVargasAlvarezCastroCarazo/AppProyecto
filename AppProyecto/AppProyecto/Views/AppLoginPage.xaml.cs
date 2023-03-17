using Acr.UserDialogs;
using AppProyecto;
using AppProyecto.Models;
using AppProyecto.ViewModels;
using AppProyecto.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoAppo_JosueVa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
    {

        UserViewModel userViewModel;

        public AppLoginPage()
        {
            InitializeComponent();
            this.BindingContext = userViewModel = new UserViewModel();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new AppSignUpPage());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (TxtIdentificación.Text != null && !string.IsNullOrEmpty(TxtIdentificación.Text.Trim()))
            {
                if (TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {


                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        await Task.Delay(1000);
                        User R = await userViewModel.ValidateLogin(TxtIdentificación.Text.Trim(), TxtPassword.Text.Trim());

                        if (R.UserId > 0)
                        {
                            Global.user = R;
                            await this.Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await DisplayAlert("Upss", "Usuario NO Encontrado", "OK");
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
                    await DisplayAlert("Error", "La contraseña esta en blanco", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "La identificación esta en blanco", "OK");
            }
        }
    }
}