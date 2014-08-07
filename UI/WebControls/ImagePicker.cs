using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using Likol.Web.Resources;
using System.Web.UI.WebControls;
using System.Drawing.Design;

[assembly: WebResource("Likol.Web.Resources.Scripts.ImagePicker.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.ImagePicker.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:ImagePicker runat=server></{0}:ImagePicker>")
    ]
    public class ImagePicker : CompositeControl, IScriptControl
    {
        private HiddenField hfImage;
        private HiddenField hfImageClient;
        private Panel panelButton;
        private Image imgIcon;
        private Image imgImage;

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

        #region public int DialogWidth

        [
        DefaultValue(800),
        Category("Likol")
        ]
        public int DialogWidth
        {
            get { return this.IntProperty("DialogWidth", 800); }
            set { this.ViewState["DialogWidth"] = value; }
        }

        #endregion

        #region public int DialogHeight

        [
        DefaultValue(600),
        Category("Likol")
        ]
        public int DialogHeight
        {
            get { return this.IntProperty("DialogHeight", 600); }
            set { this.ViewState["DialogHeight"] = value; }
        }

        #endregion

        #region public string DialogUrl

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string DialogUrl
        {
            get { return this.StringProperty("DialogUrl"); }
            set { this.ViewState["DialogUrl"] = value; }
        }

        #endregion

        #region public int ImageWidth

        [
        DefaultValue(100),
        Category("Likol")
        ]
        public int ImageWidth
        {
            get { return this.IntProperty("ImageWidth", 100); }
            set { this.ViewState["ImageWidth"] = value; }
        }

        #endregion

        #region public int ImageHeight

        [
        DefaultValue(100),
        Category("Likol")
        ]
        public int ImageHeight
        {
            get { return this.IntProperty("ImageHeight", 100); }
            set { this.ViewState["ImageHeight"] = value; }
        }

        #endregion

        #region public string ButtonText

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ButtonText
        {
            get { return this.StringProperty("ButtonText"); }
            set { this.ViewState["ButtonText"] = value; }
        }

        #endregion

        #region public string ButtonCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ButtonCssClass
        {
            get { return this.StringProperty("ButtonCssClass"); }
            set { this.ViewState["ButtonCssClass"] = value; }
        }

        #endregion

        #region public string ButtonHoverCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ButtonHoverCssClass
        {
            get { return this.StringProperty("ButtonHoverCssClass"); }
            set { this.ViewState["ButtonHoverCssClass"] = value; }
        }

        #endregion

        #region public bool ReadOnly

        [
        DefaultValue(false),
        Category("Likol")
        ]
        public bool ReadOnly
        {
            get { return this.BooleanProperty("ReadOnly", false); }
            set { this.ViewState["ReadOnly"] = value; }
        }

        #endregion

        #region public string ImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string ImageUrl
        {
            get
            {
                this.EnsureChildControls();

                return this.hfImage.Value;
            }
            set
            {
                this.EnsureChildControls();

                this.hfImage.Value = value;
                this.hfImageClient.Value = this.ResolveClientUrl(value);
            }
        }

        #endregion

        #region public string IconImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string IconImageUrl
        {
            get { return this.StringProperty("IconImageUrl"); }
            set { this.ViewState["IconImageUrl"] = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

        public ImagePicker()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.hfImage = new HiddenField();
            this.hfImage.ID = "Value";
            //this.hfImage.Value = this.ImageUrl;

            this.hfImageClient = new HiddenField();
            this.hfImageClient.ID = "Value_Client";
            //this.hfImageClient.Value = this.ResolveClientUrl(this.ImageUrl);

            this.panelButton = new Panel();
            this.panelButton.ID = "Button";
            this.panelButton.CssClass = this.ButtonCssClass;

            //string iconImageUrl = this.ResolveClientUrl(this.IconImageUrl);

            //if (iconImageUrl == "")
            //    iconImageUrl = ResourceManager.GetImageWebResourceUrl(this, "ImagePicker");

            this.imgIcon = new Image();
            this.imgIcon.ImageAlign = ImageAlign.AbsMiddle;
            //this.imgIcon.ImageUrl = iconImageUrl;

            this.panelButton.Controls.Add(this.imgIcon);
            this.panelButton.Controls.Add(new LiteralControl(" " + this.ButtonText));

            this.Controls.Add(this.hfImage);
            this.Controls.Add(this.hfImageClient);

            if (!this.ReadOnly)
                this.Controls.Add(this.panelButton);

            this.imgImage = new Image();
            this.imgImage.ID = "Image";
            
            TableCell tableCell = new TableCell();
            tableCell.Width = this.ImageWidth;
            tableCell.Height = this.ImageHeight;
            tableCell.HorizontalAlign = HorizontalAlign.Center;
            tableCell.VerticalAlign = VerticalAlign.Middle;
            tableCell.Controls.Add(this.imgImage);

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(tableCell);

            Table table = new Table();
            table.CellSpacing = 0;
            table.CellPadding = 0;
            table.BorderWidth = 0;
            //table.Width = this.Width;
            //table.Height = this.Height;
            table.Rows.Add(tableRow);

            this.Controls.Add(table);

            this.Width = this.ImageWidth;
        }

        #endregion

        #region protected override void OnPreRender(EventArgs e)

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string iconImageUrl = this.ResolveClientUrl(this.IconImageUrl);

            if (iconImageUrl == "")
                iconImageUrl = ResourceManager.GetImageWebResourceUrl(this, "ImagePicker");

            this.imgIcon.ImageUrl = iconImageUrl;

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
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.ImagePicker",
                    this.ClientID);

            descriptor.AddProperty("cssClass", this.CssClass);
            descriptor.AddProperty("hoverCssClass", this.HoverCssClass);
            descriptor.AddProperty("buttonCssClass", this.ButtonCssClass);
            descriptor.AddProperty("buttonHoverCssClass", this.ButtonHoverCssClass);
            descriptor.AddProperty("dialogWidth", this.DialogWidth);
            descriptor.AddProperty("dialogHeight", this.DialogHeight);
            descriptor.AddProperty("dialogUrl", this.ResolveUrl(this.DialogUrl));
            descriptor.AddProperty("imageWidth", this.ImageWidth);
            descriptor.AddProperty("imageHeight", this.ImageHeight);
            descriptor.AddProperty("readOnly", this.ReadOnly);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("ImagePicker");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
