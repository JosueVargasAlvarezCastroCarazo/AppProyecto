﻿using AppProyecto.Services;
using AppProyecto.Views;
using AutoAppo_JosueVa.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProyecto
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            App.Current.UserAppTheme = OSAppTheme.Light;
            MainPage = new NavigationPage(new AppLoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
