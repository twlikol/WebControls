using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class SearchKeyValueCollection : List<SearchKeyValue>
    {
        public void Add(string key, bool allowEmpty, string name, string text)
        {
            this.Add(new SearchKeyValue(key, allowEmpty, name, text));
        }

        public override string ToString()
        {
            string returnValue = "";

            foreach (SearchKeyValue searchKeyValue in this)
            {
                returnValue += searchKeyValue.ToString() + ";";
            }

            return returnValue;
        }
    }
}
