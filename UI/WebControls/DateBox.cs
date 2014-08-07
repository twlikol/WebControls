using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Likol.Web.Resources;
using Likol.Web.UI.Validation;

[assembly: WebResource("Likol.Web.Resources.Images.DateBox.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:DateBox runat=server></{0}:DateBox>"),
    DefaultProperty("DateText"),
    ControlValueProperty("DateText"),
    ValidationProperty("DateText")
    ]
    public class DateBox : CompositeControl
    {
        private TextBox txtValue;
        private System.Web.UI.WebControls.ImageButton ibButton;
        private CalendarExtender ceCalendar;

        private RequiredFieldImageValidator requiredValidator;
        private DateImageValidator dateValidator;

        #region public string ImageUrl

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ImageUrl
        {
            get { return this.StringProperty("ImageUrl"); }
            set { this.ViewState["ImageUrl"] = value; }
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

        #region public bool EnableValidation

        [
        DefaultValue(true),
        Category("Likol")
        ]
        public bool EnableValidation
        {
            get { return this.BooleanProperty("EnableValidation", true); }
            set { this.ViewState["EnableValidation"] = value; }
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

        #region public string Format
        
        [
        DefaultValue("yyyy/MM/dd"),
        Category("Likol")
        ]
        public string Format
        {
            get { return this.StringProperty("Format", "yyyy/MM/dd"); }
            set { this.ViewState["Format"] = value; }
        }

        #endregion

        #region public string DateText
        
        public string DateText
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

        #region public DateTime Date

        public DateTime Date
        {
            get
            {
                DateTime dtValue;

                bool isValid = DateTime.TryParse(this.DateText, out dtValue);

                if (isValid) return dtValue;

                return DateTime.MinValue;
            }
            set { this.DateText = value.ToString(this.Format); }
        }

        #endregion        

        #region protected override HtmlTextWriterTag TagKey
        
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        public DateBox()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            this.txtValue = new TextBox();
            this.txtValue.ID = "Value";
            this.txtValue.MaxLength = 10;
            this.txtValue.CssClass = this.TextBoxCssClass;

            this.txtValue.ReadOnly = this.ReadOnly;

            TableCell cellTextBox = new TableCell();
            cellTextBox.Controls.Add(this.txtValue);

            this.ibButton = new System.Web.UI.WebControls.ImageButton();
            this.ibButton.ImageAlign = ImageAlign.AbsMiddle;
            this.ibButton.ID = "Button";
            this.ibButton.Width = 16;
            this.ibButton.Height = 16;

            string imageUrl = this.ResolveClientUrl(this.ImageUrl);

            if (imageUrl == "")
                imageUrl = ResourceManager.GetImageWebResourceUrl(this, "DateBox");

            this.ibButton.ImageUrl = imageUrl;

            this.ceCalendar = new CalendarExtender();
            this.ceCalendar.ID = "Calendar";
            this.ceCalendar.TargetControlID = this.txtValue.ID;
            this.ceCalendar.PopupButtonID = this.ibButton.ID;
            this.ceCalendar.Format = this.Format;

            TableCell cellButton = new TableCell();
            cellButton.CssClass = this.ButtonCssClass;
            cellButton.Controls.Add(this.ibButton);
            cellButton.Controls.Add(this.ceCalendar);

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(cellTextBox);

            if (!this.ReadOnly)
                tableRow.Cells.Add(cellButton);

            if (this.EnableValidation)
            {
                this.requiredValidator = new RequiredFieldImageValidator();
                this.requiredValidator.ControlToValidate = this.txtValue.ID;
                this.requiredValidator.ImageUrl = this.ValidationImageUrl;
                this.requiredValidator.ErrorMessage = this.ErrorMessage;
                this.requiredValidator.ValidationGroup = this.ValidationGroup;

                this.dateValidator = new DateImageValidator();
                this.dateValidator.ControlToValidate = this.txtValue.ID;
                this.dateValidator.ImageUrl = this.ValidationImageUrl;
                this.dateValidator.ErrorMessage = this.ErrorMessage;
                this.dateValidator.ValidationGroup = this.ValidationGroup;

                TableCell cellValidation = new TableCell();
                cellValidation.CssClass = this.ValidationCssClass;
                cellValidation.Controls.Add(this.requiredValidator);
                cellValidation.Controls.Add(this.dateValidator);

                tableRow.Cells.Add(cellValidation);
            }

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
    }
}
