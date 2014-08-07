using System;
using System.Collections.Generic;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class SearchKeyItemCollection : List<SearchKeyItem>
    {
        public SearchKeyItem this[string key]
        {
            get
            {
                foreach (SearchKeyItem searchKeyItem in this)
                {
                    if (searchKeyItem.Key == key)
                    {
                        return searchKeyItem;
                    }
                }

                return null;
            }
        }
    }
}
