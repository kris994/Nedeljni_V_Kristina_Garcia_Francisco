using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Helper;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Login
    /// </summary>
    class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Login view
        /// </summary>
        readonly LoginWindow view;
        /// <summary>
        /// User data
        /// </summary>
        UserData userData = new UserData();

        #region Constructor
        /// <summary>
        /// Login view model constructor
        /// </summary>
        /// <param name="loginView">The window that is being opened</param>
        public LoginViewModel(LoginWindow loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = userData.GetAllUsers().ToList();
            ViewRegistration = Visibility.Collapsed;
        }
        #endregion

        #region Property
        /// <summary>
        /// Info Text
        /// </summary>
        private string infoText;
        public string InfoText
        {
            get
            {
                return infoText;
            }
            set
            {
                infoText = value;
                OnPropertyChanged("InfoText");
            }
        }

        /// <summary>
        /// Info Color
        /// </summary>
        private string infoColor;
        public string InfoColor
        {
            get
            {
                return infoColor;
            }
            set
            {
                infoColor = value;
                OnPropertyChanged("InfoColor");
            }
        }

        /// <summary>
        /// Username
        /// </summary>
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        /// <summary>
        /// Used for saving the current user
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// List of all users in the application
        /// </summary>
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        /// <summary>
        /// Visibility Registration
        /// </summary>
        private Visibility viewRegistration;
        public Visibility ViewRegistration
        {
            get
            {
                return viewRegistration;
            }
            set
            {
                viewRegistration = value;
                OnPropertyChanged("ViewRegistration");
            }
        }
        #endregion

        #region SnackBarInfo
        /// <summary>
        /// Snack bar info showing
        /// </summary>
        public async void SnackInfo()
        {
            view.InfoMessage.IsActive = true;
            await Task.Delay(3000);
            view.InfoMessage.IsActive = false;
        }
        #endregion

        #region Commands
        /// <summary>
        /// Add new user Button
        /// </summary>
        private ICommand addNewUser;
        public ICommand AddNewUser
        {
            get
            {
                if (addNewUser == null)
                {
                    addNewUser = new RelayCommand(AddNewUserExecute, param => this.CanAddNewUserExecute);
                }
                return addNewUser;
            }
        }

        /// <summary>
        /// Executes the add User command
        /// </summary>
        private void AddNewUserExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            User.UserPassword = password;
            userData.AddUser(User);
            UserList = userData.GetAllUsers().ToList();

            // Login
            ViewRegistration = Visibility.Collapsed;
            UserWindow userWindoe = new UserWindow();
            view.Close();
            userWindoe.Show();
        }

        /// <summary>
        /// Checks if its possible to add the new user
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewUserExecute
        {
            get
            {
                return User.IsValid;
            }
        }

        /// <summary>
        /// Registration Button
        /// </summary>
        private ICommand registerUser;
        public ICommand RegisterUser
        {
            get
            {
                if (registerUser == null)
                {
                    registerUser = new RelayCommand(param => RegisterUserExecute(), param => CanRegisterUserExecute());
                }
                return registerUser;
            }
        }

        /// <summary>
        /// Executes the register User command
        /// </summary>
        private void RegisterUserExecute()
        {
            ViewRegistration = Visibility.Visible;
        }

        /// <summary>
        /// Checks if its possible to register
        /// </summary>
        /// <returns>true</returns>
        private bool CanRegisterUserExecute()
        {
            return true;
        }

        /// <summary>
        /// Command used to log te user into the application
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute, CanLoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Checks if its possible to login depending on the given username and password and saves the logged in user to a list
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            bool found = false;

            if (UserList.Any())
            {
                for (int i = 0; i < UserList.Count; i++)
                {
                    if (username == UserList[i].Username && PasswordHasher.Verify(password, UserList[i].UserPassword) == true)
                    {
                        LoggedInUser.CurrentUser = new tblUser
                        {
                            UserID = UserList[i].UserID,
                            FirstName = UserList[i].FirstName,
                            LastName = UserList[i].LastName,
                            Email = UserList[i].Email,
                            DateOfBirth = UserList[i].DateOfBirth,
                            UserLocation = UserList[i].UserLocation,
                            Username = UserList[i].Username,
                            UserPassword = UserList[i].UserPassword
                        };
                        found = true;

                        UserWindow userWindoe = new UserWindow();
                        view.Close();
                        userWindoe.Show();

                        break;
                    }
                }
            }

            if (found == false)
            {
                InfoText = "Wrong Username or Password";
                InfoColor = "#FFF34A4A";
                SnackInfo();
            }
        }

        /// <summary>
        /// Checks if its possible login
        /// </summary>
        /// <returns>true</returns>
        private bool CanLoginExecute(object obj)
        {
            if (username != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Exit button
        /// </summary>
        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        /// <summary>
        /// Exits the current window
        /// </summary>
        private void ExitExecute()
        {
            MessageBoxResult dialog = Xceed.Wpf.Toolkit.MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dialog == MessageBoxResult.Yes)
            {
                view.Close();
            }
        }

        /// <summary>
        /// Checks if its possible to press the button
        /// </summary>
        /// <returns></returns>
        private bool CanExitExecute()
        {
            return true;
        }
        #endregion
    }
}
