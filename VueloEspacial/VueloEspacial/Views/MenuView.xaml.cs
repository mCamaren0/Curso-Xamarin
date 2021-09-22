using System;
using System.Collections.Generic;
using VueloEspacial.Models;
using VueloEspacial.ViewModels;
using Xamarin.Forms;

namespace VueloEspacial.Views
{
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            InitializeComponent();

            BindingContext = MenuViewModel.GetInstance();
        }
    }
}
