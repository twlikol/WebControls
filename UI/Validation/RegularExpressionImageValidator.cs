using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing.Design;

namespace Likol.Web.UI.Validation
{
    [
    ToolboxData("<{0}:RegularExpressionImageValidator runat=server />")
    ]
    public class RegularExpressionImageValidator : RegularExpressionValidator
    {
        [
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor)),
        DefaultValue(""),
        Category("Likol")
        ]
        public string ImageUrl
        {
            get
            {
                object value = this.ViewState["ImageUrl"];
                if (value != null)
                {
                    return (string)value;
                }
                return string.Empty;
            }
            set { this.ViewState["ImageUrl"] = value; }
        }

        [
        DefaultValue(ValidatorDisplay.Dynamic),
        Category("Likol")
        ]
        public new ValidatorDisplay Display
        {
            get { return base.Display; }
            set { base.Display = value; }
        }

        [
        Browsable(false)
        ]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public RegularExpressionImageValidator()
        {
            this.Display = ValidatorDisplay.Dynamic;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            CommonValidatorRender.ValidatorRender(this, this.ImageUrl);

            base.Render(writer);
        }
    }
}
