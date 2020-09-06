using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;

namespace Nedeljni_V_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Checks if the user already liked the post
    /// </summary>
    class LikeButtonContentConverter : IValueConverter
    {
        /// <summary>
        /// Checks if the user already liked the post
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
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

                if (postData.IsPostLiked(post, LoggedInUser.CurrentUser) == true)
                {
                    return "Liked";
                }
                else
                {
                    return "Like Post";
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
