using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        /// <summary>
        /// Profile Window
        /// </summary>
        /// <param name="user"></param>
        public ProfileWindow(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new ProfileViewModel(this, user);
        }
    }
}
