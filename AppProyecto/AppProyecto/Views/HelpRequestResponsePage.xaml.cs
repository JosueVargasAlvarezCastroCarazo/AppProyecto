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
	public partial class HelpRequestResponsePage : ContentPage
	{

		public HelpRequest CurrentItem;
        UserViewModel userViewModel;

        public HelpRequestResponsePage ()
		{
			InitializeComponent ();
            this.BindingContext = userViewModel = new UserViewModel();
        }

        public HelpRequestResponsePage(HelpRequest CurrentItem)
        {
            InitializeComponent();
            this.BindingContext = userViewModel = new UserViewModel();
            this.CurrentItem = CurrentItem;
            TxtEmail.Text = CurrentItem.Email;
            TxTRequest.Text = CurrentItem.Message;
        }


        //response request
        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (
              (TxTResponse.Text != null && !string.IsNullOrEmpty(TxTResponse.Text.Trim()))
              )
            {


                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = await userViewModel.DeleteHelpResquest(CurrentItem.Id);
                    R = userViewModel.SendEmailHelp(CurrentItem.Email,TxTResponse.Text.Trim());
                    
                    if (R)
                    {
                        await DisplayAlert("Atención", "Soporte Finalizado", "Aceptar");
                        await this.Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Problemas al responder el soporte", "Aceptar");
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