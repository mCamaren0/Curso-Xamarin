using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueloEspacial.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VueloEspacial.Views
{
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}