using MaterialDesignThemes.Wpf;
using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            this.DataContext = new UserWindowViewModel(this);
            this.Language = XmlLanguage.GetLanguage("en-GB");

            lblName.Content = LoggedInUser.CurrentUser.FirstName + " " + LoggedInUser.CurrentUser.LastName;

            //var menuOrders = new List<Subitem>
            //        {
            //            //new Subitem("Napisi novi recept", new AddRecipe()),
            //            new Subitem("Svi Recepti", new AllRecipesWindow()),
            //        };
            //var item1 = new ItemMenu("Recepti", menuOrders, PackIconKind.);

            //var menuShopping = new List<Subitem>
            //        {
            //            new Subitem("Sve Shopping Liste", new AllShoppingList()),
            //        };
            //var item2 = new ItemMenu("Shopping Lista", menuShopping, PackIconKind.Pizzeria);


            //var menuIngredient = new List<Subitem>
            //        {
            //            new Subitem("Lista sastojaka", new AddIngredientMenu()),
            //        };
            //var item22 = new ItemMenu("Sastojci", menuIngredient, PackIconKind.Cookie);

            //var menuStorage = new List<Subitem>
            //        {
            //            new Subitem("Skladiste Sastojaka", new AllStorageList()),
            //        };
            //var item33 = new ItemMenu("Skladiste", menuStorage, PackIconKind.Cookie);

            //var item50 = new ItemMenu("Menu", new UserControl(), PackIconKind.Pizza);

            //Menu.Children.Add(new UserControlMenuItem(item50, this));
            //Menu.Children.Add(new UserControlMenuItem(item1, this));
            //Menu.Children.Add(new UserControlMenuItem(item2, this));
            //Menu.Children.Add(new UserControlMenuItem(item22, this));
            //Menu.Children.Add(new UserControlMenuItem(item33, this));


            //determines the current page length
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            //Current time
            Time();

        }
        /// <summary>
        /// Method to change the menu
        /// </summary>
        /// <param name="sender">Selected UserControl</param>
        public void SwitchScreen(object sender)
        {


            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);

                //ArchivedOrder ao = StackPanelMain.FindName("ArchivedOrder") as ArchivedOrder;
                //ArchivedOrder ar = new ArchivedOrder();

                //if (screen.Name == "AllStorageList")
                //{
                //    AllStorageList storageList = new AllStorageList();
                //    StackPanelMain.Children.Clear();
                //    StackPanelMain.Children.Add(storageList);
                //}
                //else
                //{
                //    StackPanelMain.Children.Clear();
                //    StackPanelMain.Children.Add(screen);
                //}

            }
        }


        private void DragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

                // throw;
            }
        }

        private void Time()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Event;
            timer.Start();
        }

        /// <summary>
        /// method for printing the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event(object sender, EventArgs e)
        {
            vr.Text = DateTime.Now.ToString(@"HH:mm:ss");
        }

        /// <summary>
        /// method for the application user to log out, 
        /// after which a new login form is displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();

            login.ShowDialog();
        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dijalog = Xceed.Wpf.Toolkit.MessageBox.Show("Do you want to leave the program", "Exit app", MessageBoxButton.YesNo);

            if (dijalog == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Window enlargement method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extend_Window(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                ExtendWindow.ToolTip = "Restore Down";
                ExtendWindow1.Visibility = Visibility.Visible;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                ExtendWindow.ToolTip = "Maximize";
                ExtendWindow1.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Window reduction method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lower_Window(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }
    }
}

