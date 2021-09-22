using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VueloEspacial.Helpers
{
    public class BindableViewCell : ViewCell
    {
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(BindableViewCell), null);

        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }
    }
}
