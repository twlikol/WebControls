using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Likol.Web.Resources;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: WebResource("Likol.Web.Resources.Scripts.ScrollGridView.js", "application/x-javascript")]
[assembly: WebResource("Likol.Web.Resources.Scripts.ScrollGridView2.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:ScrollGridView runat=server></{0}:ScrollGridView>")
    ]
    public class ScrollGridView : System.Web.UI.WebControls.GridView, IScriptControl
    {
        public ScrollGridView()
        {
            this.GridLines = GridLines.None;
            this.CellSpacing = 0;
            this.CellPadding = 0;
            this.AutoGenerateColumns = false;
            this.UseAccessibleHeader = false;

            Global.TimeIsValid();
        }

        #region Override Default Properties
        
        [DefaultValue(GridLines.None)]
        public override GridLines GridLines
        {
            get { return base.GridLines; }
            set { base.GridLines = value; }
        }

        [DefaultValue(0)]
        public override int CellSpacing
        {
            get { return base.CellSpacing; }
            set { base.CellSpacing = value; }
        }

        [DefaultValue(0)]
        public override int CellPadding
        {
            get { return base.CellPadding; }
            set { base.CellPadding = value; }
        }

        [DefaultValue(false)]
        public override bool AutoGenerateColumns
        {
            get { return base.AutoGenerateColumns; }
            set { base.AutoGenerateColumns = value; }
        }

        [DefaultValue(false)]
        public override bool UseAccessibleHeader
        {
            get { return base.UseAccessibleHeader; }
            set { base.UseAccessibleHeader = value; }
        }

        #endregion

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
        DefaultValue(4),
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

                return 4;
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

        #region public bool EnabledSelect

        [
        DefaultValue(false),
        Category("Likol")
        ]
        public bool EnabledSelect
        {
            get
            {
                object value = this.ViewState["EnabledSelect"];

                if (value != null)
                {
                    return (bool)value;
                }

                return false;
            }
            set { this.ViewState["EnabledSelect"] = value; }
        }

        #endregion

        #region public Unit ScrollHeight

        [
        DefaultValue(typeof(Unit), ""),
        Category("Likol")
        ]
        public Unit ScrollHeight
        {
            get
            {
                object value = this.ViewState["ScrollHeight"];

                if (value != null)
                {
                    return (Unit)value;
                }

                return Unit.Empty;
            }
            set { this.ViewState["ScrollHeight"] = value; }
        }

        #endregion

        #region public int Version

        [
        DefaultValue(1),
        Category("Likol")
        ]
        public int Version
        {
            get
            {
                object value = this.ViewState["Version"];

                if (value != null)
                {
                    return (int)value;
                }

                return 1;
            }
            set { this.ViewState["Version"] = value; }
        }

        #endregion

        #region public int FreezeColumns

        [
        DefaultValue(2),
        Category("Likol")
        ]
        public int FreezeColumns
        {
            get
            {
                object value = this.ViewState["FreezeColumns"];

                if (value != null)
                {
                    return (int)value;
                }

                return 2;
            }
            set { this.ViewState["FreezeColumns"] = value; }
        }

        #endregion

        #region public string FreezeHeaderCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string FreezeHeaderCssClass
        {
            get
            {
                object value = this.ViewState["FreezeHeaderCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["FreezeHeaderCssClass"] = value; }
        }

        #endregion

        #region public string FreezeItemCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string FreezeItemCssClass
        {
            get
            {
                object value = this.ViewState["FreezeItemCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["FreezeItemCssClass"] = value; }
        }

        #endregion

        #region protected override void OnRowDataBound(GridViewRowEventArgs e)

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);

            if (e.Row.RowType != DataControlRowType.DataRow) return;
            
            if (this.EnabledSelect)
                e.Row.Attributes.Add("ondblclick", Page.ClientScript.GetPostBackEventReference(this, "Select$" + e.Row.RowIndex.ToString()));
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

            if (this.Version == 2)
            {
                writer.Write("<div id=\"{0}_Float_Header\" style=\"overflow:hidden;zoom: 1;\">", this.ClientID);
                writer.Write("<table id=\"{0}_Float_Header_Table\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:0px;border-collapse:collapse;z-index:11;\">", this.ClientID);
                writer.Write("</table>");
                writer.Write("</div>");
            }

            writer.Write("<div id=\"{0}_Header\" style=\"overflow:scroll;zoom: 1;overflow-x:hidden;", this.ClientID);

            if (this.SizeMode == PanelSizeMode.Custom)
                writer.Write("width:{0};", this.Width.ToString());
            else
                writer.Write("width:100%;");

            writer.Write("\">");
            writer.Write("<div style=\"zoom: 1;\">");
            writer.Write("<table id=\"{0}_Header_Table\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:0px;border-collapse:collapse;", this.ClientID);

            if (this.SizeMode == PanelSizeMode.Custom)
                writer.Write("width:{0};", this.Width.ToString());
            else
                writer.Write("width:100%;");
            
            writer.Write("\">");
            writer.Write("</table>");
            writer.Write("</div>");
            writer.Write("</div>");

            writer.Write("<div id=\"{0}_Item\" style=\"overflow:scroll;zoom:1;", this.ClientID);

            if (this.SizeMode == PanelSizeMode.Custom)
                writer.Write("width:{0};height:{1};", this.Width.ToString(), this.ScrollHeight.ToString());
            else
                writer.Write("width:100%;");

            writer.Write("\">");

            if (this.Version == 2)
            {
                writer.Write("<div id=\"{0}_Float_Item\" style=\"overflow:hidden;zoom:1;z-index:10;\">", this.ClientID);
                writer.Write("</div>");
            }

            base.Render(writer);
            writer.Write("</div>");
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.ScrollGridView",
                    this.ClientID);

            descriptor.AddProperty("sizeMode", this.SizeMode);
            descriptor.AddProperty("parentLevel", this.ParentLevel);
            descriptor.AddProperty("removeElementIDs", this.RemoveElementIDs);

            if (this.Version == 2)
            {
                descriptor.AddProperty("freezeColumns", this.FreezeColumns);    
                descriptor.AddProperty("freezeHeaderCssClass", this.FreezeHeaderCssClass);
                descriptor.AddProperty("freezeItemCssClass", this.FreezeItemCssClass);
            }

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            string scriptResource = "";

            if (this.Version == 1)
            {
                scriptResource = "ScrollGridView";
            }
            else if (this.Version == 2)
            {
                scriptResource = "ScrollGridView2";
            }

            ScriptReference scriptReference = ResourceManager.GetScriptReference(scriptResource);
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}