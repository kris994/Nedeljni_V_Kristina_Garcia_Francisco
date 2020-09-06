using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Helper;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    class ProfileViewModel : ViewModelBase
    {
        readonly ProfileWindow profileWindow;
        readonly UserProfileWindow userProfileWindow;
        PostData postData = new PostData();
        UserData userData = new UserData();

        #region Constuctor
        /// <summary>
        /// Any user Profile window
        /// </summary>
        /// <param name="profileWindowOpen">opens the window</param>
        /// <param name="userProfile">user</param>
        public ProfileViewModel(ProfileWindow profileWindowOpen, tblUser userProfile)
        {
            user = userProfile;
            profileWindow = profileWindowOpen;
            PostList = postData.GetAllUserPosts(userProfile);
            ViewPost = Visibility.Collapsed;
        }

        /// <summary>
        /// Loggedin user Profile window
        /// </summary>
        /// <param name="userProfileWindowOpen">opens the window</param>
        /// <param name="userProfile">user</param>
        public ProfileViewModel(UserProfileWindow userProfileWindowOpen, tblUser userProfile)
        {
            user = LoggedInUser.CurrentUser;
            user = userProfile;
            userProfileWindow = userProfileWindowOpen;
            ViewChangePassword = Visibility.Collapsed;
        }
        #endregion

        #region Property
        /// <summary>
        /// Specific user
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
        /// Specific post
        /// </summary>
        private tblPost post;
        public tblPost Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        /// <summary>
        /// List of posts
        /// </summary>
        private List<tblPost> postList;
        public List<tblPost> PostList
        {
            get
            {
                return postList;
            }
            set
            {
                postList = value;
                OnPropertyChanged("PostList");
            }
        }

        /// <summary>
        /// Visibility Post
        /// </summary>
        private Visibility viewPost;
        public Visibility ViewPost
        {
            get
            {
                return viewPost;
            }
            set
            {
                viewPost = value;
                OnPropertyChanged("ViewPost");
            }
        }

        /// <summary>
        /// Visibility Password Change
        /// </summary>
        private Visibility viewChangePassword;
        public Visibility ViewChangePassword
        {
            get
            {
                return viewChangePassword;
            }
            set
            {
                viewChangePassword = value;
                OnPropertyChanged("ViewChangePassword");
            }
        }

        /// <summary>
        /// Info label
        /// </summary>
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }

        /// <summary>
        /// Info label background
        /// </summary>
        private string infoLabelBG;
        public string InfoLabelBG
        {
            get
            {
                return infoLabelBG;
            }
            set
            {
                infoLabelBG = value;
                OnPropertyChanged("InfoLabelBG");
            }
        }
        #endregion

        #region SnackBarInfo
        /// <summary>
        /// Snack bar user info showing
        /// </summary>
        public async void SnackUserInfo()
        {
            userProfileWindow.InfoMessage.IsActive = true;
            await Task.Delay(3000);
            userProfileWindow.InfoMessage.IsActive = false;
        }
        #endregion

        #region Commands
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
            MessageBoxResult dialog = Xceed.Wpf.Toolkit.MessageBox.Show("Do you want to leave the profile?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dialog == MessageBoxResult.Yes)
            {
                ViewPost = Visibility.Collapsed;
                profileWindow.border.Width = 400;
                profileWindow.Close();
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

        /// <summary>
        /// PostPreview Button
        /// </summary>
        private ICommand postPreview;
        public ICommand PostPreview
        {
            get
            {
                if (postPreview == null)
                {
                    postPreview = new RelayCommand(param => PostPreviewExecute(), param => CanPostPreviewExecute());
                }
                return postPreview;
            }
        }

        /// <summary>
        /// Executes postPreview command
        /// </summary>
        private void PostPreviewExecute()
        {
            ViewPost = Visibility.Visible;
            profileWindow.border.Width = 1000;
        }

        /// <summary>
        /// Checks if its possible to press the button
        /// </summary>
        /// <returns></returns>
        private bool CanPostPreviewExecute()
        {
            return true;
        }

        /// <summary>
        /// ChangePassword button
        /// </summary>
        private ICommand changePassword;
        public ICommand ChangePassword
        {
            get
            {
                if (changePassword == null)
                {
                    changePassword = new RelayCommand(param => ChangePasswordExecute(), param => CanChangePasswordExecute());
                }
                return changePassword;
            }
        }

        /// <summary>
        /// ChangePassword preview
        /// </summary>
        private void ChangePasswordExecute()
        {
            ViewChangePassword = Visibility.Visible;
        }

        /// <summary>
        /// Checks if its possible to press the button
        /// </summary>
        /// <returns></returns>
        private bool CanChangePasswordExecute()
        {
            return true;
        }

        /// <summary>
        /// Cancel button
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Cancel
        /// </summary>
        private void CancelExecute()
        {
            MessageBoxResult dialog = Xceed.Wpf.Toolkit.MessageBox.Show("Are you sure you wan to cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dialog == MessageBoxResult.Yes)
            {
                ViewChangePassword = Visibility.Collapsed;
                userProfileWindow.newPassword.Clear();
                userProfileWindow.oldPassword.Clear();
            }
        }

        /// <summary>
        /// Checks if its possible to press the button
        /// </summary>
        /// <returns></returns>
        private bool CanCancelExecute()
        {
            return true;
        }

        /// <summary>
        /// Save button
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute, param => CanSaveExecute());
                }
                return save;
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        private void SaveExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            LoggedInUser.CurrentUser.UserPassword = password;
            userData.AddUser(LoggedInUser.CurrentUser);

            // Hash current user password
            LoggedInUser.CurrentUser.UserPassword = PasswordHasher.Hash(password);

            ViewChangePassword = Visibility.Collapsed;
            userProfileWindow.newPassword.Clear();
            userProfileWindow.oldPassword.Clear();
            userProfileWindow.error.Visibility = Visibility.Visible;

            InfoLabel = $"Successfuly updated the password.";
            InfoLabelBG = "#FF8BC34A";
            SnackUserInfo();
        }

        /// <summary>
        /// Checks if its possible to press the button
        /// </summary>
        /// <returns></returns>
        private bool CanSaveExecute()
        {
            if (PasswordHasher.Verify(userProfileWindow.oldPassword.Password.ToString(), LoggedInUser.CurrentUser.UserPassword) == true)
            {
                userProfileWindow.error.Visibility = Visibility.Collapsed;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
