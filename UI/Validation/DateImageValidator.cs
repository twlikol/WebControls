using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Likol.Web.UI.Validation
{
    [
    ToolboxData("<{0}:DateImageValidator runat=server />")
    ]
    public class DateImageValidator : CompareImageValidator
    {
        [
        Browsable(false)
        ]
        public new ValidationDataType Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }

        [
        Browsable(false)
        ]
        public new ValidationCompareOperator Operator
        {
            get { return base.Operator; }
            set { base.Operator = value; }
        }

        [
        Browsable(false)
        ]
        public new string ControlToCompare
        {
            get { return base.ControlToCompare; }
            set { base.ControlToCompare = value; }
        }

        public DateImageValidator()
            : base()
        {
            base.Type = ValidationDataType.Date;
            base.Operator = ValidationCompareOperator.DataTypeCheck;
        }
    }
}
