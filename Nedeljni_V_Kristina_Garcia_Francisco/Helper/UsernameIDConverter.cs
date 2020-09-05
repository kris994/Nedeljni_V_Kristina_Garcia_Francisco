using System;
using System.Windows.Data;
using System.Globalization;
using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using System.Linq;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;

namespace Nedeljni_V_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    ///  Gets the username of the user for the given id
    /// </summary>
    class UsernameIDConverter : IValueConverter
    {
        /// <summary>
        /// Convers the user id to username
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserData userData = new UserData();
            List<tblUser> allUsers = userData.GetAllUsers().ToList();

            if (value != null)
            {
                for (int i = 0; i < allUsers.Count; i++)
                {
                    if (allUsers[i].UserID == (int)value)
                    {
                        return allUsers[i].Username;
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
