using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    public class DateTimeField : BaseBoundField
    {
        public string FormatString { get; set; }

        public DateTimeField()
        {
            this.FormatString = "yyyy/MM/dd hh:mm:ss";
        }

        protected override DataControlField CreateField()
        {
            return new DateTimeField();
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

            DateTime dataValue;
            string value = "";

            if (!this.DesignMode)
            {
                Control namingContainer = lblText.NamingContainer;

                object objectValue = this.GetValue(namingContainer);

                if (objectValue.GetType() != typeof(DBNull) && objectValue != null)
                {
                    dataValue = (DateTime)this.GetValue(namingContainer);

                    if (dataValue != DateTime.MinValue)
                    {
                        value = dataValue.ToString(this.FormatString);
                    }
                }
            }

            lblText.Text = value;
        }

        #endregion
    }
}
