using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class DropDownItemCollection : List<DropDownItem>
    {
        public DropDownItem this[string value]
        {
            get
            {
                foreach (DropDownItem dropDownItem in this)
                {
                    if (dropDownItem.Value == value) return dropDownItem;
                }

                return null;
            }
        }
    }
}
