using System;
using System.Collections.Generic;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class ContextMenuItemCollection : List<ContextMenuItem>
    {
        #region public ContextMenuItem this[string commandName]
        
        public ContextMenuItem this[string commandName]
        {
            get
            {
                foreach (ContextMenuItem contextMenuItem in this)
                {
                    if (contextMenuItem.CommandName == commandName)
                    {
                        return contextMenuItem;
                    }
                }

                return null;
            }
        }

        #endregion

        #region public int FindIndex(ContextMenuItem findContextMenuItem)
        
        public int FindIndex(ContextMenuItem findContextMenuItem)
        {
            int itemIndex = 0;

            foreach (ContextMenuItem contextMenuItem in this)
            {
                if (contextMenuItem.Equals(findContextMenuItem))
                {
                    return itemIndex;
                }

                itemIndex++;
            }

            return -1;
        }

        #endregion
    }
}
