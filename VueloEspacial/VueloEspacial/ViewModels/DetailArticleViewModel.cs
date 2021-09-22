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
  
    public class DetailArticleViewModel : INotifyPropertyChanged
    {

        #region Properties

        public ObservableCollection<CommentModel> Comments { get; set; } = new ObservableCollection<CommentModel>();

        public string TextToSend { get; set; }

        public ICommand OnSendCommand { get; set; }

        public ArticleModel CurrentArticle { get; set; } = new ArticleModel();

        #endregion

        public DetailArticleViewModel(ArticleModel article)
        {
            InitClass(article);
            InitCommands();
        }

        private async void InitClass(ArticleModel article)
        {
            CurrentArticle = article;

            Comments = new ObservableCollection<CommentModel>();
            Comments.Add(new CommentModel { IdComment = 1, IdArticle = 1, NameUser = "Carlos Baltodano", Icon = "hombre", Comment = "buena noticia" });
            Comments.Add(new CommentModel { IdComment = 2, IdArticle = 1, NameUser = "Lady Guzman", Icon = "mujer", Comment = "no me gusto la noticia" });

            Comments = GetComments();

        }

        private ObservableCollection<CommentModel> GetComments() {
            try {
                int _id = CurrentArticle.id;

                var realm = Realm.GetInstance();
                var _comments = realm.All<CommentModel>().Where(u => u.IdArticle == _id).ToList();


                if (_comments != null)
                {
                    foreach (CommentModel c in _comments)
                    {
                        Comments.Add(c);
                    }
                }

            } catch (Exception ex) {
                //await App.Current.MainPage.DisplayAlert("Error", "error: " + ex.Message, "OK");
            }
         
            return Comments;
        }

        private void RegisterComment(CommentModel comment)
        {
            try {
                var realm = Realm.GetInstance();

                var comments = realm.All<CommentModel>();

                //Dar un consecutivo
                comment.IdComment = comments.Count() + 1;

                realm.Write(() =>
                {
                    realm.Add(comment);
                });

                //comment = new CommentModel();

                //await App.Current.MainPage.DisplayAlert("Success", "Comment created successfully", "OK");

            } catch (Exception ex) {
                string error = ex.Message;
                //await App.Current.MainPage.DisplayAlert("Error", "error: " + ex.Message, "OK");
            }
          
        }

        private async void InitCommands()
        {
            OnSendCommand = new Command(OnSend);
        }

        public void OnSend()
        {
            if (!string.IsNullOrEmpty(TextToSend))
            {
                UserModel user = LoginViewModel.GetInstance().User;
                int IdArt = CurrentArticle.id;

                CommentModel comment = new CommentModel
                {
                    IdArticle = IdArt,
                    IdUser = user.Id,
                    NameUser = user.FirstName + " " + user.LastName,
                    ArticleName = CurrentArticle.title,
                    ArticleImage = CurrentArticle.imageUrl,
                    Icon = user.Photo,
                    Comment = TextToSend
                };
                //CommentModel comment = new CommentModel { IdArticle = CurrentArticle.id, NameUser = "Usuario logeado", Icon = "usuario", Comment = TextToSend };

                RegisterComment(comment);

                Comments.Add(comment);


                TextToSend = string.Empty;
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
