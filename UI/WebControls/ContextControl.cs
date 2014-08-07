using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.ContextControl.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    public abstract class ContextControl : CompositeControl, IScriptControl, IContextControl
    {
        #region protected override HtmlTextWriterTag TagKey
        
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        #endregion

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)
        
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (this.DesignMode)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
                //writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");

                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "-5000px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "-5000px");

                writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "2000");
            }
        }

        #endregion

        #region protected override void OnPreRender(EventArgs e)
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this.DesignMode)
            {
                ResourceManager.RegisterCoreScriptResource(this.Page);

                ScriptManager.GetCurrent(this.Page).RegisterScriptControl(this);
            }
        }

        #endregion

        #region protected override void Render(HtmlTextWriter writer)
        
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode) ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);

            base.Render(writer);
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        
        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.ContextControl",
                    this.ClientID);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("ContextControl");
            return new ScriptReference[] { scriptReference };
        }

        #endregion

        public string GetClientID()
        {
            return this.ClientID;
        }
    }
}
