using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
