using System;
using VueloEspacial.Models;
using VueloEspacial.ViewModels;
using VueloEspacial.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VueloEspacial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LoginViewModel.GetInstance().GetUserRemember();

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
