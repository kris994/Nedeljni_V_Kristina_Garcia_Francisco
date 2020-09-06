using System.Windows;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Subitem
    /// </summary>
    public class Subitem
    {
        #region Constructor
        /// <summary>
        /// Menu item
        /// </summary>
        /// <param name="name">Name menu item</param>
        /// <param name="screen">UserControl Menu Item</param>
        public Subitem(string name, UserControl screen = null)
        {
            Name = name;
            Screen = screen;
        }
        #endregion

        /// <summary>
        /// Name menu item
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// User Control
        /// </summary>
        public UserControl Screen { get; private set; }
        /// <summary>
        /// Window Screen
        /// </summary>
        public Window WindowScreen { get; private set; }
    }
}
