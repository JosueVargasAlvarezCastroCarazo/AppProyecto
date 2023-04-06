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
    public partial class SelectItemPage : ContentPage
    {
        ItemViewModel ViewModel;
        ReservationPage Page;

        public SelectItemPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ItemViewModel();
        }

        public SelectItemPage(ReservationPage page)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ItemViewModel();
            this.Page = page;
        } 

        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Item> list = await ViewModel.GetList(true, TxtSearch.Text.Trim());
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

        private void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Page.SelectedItem = (ListPage.SelectedItem as Item);
            this.Navigation.PopAsync();
        }

        //busca un texto entre los items
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Item> list = await ViewModel.GetList(true, TxtSearch.Text.Trim());
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
    }
}