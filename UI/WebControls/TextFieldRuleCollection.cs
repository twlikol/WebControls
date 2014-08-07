using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    public class TextFieldRuleCollection : List<TextFieldRule>
    {
        public TextFieldRule this[string value]
        {
            get
            {
                foreach (TextFieldRule textFieldRule in this)
                {
                    if (textFieldRule.Value == value) return textFieldRule;
                }

                return null;
            }
        }
    }
}
