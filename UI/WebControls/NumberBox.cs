using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using Likol.Web.Resources;
using Likol.Web.UI.Validation;

[assembly: WebResource("Likol.Web.Resources.Scripts.NumberBox.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:NumberBox runat=server></{0}:NumberBox>")
    ]
    public class NumberBox : CompositeControl, IScriptControl
    {
        private TextBox txtValue;

        private RequiredFieldImageValidator requiredValidator;
        private CompareImageValidator compareValidator;

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

        #region public NumberBoxType Type

        [
        DefaultValue(NumberBoxType.Integer),
        Category("Likol"),
        ]
        public NumberBoxType Type
        {
            get
            {
                object value = this.ViewState["Type"];

                if (value == null) return NumberBoxType.Integer;

                return (NumberBoxType)value;
            }
            set { this.ViewState["Type"] = value; }
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

        #region public Unit BoxWidth

        [
        DefaultValue(150),
        Category("Likol"),
        ]
        public Unit BoxWidth
        {
            get
            {
                object value = this.ViewState["BoxWidth"];

                if (value == null) return 150;

                return (Unit)value;
            }
            set { this.ViewState["BoxWidth"] = value; }
        }

        #endregion

        #region public string Text

        public string Text
        {
            get
            {
                this.EnsureChildControls();

                return this.txtValue.Text;
            }
            set
            {
                this.EnsureChildControls();

                this.txtValue.Text = value;
            }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        #region protected override void CreateChildControls()

        protected override void CreateChildControls()
        {
            this.txtValue = new TextBox();
            this.txtValue.ID = "Value";
            this.txtValue.Width = this.BoxWidth;
            this.txtValue.Style[HtmlTextWriterStyle.TextAlign] = "right";
            this.txtValue.CssClass = this.TextBoxCssClass;

            this.txtValue.ReadOnly = this.ReadOnly;

            TableCell cellTextBox = new TableCell();
            cellTextBox.Controls.Add(this.txtValue);

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(cellTextBox);

            this.requiredValidator = new RequiredFieldImageValidator();
            this.requiredValidator.ControlToValidate = this.txtValue.ID;
            this.requiredValidator.ImageUrl = this.ValidationImageUrl;
            this.requiredValidator.ErrorMessage = this.ErrorMessage;
            this.requiredValidator.ValidationGroup = this.ValidationGroup;

            if (this.Type == NumberBoxType.Integer)
                this.compareValidator = new IntegerImageValidator();
            else
                this.compareValidator = new DoubleImageValidator();

            this.compareValidator.ControlToValidate = this.txtValue.ID;
            this.compareValidator.ImageUrl = this.ValidationImageUrl;
            this.compareValidator.ErrorMessage = this.ErrorMessage;
            this.compareValidator.ValidationGroup = this.ValidationGroup;

            TableCell cellValidation = new TableCell();
            cellValidation.CssClass = this.ValidationCssClass;
            cellValidation.Controls.Add(this.requiredValidator);
            cellValidation.Controls.Add(this.compareValidator);

            tableRow.Cells.Add(cellValidation);

            this.Controls.Add(tableRow);
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
                ScriptManager.GetCurrent(this.Page).RegisterScriptControl(this);
            }
        }

        #endregion

        #region protected override void Render(HtmlTextWriter writer)

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (!this.DesignMode) ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.NumberBox",
                    this.ClientID);

            descriptor.AddProperty("type", this.Type);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("NumberBox");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
