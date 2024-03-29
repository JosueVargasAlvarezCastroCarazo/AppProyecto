﻿using Acr.UserDialogs;
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
    public partial class ItemListPage : ContentPage
    {
        ItemViewModel ViewModel;
        public ItemListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ItemViewModel();
        }

        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {
                
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Item> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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


        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemPage());
        }

        //cambia entre inactivos y activos
        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Item> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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
            await this.Navigation.PushAsync(new ItemPage((ListPage.SelectedItem as Item)));
        }

        //busca un texto entre los items
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<Item> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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