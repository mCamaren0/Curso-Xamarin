using System;
using System.Collections.Generic;
using VueloEspacial.ViewModels;
using Xamarin.Forms;

namespace VueloEspacial.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            BindingContext = LoginViewModel.GetInstance();
        }
    }
}
