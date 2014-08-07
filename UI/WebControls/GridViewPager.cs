using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Likol.Web.Resources;
using System.Drawing.Design;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:GridViewPager runat=server></{0}:GridViewPager>")
    ]
    public class GridViewPager : Pager
    {
        #region public string GridViewID

        public string GridViewID
        {
            get { return this.StringProperty("GridViewID"); }
            set { this.ViewState["GridViewID"] = value; }
        }

        #endregion

        #region private GridView GridViewControl

        private GridView GridViewControl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GridViewID))
                {
                    try
                    {
                        Control control = WebControlUtil.FindControl(this, this.GridViewID);

                        if (control != null) return (GridView)control;
                    }
                    catch
                    {
                    }
                }
                return null;
            }
        }

        #endregion

        public GridViewPager()
        {
            Global.TimeIsValid();
        }

        #region protected override void OnInit(EventArgs e)

        protected override void OnInit(EventArgs e)
        {
            if (this.GridViewControl != null)
            {
                this.EnsureChildControls();

                if (this.DesignMode)
                    this.GridViewControl.PageSize = 5;
                else
                    this.GridViewControl.PageSize = this.PageSize;

                this.GridViewControl.AllowPaging = true;
                this.GridViewControl.PagerSettings.Visible = false;

                this.GridViewControl.DataBound += new EventHandler(OnGridView_DataBound);
            }
        }

        #endregion

        #region protected void OnGridView_DataBound(object sender, EventArgs e)

        protected void OnGridView_DataBound(object sender, EventArgs e)
        {
            this.PageCount = this.GridViewControl.PageCount;

            int currentPage = this.GridViewControl.PageIndex + 1;

            if (this.PageCount == 0)
                currentPage = 0;

            this.CurrentPage = currentPage;
        }

        #endregion

        #region protected override void OnPageSizeChanged(EventArgs e)
        
        protected override void OnPageSizeChanged(EventArgs e)
        {
            if (this.GridViewControl != null)
            {
                this.GridViewControl.PageSize = this.PageSize;
                this.GridViewControl.PageIndex = 0;
            }

            base.OnPageSizeChanged(e);
        }

        #endregion

        #region protected override void OnPageIndexChanged(EventArgs e)

        protected override void OnPageIndexChanged(EventArgs e)
        {
            this.GridViewControl.PageIndex = this.CurrentPage - 1;

            base.OnPageIndexChanged(e);
        }

        #endregion
    }
}
