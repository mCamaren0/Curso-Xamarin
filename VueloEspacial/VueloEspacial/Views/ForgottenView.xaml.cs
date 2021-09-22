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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgottenView : ContentPage
    {
        public ForgottenView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}