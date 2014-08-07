using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Likol.Web.Resources;
using System.Drawing.Design;
using Likol.Web.UI.Validation;

[assembly: WebResource("Likol.Web.Resources.Scripts.DialogBox.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.DialogBox.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:DialogBox runat=server></{0}:DialogBox>"),
    DefaultProperty("Value"),
    ControlValueProperty("Value"),
    ValidationProperty("Value")
    ]
    public class DialogBox : CompositeControl, IScriptControl
    {
        private Label lblText;
        private TextBox txtText;
        private LiteralControl lcButton;

        private HiddenField hfText;
        private HiddenField hfValue;

        private RequiredFieldImageValidator requiredValidator;

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

        #region public string TextBoxCssClass
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string TextBoxCssClass
        {
            get { return this.StringProperty("TextBoxCssClass"); }
            set { this.ViewState["TextBoxCssClass"] = value; }
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
        
        public string Text
        {
            get
            {
                return this.hfText.Value;
            }
            set
            {
                this.lblText.Text = value;

                if (value == "")
                    this.hfText.Value = value;
            }
        }

        #endregion

        #region public string Value
        
        public string Value
        {
            get { return this.hfValue.Value; }
            set
            {
                this.hfValue.Value = value;

                if (value == "")
                    this.lblText.Text = value;
            }
        }

        #endregion

        #region public bool ReadOnly
        
        public bool ReadOnly
        {
            get
            {
                return !this.lcButton.Visible;
            }
            set
            {
                this.lcButton.Visible = !value;
            }
        }

        #endregion

        #region public int BoxWidth

        [
        DefaultValue(150),
        Category("Likol")
        ]
        public int BoxWidth
        {
            get { return this.IntProperty("BoxWidth", 150); }
            set { this.ViewState["BoxWidth"] = value; }
        }

        #endregion

        #region public bool Required

        [
        DefaultValue(false),
        Category("Likol")
        ]
        public bool Required
        {
            get { return this.BooleanProperty("Required", false); }
            set { this.ViewState["Required"] = value; }
        }

        #endregion

        #region public string ValidationCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ValidationCssClass
        {
            get { return this.StringProperty("ValidationCssClass"); }
            set { this.ViewState["ValidationCssClass"] = value; }
        }

        #endregion

        #region public string ValidationImageUrl

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ValidationImageUrl
        {
            get { return this.StringProperty("ValidationImageUrl"); }
            set { this.ViewState["ValidationImageUrl"] = value; }
        }

        #endregion

        #region public string ErrorMessage

        [DefaultValue("")]
        public string ErrorMessage
        {
            get { return this.StringProperty("ErrorMessage"); }
            set { this.ViewState["ErrorMessage"] = value; }
        }

        #endregion

        #region public string ValidationGroup

        [DefaultValue("")]
        public string ValidationGroup
        {
            get { return this.StringProperty("ValidationGroup"); }
            set { this.ViewState["ValidationGroup"] = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey
        
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        #region public DialogBox()
        
        public DialogBox()
        {
            this.lblText = new Label();
            this.lcButton = new LiteralControl();
            this.hfText = new HiddenField();
            this.hfValue = new HiddenField();

            Global.TimeIsValid();
        }

        #endregion

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            this.lblText.ID = "Text";

            this.txtText = new TextBox();
            this.txtText.ID = "Box";
            this.txtText.CssClass = this.TextBoxCssClass;
            this.txtText.Style[HtmlTextWriterStyle.Display] = "none";

            Panel panelTextBox = new Panel();
            panelTextBox.ID = "TextBox";
            panelTextBox.CssClass = this.TextBoxCssClass;

            if (this.BoxWidth != 0)
            {
                this.txtText.Width = this.BoxWidth;

                panelTextBox.Width = this.BoxWidth;
            }

            if (this.DesignMode) panelTextBox.Height = 16;

            panelTextBox.Controls.Add(this.lblText);

            TableCell cellTextBox = new TableCell();
            cellTextBox.Controls.Add(panelTextBox);
            cellTextBox.Controls.Add(this.txtText);

            string imageUrl = this.ResolveClientUrl(this.ImageUrl);

            if (imageUrl == "")
                imageUrl = ResourceManager.GetImageWebResourceUrl(this, "DialogBox");

            this.lcButton.Text = string.Format("<img id='{0}' alt='' src='{1}' align='absmiddle' />",
                this.ClientID + "_Button", imageUrl);

            this.hfText.ID = "hfText";

            this.hfValue.ID = "hfValue";
            this.hfValue.ValueChanged += new EventHandler(hfValue_ValueChanged);

            TableCell cellButton = new TableCell();
            cellButton.CssClass = this.ButtonCssClass;
            cellButton.Controls.Add(this.lcButton);
            cellButton.Controls.Add(this.hfText);
            cellButton.Controls.Add(this.hfValue);

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(cellTextBox);
            tableRow.Cells.Add(cellButton);

            if (this.Required)
            {
                this.requiredValidator = new RequiredFieldImageValidator();
                this.requiredValidator.ControlToValidate = this.hfValue.ID;
                this.requiredValidator.ImageUrl = this.ValidationImageUrl;
                this.requiredValidator.ErrorMessage = this.ErrorMessage;
                this.requiredValidator.ValidationGroup = this.ValidationGroup;

                TableCell cellValidation = new TableCell();
                cellValidation.CssClass = this.ValidationCssClass;
                cellValidation.Controls.Add(this.requiredValidator);

                tableRow.Cells.Add(cellValidation);
            }

            this.Controls.Add(tableRow);
        }

        #endregion

        #region protected void hfValue_ValueChanged(object sender, EventArgs e)
        
        protected void hfValue_ValueChanged(object sender, EventArgs e)
        {
            this.Text = this.hfText.Value;
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
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.DialogBox",
                    this.ClientID);

            descriptor.AddProperty("dialogWidth", this.DialogWidth);
            descriptor.AddProperty("dialogHeight", this.DialogHeight);
            descriptor.AddProperty("dialogUrl", this.ResolveUrl(this.DialogUrl));

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()
        
        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("DialogBox");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
