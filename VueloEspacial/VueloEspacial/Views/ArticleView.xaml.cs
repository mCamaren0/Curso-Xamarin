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
    public partial class ArticleView : ContentPage
    {
        public ArticleView(int id, string pageTitle)
        {
            InitializeComponent();
            BindingContext = new ArticleViewModel(id, pageTitle);
        }
    }
}