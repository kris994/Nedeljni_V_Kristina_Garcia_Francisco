using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            if (value != null)
            {
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
