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
    ToolboxData("<{0}:MultiItemBox runat=server></{0}:MultiItemBox>")
    ]
    public class MultiItemBox : CompositeControl
    {
        #region public string Text
        
        [Browsable(false)]
        public string Text
        {
            get { return this.StringProperty("Text"); }
            set { this.ViewState["Text"] = value; }
        }

        #endregion

        #region public string Value

        [Browsable(false)]
        public string Value
        {
            get { return this.StringProperty("Value"); }
            set { this.ViewState["Value"] = value; }
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

        #region public string ItemCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ItemCssClass
        {
            get { return this.StringProperty("ItemCssClass"); }
            set { this.ViewState["ItemCssClass"] = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

        public MultiItemBox()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (this.Value != "")
            {
                string[] values = this.Value.Split(';');
                string[] texts = this.Text.Split(';');

                for (int i = 0; i < values.Length; i++)
                {
                    TableCell cellText = new TableCell();
                    cellText.Style["padding-left"] = "2px";
                    cellText.Text = texts[i];

                    System.Web.UI.WebControls.ImageButton ibDelete = new System.Web.UI.WebControls.ImageButton();
                    ibDelete.ImageAlign = ImageAlign.AbsMiddle;
                    ibDelete.ImageUrl = this.ImageUrl;
                    ibDelete.CommandArgument = i.ToString();

                    ibDelete.Click += new ImageClickEventHandler(ibDelete_Click);

                    TableCell cellDelete = new TableCell();
                    cellDelete.Style["padding-left"] = "2px";
                    cellDelete.Controls.Add(ibDelete);

                    TableRow tableRow = new TableRow();
                    tableRow.Cells.Add(cellText);
                    tableRow.Cells.Add(cellDelete);

                    Table table = new Table();
                    table.CellSpacing = 0;
                    table.CellPadding = 0;
                    table.GridLines = GridLines.None;
                    table.Rows.Add(tableRow);

                    Panel panel = new Panel();
                    panel.CssClass = this.ItemCssClass;
                    panel.Controls.Add(table);

                    this.Controls.Add(panel);
                }
            }

            Panel panelContainer = new Panel();
            panelContainer.Style["clear"] = "both";

            this.Controls.Add(panelContainer);
        }

        #endregion

        #region protected void ibDelete_Click(object sender, ImageClickEventArgs e)

        protected void ibDelete_Click(object sender, ImageClickEventArgs e)
        {
            int valueIndex = Convert.ToInt32(((System.Web.UI.WebControls.ImageButton)sender).CommandArgument);

            string[] values = this.Value.Split(';');
            string[] texts = this.Text.Split(';');

            string valueString = "";
            string textString = "";

            for (int i = 0; i < values.Length; i++)
            {
                if (i != valueIndex)
                {
                    valueString += values[i] + ";";
                    textString += texts[i] + ";";
                }
            }

            if (valueString != "")
            {
                valueString = valueString.Substring(0, valueString.Length - 1);
                textString = textString.Substring(0, textString.Length - 1);
            }

            this.Value = valueString;
            this.Text = textString;

            this.ChildControlsCreated = false;
        }

        #endregion

        #region public void AddItem(string text, string value)
        
        public void AddItem(string text, string value)
        {
            string[] values = this.Value.Split(';');

            foreach (string value1 in values)
            {
                if (value == value1) return;
            }

            if (this.Text == "")
            {
                this.Text = text;
                this.Value = value;
            }
            else
            {
                this.Text += ";" + text;
                this.Value += ";" + value;
            }

            this.ChildControlsCreated = false;
        }

        #endregion
    }
}
