using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AllFriendWindow.xaml
    /// </summary>
    public partial class AllFriendWindow : UserControl
    {
        public AllFriendWindow()
        {
            InitializeComponent();
            this.DataContext = new AllUsersViewModel(this);
            this.Name = "AllFriendWindow";
        }
    }
}
