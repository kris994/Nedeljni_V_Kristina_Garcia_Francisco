using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Item menu
    /// </summary>
    public class ItemMenu
    {
        #region Constuctor
        /// <summary>
        /// Item menu
        /// </summary>
        /// <param name="header">header</param>
        /// <param name="subItems">subItems</param>
        /// <param name="icon">icon</param>
        public ItemMenu(string header, List<Subitem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }

        /// <summary>
        /// Item menu
        /// </summary>
        /// <param name="header">header</param>
        /// <param name="screen">UserControl</param>
        /// <param name="icon">icon</param>
        public ItemMenu(string header, UserControl screen, PackIconKind icon)
        {
            Header = header;
            Screen = screen;
            Icon = icon;
        }
        #endregion

        /// <summary>
        /// Header
        /// </summary>
        public string Header { get; private set; }
        /// <summary>
        /// Icon
        /// </summary>
        public PackIconKind Icon { get; private set; }
        /// <summary>
        /// Subitems
        /// </summary>
        public List<Subitem> SubItems { get; private set; }
        /// <summary>
        /// Screen
        /// </summary>
        public UserControl Screen { get; private set; }
    }
}
