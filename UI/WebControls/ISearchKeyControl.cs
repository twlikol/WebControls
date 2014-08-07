using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    public interface ISearchKeyControl
    {
        SearchKeyValueCollection SearchValues { get; }

        void SetValue(string key, object value);
    }
}
