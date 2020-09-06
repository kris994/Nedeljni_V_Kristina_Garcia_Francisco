using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
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
    }
}
