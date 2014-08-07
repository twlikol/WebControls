using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("Likol.Web.Resources.Scripts.Core.js", "application/x-javascript")]

namespace Likol.Web.Resources
{
    internal static class ResourceManager
    {
        #region internal static void RegisterCoreScriptResource(Page page)
        
        internal static void RegisterCoreScriptResource(Page page)
        {
            ScriptManager.RegisterClientScriptResource(
                page,
                typeof(CoreResourceAccessor),
                "Likol.Web.Resources.Scripts.Core.js");
        }

        #endregion

        #region internal static ScriptReference GetScriptReference(string controlName)
        
        internal static ScriptReference GetScriptReference(string controlName)
        {
            return new ScriptReference(
                string.Format("Likol.Web.Resources.Scripts.{0}.js", controlName),
                    "Likol.Web");
        }

        #endregion

        #region internal static string GetImageWebResourceUrl(WebControl webControl, string imageName)
        
        internal static string GetImageWebResourceUrl(WebControl webControl, string imageName)
        {
            return webControl.Page.ClientScript.GetWebResourceUrl(
                typeof(CoreResourceAccessor), string.Format("Likol.Web.Resources.Images.{0}.gif",
                    imageName));
        }

        #endregion
    }
}
