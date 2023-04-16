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
	public partial class HelpRequestListPage : ContentPage
	{

		UserViewModel ViewModel;
		
		public HelpRequestListPage ()
		{
			InitializeComponent ();
            this.BindingContext = ViewModel = new UserViewModel();
        }


        // muestra la lista de soportes
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<HelpRequest> list = new List<HelpRequest>();


                list = await ViewModel.GetListGetHelpResquest();


                ListPage.ItemsSource = list;

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

        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new HelpRequestResponsePage((ListPage.SelectedItem as HelpRequest)));
        }
    }
}