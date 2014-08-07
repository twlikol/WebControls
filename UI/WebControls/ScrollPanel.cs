using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.ScrollPanel.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:ScrollPanel runat=server></{0}:ScrollPanel>")
    ]
    public class ScrollPanel : System.Web.UI.WebControls.Panel, IScriptControl
    {
        #region public PanelSizeMode SizeMode

        [DefaultValue(PanelSizeMode.FullPage)]
        public PanelSizeMode SizeMode
        {
            get
            {
                object value = this.ViewState["SizeMode"];

                if (value != null) return (PanelSizeMode)value;

                return PanelSizeMode.FullPage;
            }
            set { this.ViewState["SizeMode"] = value; }
        }

        #endregion

        #region public int ParentLevel

        [
        DefaultValue(2),
        Category("Likol")
        ]
        public int ParentLevel
        {
            get
            {
                object value = this.ViewState["ParentLevel"];

                if (value != null)
                {
                    return (int)value;
                }

                return 2;
            }
            set { this.ViewState["ParentLevel"] = value; }
        }

        #endregion

        #region public string RemoveElementIDs

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string RemoveElementIDs
        {
            get
            {
                object value = this.ViewState["RemoveElementIDs"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["RemoveElementIDs"] = value; }
        }

        #endregion

        public ScrollPanel()
        {
            Global.TimeIsValid();
        }

        #region protected override void OnPreRender(EventArgs e)

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //this.Style["overflow"] = "auto";
            //this.Style["zoom"] = "1";

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

            writer.Write("<div id=\"{0}_Container\">", this.ClientID);

            base.Render(writer);

            writer.Write("<div style=\"clear:both;\"></div></div>");
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.ScrollPanel",
                    this.ClientID);

            descriptor.AddProperty("sizeMode", this.SizeMode);
            descriptor.AddProperty("parentLevel", this.ParentLevel + 1);
            descriptor.AddProperty("removeElementIDs", this.RemoveElementIDs);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("ScrollPanel");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
