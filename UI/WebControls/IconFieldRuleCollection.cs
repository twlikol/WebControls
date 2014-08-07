using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    public class IconFieldRuleCollection : List<IconFieldRule>
    {
        public IconFieldRule this[string value]
        {
            get
            {
                foreach (IconFieldRule iconFieldRule in this)
                {
                    if (iconFieldRule.Value == value) return iconFieldRule;
                }

                return null;
            }
        }
    }
}
