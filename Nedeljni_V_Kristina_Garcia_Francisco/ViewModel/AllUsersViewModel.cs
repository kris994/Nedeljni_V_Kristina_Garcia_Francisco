using Nedeljni_V_Kristina_Garcia_Francisco.Commands;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// <summary>
        /// All Friends users Window
        /// </summary>
        readonly AllFriendWindow allFriendWindow;

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

        /// <summary>
        /// All Friends Window
        /// </summary>
        /// <param name="allPandingWindowOpen">open the window</param>
        public AllUsersViewModel(AllFriendWindow allFriendWindowOpen)
        {
            allFriendWindow = allFriendWindowOpen;
            FriendList = userData.GetAllFriends(LoggedInUser.CurrentUser).ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of friends
        /// </summary>
        private List<tblRelationship> friendList;
        public List<tblRelationship> FriendList
        {
            get
            {
                return friendList;
            }
            set
            {
                friendList = value;
                OnPropertyChanged("FriendList");
            }
        }

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

        #region SnackBarInfo
        /// <summary>
        /// Snack bar user info showing
        /// </summary>
        public async void SnackUserInfo()
        {
            allUsersWindow.InfoMessage.IsActive = true;
            await Task.Delay(3000);
            allUsersWindow.InfoMessage.IsActive = false;
        }

        /// <summary>
        /// Snack bar pending info showing
        /// </summary>
        public async void SnackPendingInfo()
        {
            allPandingWindow.InfoMessage.IsActive = true;
            await Task.Delay(3000);
            allPandingWindow.InfoMessage.IsActive = false;
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
                string firstname = User.FirstName;
                string lastname = User.LastName;
                userData.CreateFriendRequest(User);
                
                WaitingAcceptList = userData.GetAllWaitingToAcceptUsers(LoggedInUser.CurrentUser).ToList();
                UserList = userData.GetAllUsersButPanding().ToList();

                InfoLabel = $"Friend request sent to {firstname} {lastname}.";
                InfoLabelBG = "#FF8BC34A";
                SnackUserInfo();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Cannot issue friend request right now, please try again later." + ex);
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

        /// <summary>
        /// Accept FriendRequest Button
        /// </summary>
        private ICommand accept;
        public ICommand Accept
        {
            get
            {
                if (accept == null)
                {
                    accept = new RelayCommand(param => AcceptExecute(), param => CanAcceptExecute());
                }
                return accept;
            }
        }

        /// <summary>
        /// Executes the Accept Frient Request
        /// </summary>
        private void AcceptExecute()
        {
            try
            {
                tblUser clickedUser = new tblUser()
                {
                    FirstName = userData.GetUser(PandingUser.User1ID).FirstName,
                    LastName = userData.GetUser(PandingUser.User1ID).LastName
                };

                userData.AcceptFriendRequest(PandingUser);
                FriendList = userData.GetAllFriends(LoggedInUser.CurrentUser).ToList();
                PendingList = userData.GetAllPandingUsers(LoggedInUser.CurrentUser).ToList();

                InfoLabel = $"Accepted {clickedUser.FirstName} {clickedUser.LastName} user friend request.";
                InfoLabelBG = "#FF8BC34A";
                SnackPendingInfo();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Cannot accept friend request right now." + ex);
            }
        }

        /// <summary>
        /// Checks if its possible to execute the FriendRequest command
        /// </summary>
        /// <returns>true</returns>
        private bool CanAcceptExecute()
        {
            if (PandingUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deny FriendRequest Button
        /// </summary>
        private ICommand deny;
        public ICommand Deny
        {
            get
            {
                if (deny == null)
                {
                    deny = new RelayCommand(param => DenyExecute(), param => CanDenyExecute());
                }
                return deny;
            }
        }

        /// <summary>
        /// Executes the deny Frient Request
        /// </summary>
        private void DenyExecute()
        {
            try
            {
                tblUser clickedUser = new tblUser()
                {
                    FirstName = userData.GetUser(PandingUser.User1ID).FirstName,
                    LastName = userData.GetUser(PandingUser.User1ID).LastName
                };

                userData.DenyFriendRequest(PandingUser);
                PendingList = userData.GetAllPandingUsers(LoggedInUser.CurrentUser).ToList();
                UserList = userData.GetAllUsersButPanding().ToList();

                InfoLabel = $"Denied {clickedUser.FirstName} {clickedUser.LastName} user friend request.";
                InfoLabelBG = "#FFF34A4A";
                SnackPendingInfo();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Cannot deny friend request right now." + ex);
            }
        }

        /// <summary>
        /// Checks if its possible to execute the FriendRequest command
        /// </summary>
        /// <returns>true</returns>
        private bool CanDenyExecute()
        {
            if (PandingUser != null)
            {
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
