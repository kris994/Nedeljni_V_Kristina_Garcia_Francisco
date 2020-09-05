using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    class UserWindowViewModel : ViewModelBase
    {
        readonly UserWindow userWindow;
        PostData postData = new PostData(); 

        #region Constructor
        /// <summary>
        /// User Window
        /// </summary>
        /// <param name="userWindowOpen">Opens user window</param>
        public UserWindowViewModel(UserWindow userWindowOpen)
        {
            this.userWindow = userWindowOpen;
            PostList = postData.GetAllPosts();
        }
        #endregion

        #region Property
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
        public async void SnackInfo()
        {
            userWindow.InfoMessage.IsActive = true;
            await Task.Delay(3000);
            userWindow.InfoMessage.IsActive = false;
        }
        #endregion

        #region Commands
        /// <summary>
        /// AddPost Button
        /// </summary>
        private ICommand addPost;
        public ICommand AddPost
        {
            get
            {
                if (addPost == null)
                {
                    addPost = new RelayCommand(param => AddPostExecute(), param => CanAddPostExecute());
                }
                return addPost;
            }
        }

        /// <summary>
        /// Executes the add Post command
        /// </summary>
        private void AddPostExecute()
        {
            try
            {
                AddPostWindow addPostWindow = new AddPostWindow();
                addPostWindow.ShowDialog();

                // Notification
                if ((addPostWindow.DataContext as AddPostViewModel).IsUpdatePost == true)
                {
                    InfoLabel = $"Created a new post";
                    InfoLabelBG = "#FF8BC34A";
                    SnackInfo();
                }

                PostList = postData.GetAllPosts();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Cannot create a post right now." + ex);
            }
        }

        /// <summary>
        /// Checks if its possible to execute the add Post command
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddPostExecute()
        {
            return true;
        }
        #endregion
    }
}
