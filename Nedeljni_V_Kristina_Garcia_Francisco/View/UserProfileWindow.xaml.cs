using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : UserControl
    {
        /// <summary>
        /// User window
        /// </summary>
        /// <param name="user"></param>
        public UserProfileWindow()
        {
            InitializeComponent();
            this.DataContext = new ProfileViewModel(this, LoggedInUser.CurrentUser);
            this.Name = "UserProfileWindow";
        }

        /// <summary>
        /// Disable login button unil both field are not empty
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs event</param>
        private void txtChanged(object sender, RoutedEventArgs e)
        {

            if (oldPassword.Password.Length > 4 && newPassword.Password.Length > 4)
            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }
    }
}
