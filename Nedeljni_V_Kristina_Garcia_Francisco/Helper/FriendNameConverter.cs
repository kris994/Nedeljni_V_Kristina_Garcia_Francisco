using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nedeljni_V_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Gets the name and last name of the user for the given id
    /// </summary>
    class FriendNameConverter : IValueConverter
    {
        /// <summary>
        /// Convers the user id to his name and last name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserData userData = new UserData();
            List<tblRelationship> allRelationships = userData.GetAllFriends(LoggedInUser.CurrentUser).ToList();
            List<tblUser> allUsers = userData.GetAllUsers().ToList();
            int relationshipUserID = 0;

            // Get the friends name
            if (value != null)
            {
                for (int i = 0; i < allRelationships.Count; i++)
                {
                    if (allRelationships[i].RelationshipID == (int)value)
                    {
                        // Check the name of the person that is the current logged in users friend
                        if (allRelationships[i].User1ID != LoggedInUser.CurrentUser.UserID)
                        {
                            relationshipUserID = allRelationships[i].User1ID;
                        }
                        else
                        {
                            relationshipUserID = allRelationships[i].User2ID;
                        }
                    }
                }

                for (int i = 0; i < allUsers.Count; i++)
                {
                    if (allUsers[i].UserID == relationshipUserID)
                    {
                        return allUsers[i].FirstName + " " + allUsers[i].LastName;
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Returns the selected value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}