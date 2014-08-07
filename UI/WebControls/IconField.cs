using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    [
    ParseChildren(true, "Rules"),
    DefaultProperty("Rules")
    ]
    public class IconField : BaseBoundField
    {
        #region public IconFieldRuleCollection Rules
        
        private IconFieldRuleCollection rules;

        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null)
        ]
        public IconFieldRuleCollection Rules
        {
            get
            {
                if (this.rules == null)
                    this.rules = new IconFieldRuleCollection();

                return this.rules;
            }
        }

        #endregion

        protected override DataControlField CreateField()
        {
            return new IconField();
        }

        #region protected override void InitializeDataCell(DataControlFieldCell dataControlFieldCell, DataControlRowState dataControlRowState)

        protected override void InitializeDataCell(DataControlFieldCell dataControlFieldCell, DataControlRowState dataControlRowState)
        {
            Image imgIcon = new Image();
            imgIcon.ImageAlign = ImageAlign.AbsMiddle;

            Panel panel = new Panel();
            panel.Controls.Add(imgIcon);

            dataControlFieldCell.Controls.Add(panel);

            if (base.Visible)
            {
                imgIcon.DataBinding += new EventHandler(this.OnDataBindField);
            }
        }

        #endregion

        #region protected override void OnDataBindField(object sender, EventArgs e)

        protected override void OnDataBindField(object sender, EventArgs e)
        {
            Image imgIcon = (Image)sender;

            object dataValue = 0;
            string value = "";

            if (!this.DesignMode)
            {
                Control namingContainer = imgIcon.NamingContainer;

                dataValue = this.GetValue(namingContainer);

                if (dataValue != null && dataValue.GetType() != typeof(DBNull))
                {
                    value = dataValue.ToString();
                }
            }

            IconFieldRule iconFieldRule = this.Rules[value];

            if (iconFieldRule != null)
            {
                imgIcon.ImageUrl = iconFieldRule.ImageUrl;
            }
        }

        #endregion
    }
}
