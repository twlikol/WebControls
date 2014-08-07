using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    public abstract class BaseBoundField : System.Web.UI.WebControls.BoundField
    {
        #region public BaseBoundField()
        
        public BaseBoundField()
        {
            this.ItemStyle.Wrap = false;
        }

        #endregion

        #region public override bool Initialize(bool enableSorting, System.Web.UI.Control control)
        
        public override bool Initialize(bool enableSorting, System.Web.UI.Control control)
        {
            this.HeaderStyle.Wrap = false;
            this.ItemStyle.Wrap = false;

            return base.Initialize(enableSorting, control);
        }

        #endregion

        #region public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            if (cellType == DataControlCellType.Header)
            {
                Label lblText = new Label();
                lblText.Text = this.HeaderText;

                Panel panelContainer = new Panel();
                panelContainer.Controls.Add(lblText);

                cell.Controls.Add(panelContainer);
            }
            else
            {
                base.InitializeCell(cell, cellType, rowState, rowIndex);
            }
        }

        #endregion

        #region public object GetValue(Control controlContainer, string dataField)
        
        public object GetValue(Control controlContainer, string dataField)
        {
            object component = DataBinder.GetDataItem(controlContainer);

            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component).Find(dataField, true);

            if (base.DesignMode)
            {
                return "DataBound";
            }

            return propertyDescriptor.GetValue(component);
        }

        #endregion
    }
}
