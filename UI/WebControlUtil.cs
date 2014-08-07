using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace Likol.Web.UI
{
    internal class WebControlUtil
    {
        #region internal static Control FindControl(Control control, string controlID)
        
        internal static Control FindControl(Control control, string controlID)
        {
            Control control1 = control;
            Control control2 = null;

            if (control == control.Page)
            {
                return control.FindControl(controlID);
            }

            while ((control2 == null) && (control1 != control.Page))
            {
                control1 = control1.NamingContainer;

                if (control1 == null)
                {
                    throw new HttpException(string.Format("The {0} control '{1}' does not have a naming container.", control.GetType().Name, control.ID));
                }

                control2 = control1.FindControl(controlID);
            }

            return control2;
        }

        #endregion
    }
}
