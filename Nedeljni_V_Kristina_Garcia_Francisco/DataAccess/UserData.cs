using Nedeljni_V_Kristina_Garcia_Francisco.Helper;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_V_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// class used to create the user CRUD structure of the application
    /// </summary>
    class UserData
    {
        /// <summary>
        /// Get all data about users from the database
        /// </summary>
        /// <returns>The list of all users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();                 
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all data about users but the current one from the database
        /// </summary>
        /// <returns>The list of all users</returns>
        public List<tblUser> GetAllUsersButPanding()
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    List<tblRelationship> allRelationShips = GetAllRelationshipsUsers(LoggedInUser.CurrentUser).ToList();

                    list = (from x in context.tblUsers select x).ToList();

                    // find the current user before removing them from the list
                    tblUser userToRemove = (from r in context.tblUsers where r.UserID == LoggedInUser.CurrentUser.UserID select r).First();
                    list.Remove(userToRemove);

                    // remove users that already have a relationship with the logged in user
                    for (int i = 0; i < allRelationShips.Count; i++)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (allRelationShips[i].User1ID == list[j].UserID ||
                               allRelationShips[i].User2ID == list[j].UserID)
                            {
                                list.Remove(list[j]);
                            }
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all relationships users
        /// </summary>
        /// <param name="user">Current User</param>
        /// <returns>The list of all relationship users</returns>
        public List<tblRelationship> GetAllRelationshipsUsers(tblUser user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblRelationship> list = new List<tblRelationship>();
                    list = context.tblRelationships.Where(x => x.User1ID == user.UserID || x.User2ID == user.UserID).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all friends
        /// </summary>
        /// <param name="user">Current User</param>
        /// <returns>The list of all friends</returns>
        public List<tblRelationship> GetAllFriends(tblUser user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblRelationship> list = new List<tblRelationship>();
                    list = context.tblRelationships.Where(x => (x.User1ID == user.UserID || x.User2ID == user.UserID) && x.RelationshipStatus == "Accepted").ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all waiting to accept users
        /// </summary>
        /// <param name="user">Current User</param>
        /// <returns>The list of all users</returns>
        public List<tblRelationship> GetAllWaitingToAcceptUsers(tblUser user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblRelationship> list = new List<tblRelationship>();
                    list = context.tblRelationships.Where(x => x.User1ID == user.UserID && x.RelationshipStatus == "Pending").ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get panding users
        /// </summary>
        /// <param name="user">Current User</param>
        /// <returns>The list of all users</returns>
        public List<tblRelationship> GetAllPandingUsers(tblUser user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblRelationship> list = new List<tblRelationship>();
                    list = context.tblRelationships.Where(x => x.User2ID == user.UserID && x.RelationshipStatus == "Pending").ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Search if user with that ID exists in the user table
        /// </summary>
        /// <param name="userID">Takes the user id that we want to search for</param>
        /// <returns>True if the user exists</returns>
        private bool IsUserID(int userID)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    int result = (from x in context.tblUsers where x.UserID == userID select x.UserID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Search if user with that ID exists in the user table
        /// </summary>
        /// <param name="userID">Takes the user id that we want to search for</param>
        /// <returns>The user</returns>
        public tblUser GetUser(int userID)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    tblUser result = (from x in context.tblUsers where x.UserID == userID select x).FirstOrDefault();

                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Adds or edits a user in the database
        /// </summary>
        /// <param name="user">The user ID we are adding or editing</param>
        /// <returns>The new or edited user</returns>
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    if (user.UserID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Gender = user.Gender,
                            DateOfBirth = user.DateOfBirth,
                            UserLocation = user.UserLocation,
                            Username = user.Username,
                            UserPassword = PasswordHasher.Hash(user.UserPassword)
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return user;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == user.UserID select ss).First();

                        userToEdit.FirstName = user.FirstName;
                        userToEdit.LastName = user.LastName;
                        userToEdit.Email = user.Email;
                        userToEdit.Gender = user.Gender;
                        userToEdit.DateOfBirth = user.DateOfBirth;
                        userToEdit.UserLocation = user.UserLocation;
                        userToEdit.Username = user.Username;

                        // Save only if password changed
                        if (userToEdit.UserPassword != user.UserPassword)
                        {
                            userToEdit.UserPassword = PasswordHasher.Hash(user.UserPassword);
                        }

                        userToEdit.UserID = user.UserID;
                        context.SaveChanges();

                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Create Friend Request
        /// </summary>
        /// <param name="user">The user ID we adding for friend request</param>
        /// <returns>new relationship</returns>
        public tblRelationship CreateFriendRequest(tblUser user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    tblRelationship newRelationShip = new tblRelationship
                    {
                        RelationshipStatus = "Pending",
                        User1ID = LoggedInUser.CurrentUser.UserID,
                        User2ID = user.UserID,
                    };

                    context.tblRelationships.Add(newRelationShip);
                    context.SaveChanges();

                    return newRelationShip;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Accept Friend Request
        /// </summary>
        /// <param name="user">The user we accepting for friend request</param>
        /// <returns>edited relationship</returns>
        public tblRelationship AcceptFriendRequest(tblRelationship user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    tblRelationship requestToEdit = (from ss in context.tblRelationships where ss.RelationshipID == user.RelationshipID select ss).First();
                    requestToEdit.RelationshipStatus = "Accepted";
                    user.RelationshipStatus = "Accepted";
                    context.SaveChanges();
  
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Deny Friend Request
        /// </summary>
        /// <param name="user">The friend request we are deleting</param>
        public void DenyFriendRequest(tblRelationship user)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    tblRelationship requestToDelete = (from ss in context.tblRelationships where ss.RelationshipID == user.RelationshipID select ss).First();
                    context.tblRelationships.Remove(requestToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if the user is a friend
        /// </summary>
        /// <param name="friendID">checking if its a friend</param>
        /// <param name="user">user we are checking for friends</param>
        /// <returns>true if its a friend</returns>
        public bool CheckIfFriend(int friendID, tblUser user)
        {
            List<tblRelationship> allFriends = GetAllFriends(user).ToList();
            for (int i = 0; i < allFriends.Count; i++)
            {
                if (allFriends[i].User1ID == friendID || allFriends[i].User2ID == friendID)
                {
                    return true;
                }
            }

            if (allFriends.Count == 0)
            {
                return false;
            }

            return false;
        }
    }
}
