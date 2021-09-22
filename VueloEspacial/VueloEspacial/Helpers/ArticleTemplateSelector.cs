using System;
using VueloEspacial.Models;
using VueloEspacial.Views.Cells;
using Xamarin.Forms;

namespace VueloEspacial.Helpers
{
    public class ArticleTemplateSelector : DataTemplateSelector
    {

        DataTemplate ImageSmallDataTemplate;
        DataTemplate ImageBigDataTemplate;

        public ArticleTemplateSelector()
        {
            this.ImageSmallDataTemplate = new DataTemplate(typeof(ImageSmallViewCell));
            this.ImageBigDataTemplate = new DataTemplate(typeof(ImageBigViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var article = item as ArticleModel;

            if (article == null)
                return null;

            if (article.primero)
            {
                this.ImageBigDataTemplate.SetValue(BindableViewCell.ParentBindingContextProperty, container.BindingContext);
                return this.ImageBigDataTemplate;
            } else
            {
                this.ImageSmallDataTemplate.SetValue(BindableViewCell.ParentBindingContextProperty, container.BindingContext);
                return this.ImageSmallDataTemplate;
            }
          
        }
    }
}
