using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Allowed Text
        /// </summary>
        /// <param name="s">The text</param>
        /// <returns></returns>
        private Boolean TextAllowed(String s)
        {
            foreach (Char c in s.ToCharArray())
            {
                if (Char.IsLetter(c) || Char.IsControl(c))
                {

                    continue;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Numbers cannot be pasted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            String s = (String)e.DataObject.GetData(typeof(String));
            if (!TextAllowed(s)) e.CancelCommand();
        }

        /// <summary>
        /// Disable login button unil both field are not empty
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs event</param>
        private void txtChanged(object sender, RoutedEventArgs e)
        {

            if (passwordBox.Password.Length > 0 && txtUsername.Text.Length > 0)
            {
                btnLogin.IsEnabled = true;
            }
            else
            {
                btnLogin.IsEnabled = false;
            }
        }

        /// <summary>
        /// Disable login button unil both field are not empty
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs event</param>
        private void txtRegisterChanged(object sender, RoutedEventArgs e)
        {

            if (txtRegistrationPassword.Password.Length >= 5 
                && txtNewUsername.Text.Length > 0
                && txtFirstName.Text.Length > 0
                && txtLastName.Text.Length > 0
                && txtEmail.Text.Length > 0)
            {
                btnRegistration.IsEnabled = true;
            }
            else
            {
                btnRegistration.IsEnabled = false;
            }
        }
    }
}
