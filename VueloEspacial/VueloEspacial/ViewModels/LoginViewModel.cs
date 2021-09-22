using Plugin.Media;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VueloEspacial.Models;
using VueloEspacial.Views;
using Xamarin.Forms;
namespace VueloEspacial.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties

        private UserModel _User = new UserModel();

        public UserModel User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }



        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get
            {
                return _ImageSource;
            }

            set
            {
                _ImageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
      

        #endregion

        #region Command

        public ICommand LoginCommand { get; set; }
        public ICommand EnterRegisterCommand { get; set; }
        public ICommand EnterForgottenCommand { get; set; }
        public ICommand ExitRegisterCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        #endregion


        #region Singleton

        private static LoginViewModel instance = null;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            EnterRegisterCommand = new Command(EnterRegister);
            ExitRegisterCommand = new Command(ExitRegister);
            RegisterCommand = new Command(Register);
            EnterForgottenCommand = new Command(EnterForgotten);
            UpdateCommand = new Command(Update);

            TakePhotoCommand = new Command(TakePhoto);

        }

        public static LoginViewModel GetInstance()
        {
            if (instance == null)
                instance = new LoginViewModel();
            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
                instance = null;
        }
        #endregion

        public async void TakePhoto()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions { SaveMetaData = true, SaveToAlbum = true, Directory = "drawable" });
            try
            {
                if (file == null)
                {
                    return;
                }

                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    _User.Photo = file.AlbumPath;
                    return stream;
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void Register()
        {
            try
            {
                var realm = Realm.GetInstance();
                var users = realm.All<UserModel>();
                //var user_exist = realm.All<UserModel>().Where(u => u.Email == _User.Email).FirstOrDefault();

                //Dar un consecutivo
                //_User.Id = users.Count() + 1;

                string Msj = null;

                if(_User.FirstName == null)
                {
                    Msj = "First name can't be null";
                }
                if (_User.LastName == null)
                {
                    Msj = "Last name can't be null";
                }
                if (_User.Cellphone == null)
                {
                    Msj = "Cellphone number can't be null";
                }
                if (_User.Email == null)
                {
                    Msj = "Email can't be null";
                }
                if (_User.Password == null)
                {
                    Msj = "Password can't be null";
                }

                if(Msj == null || Msj != "")
                {
               
                    var email = users.Where(u => u.Email == _User.Email).FirstOrDefault();
                    if (email == null)
                    {
                        _User.Id = users.Count() + 1;
                        realm.Write(() =>
                        {
                            realm.Add(_User);
                        });
                        Clean();
                        await App.Current.MainPage.DisplayAlert("Success", "User created successfully", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Success", "User mail exists, please check your email address", "OK");
                    }
                    
                 
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "User not created, "+ Msj, "OK");
                }               
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Login error: " + ex.Message, "OK");
            }
            finally
            {
                App.Current.MainPage = new LoginView();
            }
        }

        public async void Update()
        {
            try
            {
                var realm = Realm.GetInstance();
                var dbUser = realm.All<UserModel>().Where(u => u.Email == _User.Email).FirstOrDefault();

                if (dbUser != null && dbUser.Email == _User.Email)
                {
                    realm.Write(() =>
                    {
                        dbUser.Password = _User.Password;
                        realm.Add(dbUser);
                    });
                    await App.Current.MainPage.DisplayAlert("Success", "User was updated succesfully", "OK");
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "User wasn't updated, error: " + ex.Message, "OK");
            }
            finally
            {
                App.Current.MainPage = new LoginView();
            }
        }

        private void Clean()
        {
            _User = new UserModel();
        }

        public async void EnterRegister()
        {
            App.Current.MainPage = new RegisterView();
        }

        public async void EnterForgotten()
        {
            App.Current.MainPage = new ForgottenView();
        }

        public async void ExitRegister()
        {
            App.Current.MainPage = new LoginView();
        }

        public async void Login()
        {
            try
            {
                var realm = Realm.GetInstance();

                var dbUser = realm.All<UserModel>().Where(u => u.Email == _User.Email).FirstOrDefault();

                if (dbUser == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "Usuario no existe", "OK");
                }
                else
                {
                    if (_User.Password == dbUser.Password)
                    {
                       
                            realm.Write(() =>
                            {
                                dbUser.Remember = User.Remember;
                                realm.Add(dbUser);
                            });
                          

                       // _User = dbUser;

                        _User  = new UserModel
                        {
                            Id = dbUser.Id,
                            FirstName = dbUser.FirstName,
                            LastName = dbUser.LastName,
                            Cellphone = dbUser.Cellphone,
                            Email = dbUser.Email,
                            User = dbUser.User,
                            Photo = dbUser.Photo,
                            Remember = dbUser.Remember,
                        };
                       

        NavigationPage navigation = new NavigationPage(new HomeView());

                        Application.Current.MainPage = new FlyoutPage
                        {
                            Flyout = new MenuView(),
                            Detail = navigation
                        };
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Wrong Credentials", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Login error:" + ex.Message, "OK");
            }
        }

        public async void Logout()
        {
            try
            {
                var realm = Realm.GetInstance();

                var dbUser = realm.All<UserModel>().Where(u => u.Remember == true).FirstOrDefault();

                if (dbUser != null)
                {
                    realm.Write(() =>
                    {
                        dbUser.Remember = false;
                        realm.Add(dbUser);
                    });

                }


                Clean();
                App.Current.MainPage = new LoginView();

            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert("Error", "Login error:" + ex.Message, "OK");
            }
        }

        public void GetUserRemember()
        {
            try
            {
                var realm = Realm.GetInstance();

                var dbUser = realm.All<UserModel>().Where(u => u.Remember == true).FirstOrDefault();

                if (dbUser == null)
                {
                    Clean();
                    App.Current.MainPage = new LoginView();
                }
                else
                {
                    _User = dbUser;

                    NavigationPage navigation = new NavigationPage(new HomeView());

                    Application.Current.MainPage = new FlyoutPage
                    {
                        Flyout = new MenuView(),
                        Detail = navigation
                    };
                }

            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert("Error", "Login error:" + ex.Message, "OK");
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
