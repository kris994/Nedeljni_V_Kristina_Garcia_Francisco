using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// All users preview
    /// </summary>
    class AllUsersViewModel : ViewModelBase
    {
        /// <summary>
        /// User data
        /// </summary>
        UserData userData = new UserData();
        /// <summary>
        /// All users window
        /// </summary>
        readonly AllUsersWindow allUsersWindow;
        /// <summary>
        /// All Panding users Window
        /// </summary>
        readonly AllPendingUsersWindow allPandingWindow;

        #region Constructor
        /// <summary>
        /// All Users Window
        /// </summary>
        /// <param name="allUsersWindowOpen">open the window</param>
        public AllUsersViewModel(AllUsersWindow allUsersWindowOpen)
        {
            allUsersWindow = allUsersWindowOpen;
            UserList = userData.GetAllUsersButPanding().ToList();
            RelationshipList = userData.GetAllRelationshipsUsers(LoggedInUser.CurrentUser).ToList();
        }

        /// <summary>
        /// All Panding Window
        /// </summary>
        /// <param name="allPandingWindowOpen">open the window</param>
        public AllUsersViewModel(AllPendingUsersWindow allPandingWindowOpen)
        {
            allPandingWindow = allPandingWindowOpen;
            PendingList = userData.GetAllPandingUsers(LoggedInUser.CurrentUser).ToList();
            WaitingAcceptList = userData.GetAllWaitingToAcceptUsers(LoggedInUser.CurrentUser).ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of paning users
        /// </summary>
        private List<tblRelationship> relationshipList;
        public List<tblRelationship> RelationshipList
        {
            get
            {
                return relationshipList;
            }
            set
            {
                relationshipList = value;
                OnPropertyChanged("RelationshipList");
            }
        }

        /// <summary>
        /// List of paning users
        /// </summary>
        private List<tblRelationship> pendingList;
        public List<tblRelationship> PendingList
        {
            get
            {
                return pendingList;
            }
            set
            {
                pendingList = value;
                OnPropertyChanged("PendingList");
            }
        }

        /// <summary>
        /// List of waiting to accept users
        /// </summary>
        private List<tblRelationship> waitingAcceptList;
        public List<tblRelationship> WaitingAcceptList
        {
            get
            {
                return waitingAcceptList;
            }
            set
            {
                waitingAcceptList = value;
                OnPropertyChanged("WaitingAcceptList");
            }
        }

        /// <summary>
        /// List of users
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
        /// Panding user
        /// </summary>
        private tblRelationship pandingUser;
        public tblRelationship PandingUser
        {
            get
            {
                return pandingUser;
            }
            set
            {
                pandingUser = value;
                OnPropertyChanged("PandingUser");
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

        #region Commands
        /// <summary>
        /// FriendRequest Button
        /// </summary>
        private ICommand friendRequest;
        public ICommand FriendRequest
        {
            get
            {
                if (friendRequest == null)
                {
                    friendRequest = new RelayCommand(param => FriendRequestExecute(), param => CanFriendRequestExecute());
                }
                return friendRequest;
            }
        }

        /// <summary>
        /// Executes the Frient Request
        /// </summary>
        private void FriendRequestExecute()
        {
            try
            {
                userData.CreateFriendRequest(User);
                WaitingAcceptList = userData.GetAllWaitingToAcceptUsers(LoggedInUser.CurrentUser).ToList();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Cannot issue friend reequest right now, please try again later." + ex);
            }
        }

        /// <summary>
        /// Checks if its possible to execute the FriendRequest command
        /// </summary>
        /// <returns>true</returns>
        private bool CanFriendRequestExecute()
        {
            return true;
        }
        #endregion
    }
}
