using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VueloEspacial.Models;
using Xamarin.Forms;

namespace VueloEspacial.ViewModels
{
    public class CommentViewModel : INotifyPropertyChanged
    {
        #region Properties
     
        private ObservableCollection<CommentModel> _lstCommentsUser = new ObservableCollection<CommentModel>();

        public ObservableCollection<CommentModel> CommentsUser
        {
            get
            {
                return _lstCommentsUser;
            }
            set
            {
                _lstCommentsUser = value;
                OnPropertyChanged("CommentsUser");
            }
        }


        private static CommentViewModel instance = null;

        #endregion

        #region Commands

        public ICommand DeleteCommentCommand { get; set; }

        #endregion

        public CommentViewModel()
        {
            InitClass();
            InitCommands();
        }

        //public static CommentViewModel GetInstance()
        //{
        //    if (instance == null)
        //        instance = new CommentViewModel();

        //    return instance;
        //}

        private async void InitClass()
        {
           GetCommentsByUser();
        }

        private async void InitCommands()
        {
            DeleteCommentCommand = new Command<CommentModel>(DeleteComment);
        }

        public void GetCommentsByUser()
        {
            try
            {
                _lstCommentsUser = new ObservableCollection<CommentModel>();

                int _id = LoginViewModel.GetInstance().User.Id;

                var realm = Realm.GetInstance();
                var _commentsUser = realm.All<CommentModel>().Where(u => u.IdUser == _id).ToList();

                if (_commentsUser != null)
                {
                    foreach (CommentModel c in _commentsUser)
                    {
                        _lstCommentsUser.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
            }

          
        }

        public async void DeleteComment(CommentModel Comment)
        {
            try
            {
                var realm = Realm.GetInstance();
                var commentUser = realm.All<CommentModel>().FirstOrDefault(u => u.IdComment == Comment.IdComment); //&& u.MailUser == LoginViewModel.GetInstance().User.Email
                using (var transaction = realm.BeginWrite())
                    {
                        realm.Remove(commentUser);
                        transaction.Commit();
                        _lstCommentsUser.Remove(Comment);
                        await App.Current.MainPage.DisplayAlert("Error", "Comment was deleted", "OK");
                    }
             }
            catch (Exception ex)
            {
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
