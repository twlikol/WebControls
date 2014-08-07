using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Likol.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing.Design;

namespace Likol.Web.UI.Validation
{
    [
    ToolboxData("<{0}:DialogBoxImageValidator runat=server />")
    ]
    public class DialogBoxImageValidator : BaseValidator
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

        public DialogBoxImageValidator()
        {
            this.Display = ValidatorDisplay.Dynamic;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.DetermineRenderUplevel() && this.EnableClientScript)
            {
                Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "DialogBoxImageValidatorEvaluateIsValid");

                this.RegisterValidScript();

            }

            base.OnPreRender(e);
        }

        protected void RegisterValidScript()
        {

        }

        protected override bool EvaluateIsValid()
        {
            Control control = WebControlUtil.FindControl(this.Page, base.ControlToValidate);

            DialogBox dialogBox = control as DialogBox;

            return true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            CommonValidatorRender.ValidatorRender(this, this.ImageUrl);

            base.Render(writer);
        }
    }
}
