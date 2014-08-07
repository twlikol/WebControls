using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class SelectKeyValue
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public SelectKeyValue(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", this.Key, this.Value);
        }
    }
}
