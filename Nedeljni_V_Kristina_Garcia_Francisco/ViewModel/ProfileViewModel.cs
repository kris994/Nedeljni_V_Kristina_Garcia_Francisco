using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    class ProfileViewModel : ViewModelBase
    {
        readonly ProfileWindow profileWindow;
        PostData postData = new PostData();

        #region Constuctor
        public ProfileViewModel(ProfileWindow profileWindowOpen, tblUser userProfile)
        {
            user = userProfile;
            profileWindow = profileWindowOpen;
            PostList = postData.GetAllUserPosts(userProfile);
            ViewPost = Visibility.Collapsed;
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
        #endregion
    }
}
