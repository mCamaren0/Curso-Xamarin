using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using VueloEspacial.Models;
using VueloEspacial.Views;
using Xamarin.Forms;

namespace VueloEspacial.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<ArticleModel> _lstArticles = new ObservableCollection<ArticleModel>();

        public ObservableCollection<ArticleModel> lstArticles
        {
            get
            {
                return _lstArticles;
            }
            set
            {
                _lstArticles = value;
                OnPropertyChanged("lstArticles");
            }
        }

        private static string _pageTitle = "";

        public string pageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
                OnPropertyChanged("pageTitle");
            }
        }

        #endregion

        #region Commands

        public ICommand EnterArticleDetailCommand { get; set; }
        
        #endregion

        #region Singleton


        public ArticleViewModel(int id, string pageTitle)
        {
            _pageTitle = pageTitle;
            InitClass(id);
            InitCommand();
        }
      
        #endregion

        public async void InitClass(int id)
        {
            lstArticles = await ArticleModel.GetAllArticles(id);
        }

        public void InitCommand()
        {
            EnterArticleDetailCommand = new Command<ArticleModel>(EnterArticleDetail);
        }


        public  void EnterArticleDetail(ArticleModel article)
        {
            //CurrentDoctor = lstDoctors.Where(x => x.Id == doctor.Id).FirstOrDefault();
          
           // ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new DetailArticleView(article));
            ((FlyoutPage)App.Current.MainPage).Detail.Navigation.PushAsync(new DetailArticleView(article));
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
