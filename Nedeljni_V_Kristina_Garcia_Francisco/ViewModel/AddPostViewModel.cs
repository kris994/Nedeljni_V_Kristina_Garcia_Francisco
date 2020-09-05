using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Add Posts Class
    /// </summary>
    class AddPostViewModel : ViewModelBase
    {
        readonly AddPostWindow addPostWindow;
        PostData postData = new PostData();

        #region Constructor
        /// <summary>
        /// Add Post window
        /// </summary>
        /// <param name="addPostWindowOpen">Opens the window</param>
        public AddPostViewModel(AddPostWindow addPostWindowOpen)
        {
            post = new tblPost();
            addPostWindow = addPostWindowOpen;
        }
        #endregion

        #region Property
        /// <summary>
        /// Information about the specific post
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
        /// Checks if the post was updated
        /// </summary>
        private bool isUpdatePost;
        public bool IsUpdatePost
        {
            get
            {
                return isUpdatePost;
            }
            set
            {
                isUpdatePost = value;
            }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Create Post Button
        /// </summary>
        private ICommand createPost;
        public ICommand CreatePost
        {
            get
            {
                if (createPost == null)
                {
                    createPost = new RelayCommand(param => CreatePostExecute(), param => CanCreatePostExecute());
                }
                return createPost;
            }
        }

        /// <summary>
        /// Executes the Create Post command
        /// </summary>
        private void CreatePostExecute()
        {
            try
            {
                postData.AddPost(Post);
                IsUpdatePost = true;
                addPostWindow.Close();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("There has been a problem creating the post, please try again later." + ex);
            }
        }

        /// <summary>
        /// Can only execute if the field are filled up
        /// </summary>
        /// <returns>true if possible</returns>
        private bool CanCreatePostExecute()
        {
            if (Post.PostText != null && Post.PostText.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Cancel post creation button
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
        /// Executes the cancel command
        /// </summary>
        private void CancelExecute()
        {
            var result = Xceed.Wpf.Toolkit.MessageBox.Show("Are you sure you want to cancel?\nAll Data will be lost.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    addPostWindow.Close();
                }
                catch (Exception ex)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if its possible to cancel
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
