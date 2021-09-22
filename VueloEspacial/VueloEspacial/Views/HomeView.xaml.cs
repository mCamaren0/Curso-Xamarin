using System;
using System.Collections.Generic;
using VueloEspacial.ViewModels;
using Xamarin.Forms;

namespace VueloEspacial.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            BindingContext =  MenuViewModel.GetInstance();
        }
    }
}
