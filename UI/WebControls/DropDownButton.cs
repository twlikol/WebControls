using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Likol.Web.Resources;
using System.Drawing.Design;

[assembly: WebResource("Likol.Web.Resources.Scripts.DropDownButton.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.DropDownButtonArrow.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:DropDownButton runat=server></{0}:DropDownButton>")
    ]
    public class DropDownButton : CompositeControl, IScriptControl
    {
        private Image imgIcon;
        private Label lblText;
        private Image imgArrow;

        #region public string ImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string ImageUrl
        {
            get { return this.StringProperty("ImageUrl"); }
            set { this.ViewState["ImageUrl"] = value; }
        }

        #endregion

        #region public string Text

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string Text
        {
            get { return this.StringProperty("Text"); }
            set { this.ViewState["Text"] = value; }
        }

        #endregion

        #region public string ArrowImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string ArrowImageUrl
        {
            get { return this.StringProperty("ArrowImageUrl"); }
            set { this.ViewState["ArrowImageUrl"] = value; }
        }

        #endregion

        #region public string HoverCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string HoverCssClass
        {
            get { return this.StringProperty("HoverCssClass"); }
            set { this.ViewState["HoverCssClass"] = value; }
        }

        #endregion

        #region public DropDownButtonMode Mode

        [
        DefaultValue(DropDownButtonMode.Large),
        Category("Likol"),
        ]
        public DropDownButtonMode Mode
        {
            get
            {
                object value = this.ViewState["Mode"];

                if (value == null) return DropDownButtonMode.Large;

                return (DropDownButtonMode)value;
            }
            set { this.ViewState["Mode"] = value; }
        }

        #endregion

        #region public string ContextControlID

        public string ContextControlID
        {
            get { return this.StringProperty("ContextControlID"); }
            set { this.ViewState["ContextControlID"] = value; }
        }

        #endregion

        #region private IContextControl ContextControl

        private IContextControl ContextControl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ContextControlID))
                {
                    try
                    {
                        Control control = WebControlUtil.FindControl(this, this.ContextControlID);

                        if (control != null) return (IContextControl)control;
                    }
                    catch
                    {
                    }
                }
                return null;
            }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        public DropDownButton()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.imgIcon = new Image();
            this.imgIcon.ImageAlign = ImageAlign.AbsMiddle;
            this.imgIcon.ImageUrl = this.ImageUrl;

            this.lblText = new Label();
            this.lblText.Text = this.Text;

            this.imgArrow = new Image();
            this.imgArrow.ImageAlign = ImageAlign.AbsMiddle;

            string arrowImageUrl = this.ArrowImageUrl;

            if (arrowImageUrl == "")
            {
                arrowImageUrl = ResourceManager.GetImageWebResourceUrl(this, "DropDownButtonArrow");
            }

            this.imgArrow.ImageUrl = arrowImageUrl;

            TableCell tableCellIcon = new TableCell();
            tableCellIcon.VerticalAlign = VerticalAlign.Middle;
            tableCellIcon.HorizontalAlign = HorizontalAlign.Center;
            tableCellIcon.Controls.Add(this.imgIcon);

            TableCell tableCellText = new TableCell();
            tableCellText.VerticalAlign = VerticalAlign.Middle;
            tableCellText.HorizontalAlign = HorizontalAlign.Center;
            tableCellText.Controls.Add(this.lblText);

            TableCell tableCellArrow = new TableCell();
            tableCellArrow.VerticalAlign = VerticalAlign.Middle;
            tableCellArrow.HorizontalAlign = HorizontalAlign.Center;
            tableCellArrow.Controls.Add(this.imgArrow);

            //Table tableControl = new Table();
            //tableControl.CellSpacing = 0;
            //tableControl.CellPadding = 2;
            //tableControl.BorderWidth = 0;

            if (this.Mode == DropDownButtonMode.Small)
            {
                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(tableCellIcon);
                tableRow.Cells.Add(tableCellText);
                tableRow.Cells.Add(tableCellArrow);

                this.Controls.Add(tableRow);
            }
            else
            {
                TableRow tableRowIcon = new TableRow();
                tableRowIcon.Controls.Add(tableCellIcon);

                TableRow tableRowText = new TableRow();
                tableRowText.Controls.Add(tableCellText);

                TableRow tableRowArrow = new TableRow();
                tableRowArrow.Controls.Add(tableCellArrow);

                this.Controls.Add(tableRowIcon);
                this.Controls.Add(tableRowText);
                this.Controls.Add(tableRowArrow);
            }

            //TableCell tableCell = new TableCell();
            //tableCell.Controls.AddAt(0, tableControl);

            //TableRow tableRow = new TableRow();
            //tableRow.Cells.Add(tableCell);

            //this.Controls.Add(tableRow);
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

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.DropDownButton",
                    this.ClientID);

            string contextControlID = "";

            if (this.ContextControl != null)
                contextControlID = this.ContextControl.GetClientID();

            descriptor.AddProperty("cssClass", this.CssClass);
            descriptor.AddProperty("hoverCssClass", this.HoverCssClass);
            descriptor.AddProperty("contextControlID", contextControlID);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("DropDownButton");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
