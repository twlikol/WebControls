using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;

namespace Likol.Web.UI.WebControls
{
    public class CheckBoxField : DataControlField
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
            return new CheckBoxField();
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
            cell.Width = 20;
            cell.HorizontalAlign = HorizontalAlign.Center;

            CheckBox cbSelect = new CheckBox();

            Panel panel = new Panel();
            panel.Width = 20;
            panel.Controls.Add(cbSelect);

            cell.Controls.Add(panel);

            if (cellType == DataControlCellType.Header)
            {
                cbSelect.ID = string.Format("Header_{0}", this.Key);
                cbSelect.InputAttributes["onclick"] = string.Format("CheckBoxField_{0}(this);", cbSelect.ID);

                string clientScript = "";
                clientScript += "\n" + string.Format("function CheckBoxField_{0}(control){{", cbSelect.ID);
                clientScript += "\n" + "var checked = control.checked;";
                clientScript += "\n" + string.Format("var table = document.getElementById('{0}');", this.GridViewClientID);
                clientScript += "\n" + "var inputs = table.getElementsByTagName('input')";
                clientScript += "\n" + "for(var i = 0; i < inputs.length; i++){";
                clientScript += "\n" + string.Format("if (inputs[i].type == 'checkbox' && inputs[i].value == '{0}'){{", this.Key);
                clientScript += "\n" + "inputs[i].checked = checked;}}}";

                clientScript += "\n" + string.Format("function CheckBoxField_{0}_CheckBox(control){{", this.Key);
                clientScript += "\n" + "var checked = control.checked;";
                clientScript += "\n" + string.Format("var table = document.getElementById('{0}');", this.GridViewClientID);
                clientScript += "\n" + "var inputs = table.getElementsByTagName('input');";
                clientScript += "\n" + "var checkedAll = true;";
                clientScript += "\n" + "if (checked){";
                clientScript += "\n" + "for(var i = 0; i < inputs.length; i++){";
                clientScript += "\n" + string.Format("if (inputs[i].type == 'checkbox' && inputs[i].value == '{0}'){{", this.Key);
                clientScript += "\n" + "if (inputs[i].checked) {continue;} else { checkedAll = false; break;}}}";
                clientScript += "\n" + "} else { checkedAll = false; }";
                clientScript += "\n" + string.Format("var headerBoxID = '{0}_{1}';", this.GridViewClientID, cbSelect.ClientID);
                clientScript += "\n" + "var headerBox = document.getElementById(headerBoxID);";
                clientScript += "\n" + "headerBox.checked = checkedAll;}";

                ScriptManager.RegisterClientScriptBlock(this.Control.Page, typeof(CheckBoxField),
                    "Checked", clientScript, true);
            }
            else if ((rowState == DataControlRowState.Normal || rowState == DataControlRowState.Alternate)
                && cellType == DataControlCellType.DataCell)
            {
                cbSelect.InputAttributes["value"] = this.Key;
                cbSelect.InputAttributes["onclick"] = string.Format("CheckBoxField_{0}_CheckBox(this);", this.Key);
            }
        }

        #endregion

        #region public static bool HasChecked(GridView gridView, GridViewRow gridViewRow, int columnIndex)
        
        public static bool HasChecked(GridView gridView, GridViewRow gridViewRow, int columnIndex)
        {
            CheckBoxField checkBoxField = (CheckBoxField)gridView.Columns[columnIndex];
            TableCell tableCell = gridViewRow.Cells[columnIndex];

            CheckBox cbSelect = (CheckBox)tableCell.Controls[0].Controls[0];

            return cbSelect.Checked;
        }

        #endregion
    }
}
