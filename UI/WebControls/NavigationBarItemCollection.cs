using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class NavigationBarItemCollection : List<NavigationBarItem>
    {
        public NavigationBarItem this[string commandName]
        {
            get
            {
                foreach (NavigationBarItem navigationBarItem in this)
                {
                    if (navigationBarItem.CommandName == commandName)
                        return navigationBarItem;
                }

                return null;
            }
        }

        public int GetItemIndex(NavigationBarItem navigationBarItemCurrent)
        {
            int itemIndex = -1;

            foreach (NavigationBarItem navigationBarItem in this)
            {
                itemIndex++;

                if (navigationBarItem.CommandName == navigationBarItemCurrent.CommandName)
                    return itemIndex;
            }

            return itemIndex;
        }
    }
}
