using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

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

        /// <summary>
        /// Select Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectCurrentItem(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }
    }
}
