using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AllUsersWindow.xaml
    /// </summary>
    public partial class AllUsersWindow : UserControl
    {
        public AllUsersWindow()
        {
            InitializeComponent();
            this.DataContext = new AllUsersViewModel(this);
        }
    }
}
