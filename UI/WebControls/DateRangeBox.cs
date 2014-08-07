using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Likol.Web.Resources;
using System.Web.UI.WebControls;
using Likol.Web.UI.Validation;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:DateBox runat=server></{0}:DateBox>")
    ]
    public class DateRangeBox : CompositeControl
    {
        private DateBox dbStart;
        private DateBox dbEnd;

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

        #region public string StartDateText

        public string StartDateText
        {
            get
            {
                this.EnsureChildControls();

                return this.dbStart.DateText;
            }
            set
            {
                this.EnsureChildControls();

                this.dbStart.DateText = value;
            }
        }

        #endregion

        #region public DateTime StartDate
        
        public DateTime StartDate
        {
            get
            {
                this.EnsureChildControls();

                return this.dbStart.Date;
            }
        }

        #endregion

        #region public string EndDateText

        public string EndDateText
        {
            get
            {
                this.EnsureChildControls();

                return this.dbEnd.DateText;
            }
            set
            {
                this.EnsureChildControls();

                this.dbEnd.DateText = value;
            }
        }

        #endregion

        #region public DateTime EndDate
        
        public DateTime EndDate
        {
            get
            {
                this.EnsureChildControls();

                DateTime dtEnd = this.dbEnd.Date;

                return dtEnd.AddDays(1).AddMinutes(-1);
            }
        }

        #endregion

        #region public string DateRangeText

        public string DateRangeText
        {
            get
            {
                return string.Format("{0}~{1}", this.StartDateText, this.EndDateText);
            }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        public DateRangeBox()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            string imageUrl = this.ResolveClientUrl(this.ImageUrl);

            if (imageUrl == "")
                imageUrl = ResourceManager.GetImageWebResourceUrl(this, "DateBox");

            this.dbStart = new DateBox();
            this.dbStart.ID = "Start";
            this.dbStart.TextBoxCssClass = this.TextBoxCssClass;
            this.dbStart.ButtonCssClass = this.ButtonCssClass;
            this.dbStart.Format = this.Format;
            this.dbStart.ImageUrl = imageUrl;
            this.dbStart.EnableValidation = this.EnableValidation;
            this.dbStart.ValidationGroup = this.ValidationGroup;

            this.dbEnd = new DateBox();
            this.dbEnd.ID = "End";
            this.dbEnd.TextBoxCssClass = this.TextBoxCssClass;
            this.dbEnd.ButtonCssClass = this.ButtonCssClass;
            this.dbEnd.Format = this.Format;
            this.dbEnd.ImageUrl = imageUrl;
            this.dbEnd.EnableValidation = this.EnableValidation;
            this.dbEnd.ValidationGroup = this.ValidationGroup;

            this.dbStart.ReadOnly = this.ReadOnly;
            this.dbEnd.ReadOnly = this.ReadOnly;

            TableCell cellStart = new TableCell();
            cellStart.Controls.Add(this.dbStart);

            TableCell cellSplit = new TableCell();
            cellSplit.Controls.Add(new LiteralControl("～"));

            TableCell cellEnd = new TableCell();
            cellEnd.Controls.Add(this.dbEnd);

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(cellStart);
            tableRow.Cells.Add(cellSplit);
            tableRow.Cells.Add(cellEnd);

            if (this.EnableValidation)
            {
                this.compareValidator = new CompareImageValidator();
                this.compareValidator.ControlToValidate = this.dbEnd.ID;
                this.compareValidator.ControlToCompare = this.dbStart.ID;
                this.compareValidator.Operator = ValidationCompareOperator.GreaterThanEqual;
                this.compareValidator.Type = ValidationDataType.Date;
                this.compareValidator.ImageUrl = this.ValidationImageUrl;
                this.compareValidator.ErrorMessage = this.ErrorMessage;
                this.compareValidator.ValidationGroup = this.ValidationGroup;

                TableCell cellValidation = new TableCell();
                cellValidation.CssClass = this.ValidationCssClass;
                cellValidation.Controls.Add(this.compareValidator);

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
    }
}
