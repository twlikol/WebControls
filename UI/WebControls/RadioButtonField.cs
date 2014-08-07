using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    public class RadioButtonField : DataControlField
    {
        private string GridViewClientID;

        #region public string Key

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string Key
        {
            get
            {
                object value = this.ViewState["Key"];

                if (value != null)
                {
                    return (string)value;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Key"] = value;
            }
        }

        #endregion

        #region protected override DataControlField CreateField()
        
        protected override DataControlField CreateField()
        {
            return new RadioButtonField();
        }

        #endregion

        #region public override bool Initialize(bool sortingEnabled, Control control)
        
        public override bool Initialize(bool sortingEnabled, Control control)
        {
            this.HeaderStyle.Wrap = false;
            this.ItemStyle.Wrap = false;

            this.GridViewClientID = control.ClientID;

            return base.Initialize(sortingEnabled, control);
        }

        #endregion

        #region public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            cell.HorizontalAlign = HorizontalAlign.Center;

            Panel panel = new Panel();
            
            if (cellType == DataControlCellType.Header)
            {
                Label lblHeader = new Label();
                lblHeader.Text = this.HeaderText;

                panel.Controls.Add(lblHeader);

                string clientScript = "";
                clientScript += "\n" + string.Format("function RadioButtonField_{0}_RadioButton(control){{", this.Key);
                clientScript += "\n" + string.Format("var table = document.getElementById('{0}');", this.GridViewClientID);
                clientScript += "\n" + "var inputs = table.getElementsByTagName('input');";
                clientScript += "\n" + "for(var i = 0; i < inputs.length; i++){";
                clientScript += "\n" + string.Format("if (inputs[i].type == 'radio' && inputs[i].value == '{0}'){{", this.Key);
                clientScript += "\n" + "if (inputs[i].id != control.id) {inputs[i].checked = false;}";
                clientScript += "\n" + "}}}";

                ScriptManager.RegisterClientScriptBlock(this.Control.Page, typeof(RadioButtonField),
                    "Checked", clientScript, true);
            }
            else if ((rowState == DataControlRowState.Normal || rowState == DataControlRowState.Alternate)
                && cellType == DataControlCellType.DataCell)
            {
                RadioButton rbSelect = new RadioButton();
                rbSelect.ID = this.Key;
                rbSelect.InputAttributes["onclick"] = string.Format("RadioButtonField_{0}_RadioButton(this);", this.Key);

                panel.Controls.Add(rbSelect);
            }

            cell.Controls.Add(panel);
        }

        #endregion

        #region public static int GetCheckedIndex(GridView gridView, int columnIndex)

        public static int GetCheckedIndex(GridView gridView, int columnIndex)
        {
            foreach (GridViewRow gridViewRow in gridView.Rows)
            {
                if (gridViewRow.RowType != DataControlRowType.DataRow) continue;

                TableCell tableCell = gridViewRow.Cells[columnIndex];

                RadioButton rbSelect = (RadioButton)tableCell.Controls[0].Controls[0];

                if (rbSelect.Checked) return gridViewRow.RowIndex;
            }

            return -1;
        }

        #endregion
    }
}
