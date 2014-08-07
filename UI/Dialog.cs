using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Likol.Web.UI
{
    public class Dialog
    {
        #region public static void Close(Page page, string text, string value)

        public static void Close(Page page, string text, string value)
        {
            string scriptTextFormat = "";
            scriptTextFormat += "function DialogClose(){{";
            scriptTextFormat += "window.returnValue = '{0};{1}';";
            scriptTextFormat += "if (window.opener)";
            scriptTextFormat += "    window.opener.returnValue = '{0};{1}';";
            scriptTextFormat += "window.close();";
            scriptTextFormat += "}}";
            scriptTextFormat += "DialogClose();";

            string scriptText = string.Format(scriptTextFormat,
                text, value);

            ScriptManager.RegisterStartupScript(page, typeof(Dialog), "Close",
                scriptText, true);
        }

        #endregion

        #region public static void Close(Page page)

        public static void Close(Page page)
        {
            string scriptText = "";
            scriptText += "function DialogClose(){{";
            scriptText += "window.close();";
            scriptText += "}}";
            scriptText += "DialogClose();";

            ScriptManager.RegisterStartupScript(page, typeof(Dialog), "Close",
                scriptText, true);
        }

        #endregion
    }
}
