using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddPostWindow.xaml
    /// </summary>
    public partial class AddPostWindow : Window
    {
        public AddPostWindow()
        {
            InitializeComponent();
            this.DataContext = new AddPostViewModel(this);
        }
    }
}
