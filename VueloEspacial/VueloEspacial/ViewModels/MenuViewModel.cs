using Realms;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using VueloEspacial.Models;
using VueloEspacial.Views;
using Xamarin.Forms;

namespace VueloEspacial.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {

        #region Properties
        //private UserModel _User = new UserModel();
        public ObservableCollection<MenuModel> lstMenu { get; set; } = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuModel> lstMenu2 { get; set; } = new ObservableCollection<MenuModel>();

        //private ObservableCollection<MenuModel> _lstMenu = new ObservableCollection<MenuModel>();
        //private ObservableCollection<MenuModel> _lstMenu2 = new ObservableCollection<MenuModel>();
        //private ObservableCollection<UserModel> _lstMenu3 = new ObservableCollection<UserModel>();
        //public ObservableCollection<MenuModel> lstMenu
        //{
        //    get
        //    {
        //        return _lstMenu;
        //    }
        //    set
        //    {
        //        _lstMenu = value;
        //        OnPropertyChanged("lstMenu");
        //    }
        //}

        //public ObservableCollection<MenuModel> lstMenu2
        //{
        //    get
        //    {
        //        return _lstMenu2;
        //    }
        //    set
        //    {
        //        _lstMenu2 = value;
        //        OnPropertyChanged("lstMenu2");
        //    }
        //}

        //public ObservableCollection<UserModel> lstMenu3
        //{
        //    get
        //    {
        //        return _lstMenu3;
        //    }
        //    set
        //    {
        //        _lstMenu3 = value;
        //        OnPropertyChanged("lstMenu3");
        //    }
        //}

        #endregion

        #region Command
        public ICommand EnterMenuOptionCommand { get; set; }
        public ICommand CurrentUserCommand { get; set; }
        #endregion

        #region Singleton

        private static MenuViewModel instance = null;

        public MenuViewModel()
        {
            EnterMenuOptionCommand = new Command<MenuModel>(EnterMenuOption);

            lstMenu.Add(new MenuModel { Id = 1, Name = "News", Icon = "noticias" });
            lstMenu.Add(new MenuModel { Id = 2, Name = "Blogs", Icon = "blogs" });
            lstMenu.Add(new MenuModel { Id = 3, Name = "Reports", Icon = "informes" });
            lstMenu.Add(new MenuModel { Id = 4, Name = "Comments", Icon = "comentarios" });

            lstMenu2.Add(new MenuModel { Id = 1, Name = "News", Icon = "noticias" });
            lstMenu2.Add(new MenuModel { Id = 2, Name = "Blogs", Icon = "blogs" });
            lstMenu2.Add(new MenuModel { Id = 3, Name = "Reports", Icon = "informes" });
            lstMenu2.Add(new MenuModel { Id = 4, Name = "Comments", Icon = "comentarios" });
            lstMenu2.Add(new MenuModel { Id = 5, Name = "Logout", Icon = "logout" });

            //lstMenu3.Add(new UserModel { Id = 1, User = VueloEspacial.Helpers.Settings.UserName, Photo = VueloEspacial.Helpers.Settings.UserPhoto, Email = VueloEspacial.Helpers.Settings.Email });
        }

        public static MenuViewModel GetInstance()
        {
            if (instance == null)
                instance = new MenuViewModel();
            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
                instance = null;
        }
        #endregion

        public async void EnterMenuOption(MenuModel menu)
        {
            switch (menu.Id)
            {
                case 4: //Comentarios
                    await ((FlyoutPage)App.Current.MainPage).Detail.Navigation.PushAsync(new CommentView());
                    ((FlyoutPage)App.Current.MainPage).IsPresented = false;
                    break;
                case 5: //Salir
                    LoginViewModel.GetInstance().Logout();
                    break;
                default: //Noticias,Blog,Reporte
                    await ((FlyoutPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ArticleView(menu.Id, menu.Name));
                    ((FlyoutPage)App.Current.MainPage).IsPresented = false;
                    break;
            }
        }

        #region PropertyChangedImplementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
