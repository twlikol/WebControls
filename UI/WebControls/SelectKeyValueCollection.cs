using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class SelectKeyValueCollection : List<SelectKeyValue>
    {
        public SelectKeyValue this[string key]
        {
            get
            {
                foreach (SelectKeyValue selectKeyValue in this)
                {
                    if (selectKeyValue.Key == key)
                    {
                        return selectKeyValue;
                    }
                }

                return null;
            }
        }

        public void Add(string key, object value)
        {
            this.Add(new SelectKeyValue(key, value));
        }

        public void SetValue(string key, object value)
        {
            SelectKeyValue selectKeyValue = this[key];

            if (selectKeyValue == null)
                this.Add(key, value);
            else
                selectKeyValue.Value = value;
        }

        public override string ToString()
        {
            string returnValue = "";

            foreach (SelectKeyValue selectKeyValue in this)
            {
                returnValue += selectKeyValue.ToString() + ";";
            }

            return returnValue;
        }
    }
}
