using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VueloEspacial.Models;
using VueloEspacial.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VueloEspacial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailArticleView : ContentPage
    {

        public DetailArticleView(ArticleModel article)
        {
            InitializeComponent();
         
            BindingContext = new DetailArticleViewModel(article);

        }

        private void ViewCell_Appearing(object sender, EventArgs e) { 
            var viewCell = sender as ViewCell; 
            UpdateListViewHeight(viewCell);

            commentTextInput.Text = "";

            var lastChild = stkMain.Children.LastOrDefault();
            if (lastChild != null)
                commentScroll.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, true);
        }
      
        private void UpdateListViewHeight(ViewCell viewCell) { 
            try { 
                double height = 0; 
                foreach (var cell in CommentList.ItemsSource) {
                    height += (int)viewCell.View.Measure(1000, 1000, MeasureFlags.IncludeMargins).Minimum.Height;
                   
                }
                CommentList.HeightRequest = height;
                CommentList.Layout(new Rectangle(CommentList.X, CommentList.Y, CommentList.Width, height));
            } catch { 
            } 
        }

    }
}