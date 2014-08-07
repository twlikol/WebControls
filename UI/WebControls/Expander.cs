using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Likol.Web.Resources;
using System.ComponentModel;
using System.Web;

[assembly: WebResource("Likol.Web.Resources.Scripts.Expander.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.ExpanderCollapse.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.ExpanderExpand.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:Expander runat=server></{0}:Expander>")
    ]
    public class Expander : Panel, IScriptControl
    {
        private Panel panelHeader = null;

        private HiddenField hfStatus = null;

        #region public string CollapseCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string CollapseCssClass
        {
            get
            {
                object value = this.ViewState["CollapseCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["CollapseCssClass"] = value; }
        }

        #endregion

        #region public string CollapseHoverCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string CollapseHoverCssClass
        {
            get
            {
                object value = this.ViewState["CollapseHoverCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["CollapseHoverCssClass"] = value; }
        }

        #endregion

        #region public string CollapseTitle

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string CollapseTitle
        {
            get
            {
                object value = this.ViewState["CollapseTitle"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["CollapseTitle"] = value; }
        }

        #endregion

        #region public string CollapseImageUrl

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string CollapseImageUrl
        {
            get
            {
                object value = this.ViewState["CollapseImageUrl"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["CollapseImageUrl"] = value; }
        }

        #endregion

        #region public string ExpandCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ExpandCssClass
        {
            get
            {
                object value = this.ViewState["ExpandCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ExpandCssClass"] = value; }
        }

        #endregion

        #region public string ExpandHoverCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ExpandHoverCssClass
        {
            get
            {
                object value = this.ViewState["ExpandHoverCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ExpandHoverCssClass"] = value; }
        }

        #endregion

        #region public string ExpandTitle

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ExpandTitle
        {
            get
            {
                object value = this.ViewState["ExpandTitle"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ExpandTitle"] = value; }
        }

        #endregion

        #region public string ExpandImageUrl

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ExpandImageUrl
        {
            get
            {
                object value = this.ViewState["ExpandImageUrl"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ExpandImageUrl"] = value; }
        }

        #endregion

        public Expander()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            string collapseImageUrl = this.ResolveClientUrl(this.CollapseImageUrl);

            if (collapseImageUrl == "")
                collapseImageUrl = ResourceManager.GetImageWebResourceUrl(this, "ExpanderCollapse");

            Image imageCollapse = new Image();
            imageCollapse.ImageAlign = ImageAlign.AbsMiddle;
            imageCollapse.ImageUrl = collapseImageUrl;

            string expandImageUrl = this.ResolveClientUrl(this.ExpandImageUrl);

            if (expandImageUrl == "")
                expandImageUrl = ResourceManager.GetImageWebResourceUrl(this, "ExpanderExpand");

            Image imageExpand = new Image();
            imageExpand.ImageAlign = ImageAlign.AbsMiddle;
            imageExpand.ImageUrl = expandImageUrl;

            Panel panelCollapse = new Panel();
            panelCollapse.ID = this.ClientID + "_Collapse";
            panelCollapse.CssClass = this.CollapseCssClass;
            panelCollapse.Controls.Add(imageCollapse);
            panelCollapse.Controls.Add(new LiteralControl(" "));
            panelCollapse.Controls.Add(new LiteralControl(this.CollapseTitle));

            panelCollapse.Attributes["onmouseover"] = string.Format("this.className='{0}'", this.CollapseHoverCssClass);
            panelCollapse.Attributes["onmouseout"] = string.Format("this.className='{0}'", this.CollapseCssClass);

            Panel panelExpand = new Panel();
            panelExpand.ID = this.ClientID + "_Expand";
            panelExpand.CssClass = this.ExpandCssClass;
            panelExpand.Controls.Add(imageExpand);
            panelExpand.Controls.Add(new LiteralControl(" "));
            panelExpand.Controls.Add(new LiteralControl(this.ExpandTitle));

            panelExpand.Attributes["onmouseover"] = string.Format("this.className='{0}'", this.ExpandHoverCssClass);
            panelExpand.Attributes["onmouseout"] = string.Format("this.className='{0}'", this.ExpandCssClass);

            this.panelHeader = new Panel();
            this.panelHeader.Controls.Add(panelCollapse);
            this.panelHeader.Controls.Add(panelExpand);

            //this.Controls.AddAt(0, panelHeader);

            this.hfStatus = new HiddenField();
            this.hfStatus.ID = this.ID + "_Status";
            this.hfStatus.Value = "Expand";

            this.Controls.Add(this.hfStatus);

            base.CreateChildControls();
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

            writer.Write("<div id=\"{0}_Expander\">", this.ClientID);

            this.panelHeader.RenderControl(writer);

            writer.Write("<div id=\"{0}_Content\">", this.ClientID);

            base.Render(writer);

            writer.Write("</div>");
            writer.Write("</div>");
        }

        #endregion

        public void Collapse()
        {
            this.EnsureChildControls();

            this.hfStatus.Value = "Collapse";
        }

        public void Expand()
        {
            this.EnsureChildControls();

            this.hfStatus.Value = "Expand";
        }

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.Expander",
                    this.ClientID);

            //descriptor.AddProperty("status", this.Status);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("Expander");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
