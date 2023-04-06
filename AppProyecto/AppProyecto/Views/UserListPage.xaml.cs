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
    public partial class UserListPage : ContentPage
    {

        UserViewModel ViewModel;

        public UserListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
        }


        //carga una lista de usuarios
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<User> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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

        //Busca usuarios en el sistema
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<User> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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

        //muestra si se quieren ver usuarios activos
        private async void SwitchShowDisable_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<User> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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
            await this.Navigation.PushAsync(new UserPage((ListPage.SelectedItem as User)));
        }
    }
}