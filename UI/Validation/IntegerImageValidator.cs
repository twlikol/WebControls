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
    ToolboxData("<{0}:IntegerImageValidator runat=server />")
    ]
    public class IntegerImageValidator : CompareImageValidator
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

        public IntegerImageValidator() : base()
        {
            base.Type = ValidationDataType.Integer;
            base.Operator = ValidationCompareOperator.DataTypeCheck;
        }
    }
}
