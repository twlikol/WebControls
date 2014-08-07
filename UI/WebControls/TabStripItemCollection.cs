using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class TabStripItemCollection : List<TabStripItem>
    {
        public TabStripItem this[string commandName]
        {
            get
            {
                foreach (TabStripItem tabControlItem in this)
                {
                    if (tabControlItem.CommandName == commandName)
                        return tabControlItem;
                }

                return null;
            }
        }

        public int GetItemIndex(string commandName)
        {
            int itemIndex = -1;

            foreach (TabStripItem tabControlItem in this)
            {
                itemIndex++;

                if (tabControlItem.CommandName == commandName)
                    return itemIndex;
            }

            return itemIndex;
        }

        public int GetItemIndex(TabStripItem tabControlItemCurrent)
        {
            int itemIndex = -1;

            foreach (TabStripItem tabControlItem in this)
            {
                itemIndex++;

                if (tabControlItem.CommandName == tabControlItemCurrent.CommandName)
                    return itemIndex;
            }

            return itemIndex;
        }
    }
}
