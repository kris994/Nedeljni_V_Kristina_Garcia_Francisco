using System.Windows;
using System.Windows.Controls;

namespace Nedeljni_V_Kristina_Garcia_Francisco.ViewModel
{
    public class Subitem
    {
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
        public string Name { get; private set; }
        public UserControl Screen { get; private set; }
        public Window WindowScreen { get; private set; }
    }
}
