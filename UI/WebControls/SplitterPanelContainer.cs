using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Likol.Web.UI.WebControls
{
    public class SplitterPanelContainer : System.Web.UI.WebControls.Panel, INamingContainer
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //this.Style[HtmlTextWriterStyle.Overflow] = "auto";
            //this.Style["zoom"] = "1";
        }
    }
}
