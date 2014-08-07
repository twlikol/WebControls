using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    public class WindowPanelContentControl : UserControl, IWindowPanelContent
    {
        #region public string Status

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string Status
        {
            get
            {
                object value = this.ViewState["Status"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["Status"] = value; }
        }

        #endregion

        public virtual void Initialize(object value)
        {
            
        }
    }
}
