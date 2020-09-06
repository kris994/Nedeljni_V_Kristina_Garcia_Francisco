using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AllPendingUsersWindow.xaml
    /// </summary>
    public partial class AllPendingUsersWindow : UserControl
    {
        /// <summary>
        /// Add Pending users Window
        /// </summary>
        public AllPendingUsersWindow()
        {
            InitializeComponent();
            this.DataContext = new AllUsersViewModel(this);
            this.Name = "AllPendingUsersWindow";
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
