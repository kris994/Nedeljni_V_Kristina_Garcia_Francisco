﻿using Nedeljni_V_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_V_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        UserWindow _context;
        private bool mouseClicked;

        /// <summary>
        /// User Control Menu
        /// </summary>
        /// <param name="itemMenu"></param>
        /// <param name="context"></param>
        public UserControlMenuItem(ItemMenu itemMenu, UserWindow context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        /// <summary>
        /// The method that determines which menu is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mouseClicked)
            {
                if (e.AddedItems.Count > 0)
                    _context.SwitchScreen(((Subitem)((ListView)sender).SelectedItem).Screen);
            }
        }

        /// <summary>
        /// The method that determines which menu is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMenu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = true;
            if (ListViewMenu.SelectedItem != null)
            {
                _context.SwitchScreen(((Subitem)((ListView)sender).SelectedItem).Screen);
            }
        }
    }
}
