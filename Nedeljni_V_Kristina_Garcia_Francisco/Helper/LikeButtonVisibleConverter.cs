using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System.Windows;
using System.Collections.Generic;

namespace Nedeljni_V_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Checks if the user can see the button
    /// </summary>
    class LikeButtonVisibleConverter : IValueConverter
    {
        /// <summary>
        /// Checks if the user can see the button
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserData userData = new UserData();
            PostData postData = new PostData();
            List<tblPost> allPosts = postData.GetAllPosts();
            tblPost post = null;

            if (value != null)
            {
                for (int i = 0; i < allPosts.Count; i++)
                {
                    if (allPosts[i].PostID == (int)value)
                    {
                        post = allPosts[i];
                    }
                }

                // Return visible if the post belongs to the user
                if (post.UserID == LoggedInUser.CurrentUser.UserID)
                {
                    return Visibility.Collapsed;
                }

                if (userData.CheckIfFriend(post.UserID, LoggedInUser.CurrentUser) == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
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
