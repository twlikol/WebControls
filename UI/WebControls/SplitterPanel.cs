using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using Likol.Web.Resources;
using System.ComponentModel;

[assembly: WebResource("Likol.Web.Resources.Scripts.SplitterPanel.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:SplitterPanel runat=server></{0}:SplitterPanel>"),
    Designer("Likol.Design.Web.UI.WebControls.SplitterPanelDesigner"),
    ParseChildren(true),
    PersistChildren(false)
    ]
    public class SplitterPanel : CompositeControl, IScriptControl
    {
        private ITemplate masterTemplate;
        private ITemplate detailTemplate;

        #region public ITemplate MasterTemplate
        
        [
        TemplateContainer(typeof(SplitterPanelContainer)),
        PersistenceMode(PersistenceMode.InnerProperty),
        Browsable(false)
        ]
        public ITemplate MasterTemplate
        {
            get { return this.masterTemplate; }
            set { this.masterTemplate = value; }
        }

        #endregion

        #region public ITemplate DetailTemplate
        
        [
        TemplateContainer(typeof(SplitterPanelContainer)),
        PersistenceMode(PersistenceMode.InnerProperty),
        Browsable(false)
        ]
        public ITemplate DetailTemplate
        {
            get { return this.detailTemplate; }
            set { this.detailTemplate = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey
        
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        #endregion

        #region public SplitterPanelType PanelType
        
        [DefaultValue(SplitterPanelType.VerticalAlign)]
        public SplitterPanelType PanelType
        {
            get
            {
                object value = this.ViewState["PanelType"];

                if (value != null) return (SplitterPanelType)value;

                return SplitterPanelType.VerticalAlign;
            }
            set { this.ViewState["PanelType"] = value; }
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

        #region public string RemoveElementIDs
        
        [DefaultValue("")]
        public string RemoveElementIDs
        {
            get { return this.StringProperty("RemoveElementIDs"); }
            set { this.ViewState["RemoveElementIDs"] = value; }
        }

        #endregion

        #region public string ParentElementID

        [DefaultValue("")]
        public string ParentElementID
        {
            get { return this.StringProperty("ParentElementID"); }
            set { this.ViewState["ParentElementID"] = value; }
        }

        #endregion

        #region public string MasterCssClass
        
        [DefaultValue("")]
        public string MasterCssClass
        {
            get { return this.StringProperty("MasterCssClass"); }
            set { this.ViewState["MasterCssClass"] = value; }
        }

        #endregion

        #region public string DetailCssClass
        
        [DefaultValue("")]
        public string DetailCssClass
        {
            get { return this.StringProperty("DetailCssClass"); }
            set { this.ViewState["DetailCssClass"] = value; }
        }

        #endregion

        #region public string SplitterCssClass
        
        [DefaultValue("")]
        public string SplitterCssClass
        {
            get { return this.StringProperty("SplitterCssClass"); }
            set { this.ViewState["SplitterCssClass"] = value; }
        }

        #endregion

        #region public string SplitterMouseOverCssClass
        
        [DefaultValue("")]
        public string SplitterMouseOverCssClass
        {
            get { return this.StringProperty("SplitterMouseOverCssClass"); }
            set { this.ViewState["SplitterMouseOverCssClass"] = value; }
        }

        #endregion

        #region public int MasterWidth
        
        [DefaultValue("180")]
        public int MasterWidth
        {
            get { return this.IntProperty("MasterWidth", 180); }
            set { this.ViewState["MasterWidth"] = value; }
        }

        #endregion

        #region public int MinMasterWidth

        [DefaultValue("180")]
        public int MinMasterWidth
        {
            get { return this.IntProperty("MinMasterWidth", 180); }
            set { this.ViewState["MinMasterWidth"] = value; }
        }

        #endregion

        #region public int MinDetailWidth

        [DefaultValue("400")]
        public int MinDetailWidth
        {
            get { return this.IntProperty("MinDetailWidth", 400); }
            set { this.ViewState["MinDetailWidth"] = value; }
        }

        #endregion

        #region public int SplitterWidth
        
        [DefaultValue("3")]
        public int SplitterWidth
        {
            get { return this.IntProperty("SplitterWidth", 3); }
            set { this.ViewState["SplitterWidth"] = value; }
        }

        #endregion

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            TableCell tableCellMaster = new TableCell();
            tableCellMaster.ID = "Master";
            tableCellMaster.CssClass = this.MasterCssClass;
            tableCellMaster.Width = this.MasterWidth;
            tableCellMaster.VerticalAlign = VerticalAlign.Top;

            if (this.MasterTemplate != null)
            {
                SplitterPanelContainer container = new SplitterPanelContainer();
                container.ID = "MasterContainer";
                container.Style[HtmlTextWriterStyle.Overflow] = "auto";

                this.MasterTemplate.InstantiateIn(container);

                tableCellMaster.Controls.Add(container);
            }

            TableCell tableCellSplit = new TableCell();
            tableCellSplit.ID = "Splitter";
            tableCellSplit.CssClass = this.SplitterCssClass;
            tableCellSplit.Width = this.SplitterWidth;
            tableCellSplit.Attributes["onmousedown"] = "return false;";

            TableCell tableCellDetail = new TableCell();
            tableCellDetail.VerticalAlign = VerticalAlign.Top;
            tableCellDetail.ID = "Detail";
            tableCellDetail.CssClass = this.DetailCssClass;

            if (this.DetailTemplate != null)
            {
                SplitterPanelContainer container = new SplitterPanelContainer();
                container.ID = "DetailContainer";
                container.Style[HtmlTextWriterStyle.Overflow] = "hidden";

                this.DetailTemplate.InstantiateIn(container);

                tableCellDetail.Controls.Add(container);
            }

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(tableCellMaster);
            tableRow.Cells.Add(tableCellSplit);
            tableRow.Cells.Add(tableCellDetail);

            this.Controls.Add(tableRow);

            this.Width = Unit.Percentage(100);
        }

        #endregion

        public SplitterPanel()
        {
            Global.TimeIsValid();
        }

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)
        
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
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
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.SplitterPanel",
                    this.ClientID);

            descriptor.AddProperty("splitterCssClass", this.SplitterCssClass);
            descriptor.AddProperty("splitterMouseOverCssClass", this.SplitterMouseOverCssClass);

            descriptor.AddProperty("panelType", this.PanelType);
            descriptor.AddProperty("sizeMode", this.SizeMode);

            descriptor.AddProperty("removeElementIDs", this.RemoveElementIDs);

            descriptor.AddProperty("minMasterWidth", this.MinMasterWidth);
            descriptor.AddProperty("minDetailWidth", this.MinDetailWidth);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()
        
        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("SplitterPanel");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
