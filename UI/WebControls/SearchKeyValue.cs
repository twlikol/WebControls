using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class SearchKeyValue
    {
        public string Key { get; set; }
        public bool AllowEmpty { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public SearchKeyValue(string key, bool allowEmpty, string name, string text)
        {
            this.Key = key;
            this.AllowEmpty = allowEmpty;
            this.Name = name;
            this.Text = text;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}", this.Key, this.AllowEmpty, this.Name, this.Text);
        }
    }
}
