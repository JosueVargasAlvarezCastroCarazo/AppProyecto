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
	public partial class HelpRequestCreatePage : ContentPage
	{
        UserViewModel userViewModel;
        public HelpRequestCreatePage ()
		{
			InitializeComponent ();
            this.BindingContext = userViewModel = new UserViewModel();
        }

        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (
               (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim())) &&
               (TxtDescription.Text != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim()))
               )
            {


                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = await userViewModel.SaveHelpResquest(
                         TxtEmail.Text.Trim(),
                         TxtDescription.Text.Trim()
                        );

                    if (R)
                    {
                        await DisplayAlert("Atención", "Soporte Creado", "Aceptar");
                        await this.Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Problemas Inesperados", "Aceptar");
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