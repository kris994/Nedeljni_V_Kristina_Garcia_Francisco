using Nedeljni_V_Kristina_Garcia_Francisco.DataAccess;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nedeljni_V_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Validates the user inputs
    /// </summary>
    class Validations
    {
        /// <summary>
        /// The input cannot be empty
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Cannot be empty";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the Username is exists
        /// </summary>
        /// <param name="username">the username we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string UsernameChecker(string username, int id)
        {
            UserData userData = new UserData();

            List<tblUser> AllUsers = userData.GetAllUsers();
            string currectUsername = "";

            if (username == null)
            {
                return "Username cannot be empty.";
            }

            // Get the current users username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currectUsername = AllUsers[i].Username;
                    break;
                }
            }

            // Check if the username already exists, but it is not the current user username
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if ((AllUsers[i].Username == username && currectUsername != username))
                {
                    return "This Username already exists!";
                }
            }

            return null;
        }      

        /// <summary>
        /// Date cannot be in the future
        /// </summary>
        /// <param name="value">value of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CheckDate(DateTime value)
        {
            if (value == default(DateTime) || value > DateTime.Now)
            {
                return "Value cannot be later than today";
            }

            return null;
        }

        /// <summary>
        /// Validates the given string if its an email or not
        /// </summary>
        /// <param name="email">string that is validated</param>
        /// <param name="id">for the specific user</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string IsValidEmailAddress(string email, int id)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            UserData userdata = new UserData();

            List<tblUser> allUsers = userdata.GetAllUsers();
            string currentEmail = "";

            if (email == null)
            {
                return "Email cannot be empty.";
            }

            // Get the current users email
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].UserID == id)
                {
                    currentEmail = allUsers[i].Email;
                    break;
                }
            }

            // Check if the email already exists, but it is not the current user email
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Email == email && currentEmail != email)
                {
                    return "This Email already exists!";
                }
            }

            if (regex.IsMatch(email) == false)
            {
                return "Invalid email";
            }

            return null;
        }
    }
}
