﻿using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System.Windows;
using System.Collections.Generic;

namespace Nedeljni_V_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Returns true if the user can see total number of likes
    /// </summary>
    class TotalNumberVisibleConverter : IValueConverter
    {
        /// <summary>
        /// Checks if the user can see the total likes field
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserData userData = new UserData();
            PostData postdata = new PostData();
            List<tblPost> allPosts = postdata.GetAllPosts();

            if (value != null)
            {
                // Return visible if the post belongs to the current user
                for (int i = 0; i < allPosts.Count; i++)
                {
                    if (allPosts[i].UserID == (int)value && allPosts[i].UserID == LoggedInUser.CurrentUser.UserID)
                    {
                        return Visibility.Visible;
                    }
                }
                if (userData.CheckIfFriend((int)value, LoggedInUser.CurrentUser) == true)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
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
