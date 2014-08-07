using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class GroupButtonItemCollection : List<GroupButtonItem>
    {
        public GroupButtonItem this[string commandName]
        {
            get
            {
                foreach (GroupButtonItem groupButtonItem in this)
                {
                    if (groupButtonItem.CommandName == commandName)
                        return groupButtonItem;
                }

                return null;
            }
        }

        public int GetItemIndex(string commandName)
        {
            int itemIndex = -1;

            foreach (GroupButtonItem groupButtonItem in this)
            {
                itemIndex++;

                if (groupButtonItem.CommandName == commandName)
                    return itemIndex;
            }

            return itemIndex;
        }

        public int GetItemIndex(GroupButtonItem groupButtonItemCurrent)
        {
            int itemIndex = -1;

            foreach (GroupButtonItem groupButtonItem in this)
            {
                itemIndex++;

                if (groupButtonItem.CommandName == groupButtonItemCurrent.CommandName)
                    return itemIndex;
            }

            return itemIndex;
        }
    }
}
