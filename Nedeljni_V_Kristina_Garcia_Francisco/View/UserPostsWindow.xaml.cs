using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for UserPostsWindow.xaml
    /// </summary>
    public partial class UserPostsWindow : UserControl
    {
        public UserPostsWindow()
        {
            InitializeComponent();
            this.DataContext = new UserWindowViewModel(this, LoggedInUser.CurrentUser);
            this.Name = "UserPostsWindow";
        }
    }
}
