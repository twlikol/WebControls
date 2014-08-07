using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    [
    ParseChildren(true, "Rules"),
    DefaultProperty("Rules")
    ]
    public class TextField : BaseBoundField
    {
        #region public TextFieldRuleCollection Rules

        private TextFieldRuleCollection rules;

        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null)
        ]
        public TextFieldRuleCollection Rules
        {
            get
            {
                if (this.rules == null)
                    this.rules = new TextFieldRuleCollection();

                return this.rules;
            }
        }

        #endregion

        protected override DataControlField CreateField()
        {
            return new TextField();
        }

        #region protected override void InitializeDataCell(DataControlFieldCell dataControlFieldCell, DataControlRowState dataControlRowState)

        protected override void InitializeDataCell(DataControlFieldCell dataControlFieldCell, DataControlRowState dataControlRowState)
        {
            Label lblText = new Label();

            Panel panel = new Panel();
            panel.Controls.Add(lblText);

            dataControlFieldCell.Controls.Add(panel);

            if (base.Visible)
            {
                lblText.DataBinding += new EventHandler(this.OnDataBindField);
            }
        }

        #endregion

        #region protected override void OnDataBindField(object sender, EventArgs e)

        protected override void OnDataBindField(object sender, EventArgs e)
        {
            Label lblText = (Label)sender;

            object dataValue = null;
            string value = "";

            if (!this.DesignMode)
            {
                Control namingContainer = lblText.NamingContainer;

                dataValue = this.GetValue(namingContainer);

                if (dataValue != null && dataValue.GetType() != typeof(DBNull))
                {
                    value = dataValue.ToString();
                }
            }
            else
            {
                value = "DataBound";
            }

            if (this.Rules == null || this.Rules.Count == 0)
            {
                lblText.Text = value;

                return;
            }

            TextFieldRule textFieldRule = this.Rules[value];

            if (textFieldRule != null)
            {
                lblText.Text = textFieldRule.Text;

                if (textFieldRule.Color != System.Drawing.Color.Black)
                    lblText.ForeColor = textFieldRule.Color;
            }
        }

        #endregion
    }
}
