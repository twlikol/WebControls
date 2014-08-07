using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    public class LinkButtonField : BaseBoundField
    {
        public string CommandName { get; set; }
        public string CommandArgumentField { get; set; }

        protected override DataControlField CreateField()
        {
            return new LinkButtonField();
        }

        #region protected override void InitializeDataCell(DataControlFieldCell dataControlFieldCell, DataControlRowState dataControlRowState)

        protected override void InitializeDataCell(DataControlFieldCell dataControlFieldCell, DataControlRowState dataControlRowState)
        {
            LinkButton lbText = new LinkButton();
            lbText.CommandName = this.CommandName;

            Panel panel = new Panel();
            panel.Controls.Add(lbText);

            dataControlFieldCell.Controls.Add(panel);

            if (base.Visible)
            {
                lbText.DataBinding += new EventHandler(this.OnDataBindField);
            }
        }

        #endregion

        #region protected override void OnDataBindField(object sender, EventArgs e)

        protected override void OnDataBindField(object sender, EventArgs e)
        {
            LinkButton lbText = (LinkButton)sender;
            
            object dataValue = null;
            string value = "";

            if (!this.DesignMode)
            {
                Control namingContainer = lbText.NamingContainer;

                dataValue = this.GetValue(namingContainer);

                if (dataValue != null && dataValue.GetType() != typeof(DBNull))
                {
                    value = dataValue.ToString();
                }

                lbText.CommandArgument = this.GetValue(namingContainer, this.CommandArgumentField).ToString();
            }
            else
            {
                value = "DataBound";
            }

            lbText.Text = value;
        }

        #endregion
    }
}
