using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:RepeaterPager runat=server></{0}:RepeaterPager>")
    ]
    public class RepeaterPager : Pager
    {
        private bool initialized;
        private bool currentViewValid;
        private bool requiresDataBinding;

        private DataSourceView currentView;

        #region public string RepeaterID

        public string RepeaterID
        {
            get { return this.StringProperty("RepeaterID"); }
            set { this.ViewState["RepeaterID"] = value; }
        }

        #endregion

        #region private Repeater RepeaterControl

        private Repeater RepeaterControl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.RepeaterID))
                {
                    try
                    {
                        Control control = WebControlUtil.FindControl(this, this.RepeaterID);

                        if (control != null) return (Repeater)control;
                    }
                    catch
                    {
                    }
                }
                return null;
            }
        }

        #endregion

        #region public string DataSourceID

        public string DataSourceID
        {
            get
            {
                return this.StringProperty("DataSourceID");
            }
            set
            {
                this.ViewState["DataSourceID"] = value;

                this.OnDataPropertyChanged();
            }
        }

        #endregion

        #region private IDataSource DataSource

        private IDataSource DataSource
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DataSourceID))
                {
                    try
                    {
                        Control control = WebControlUtil.FindControl(this, this.DataSourceID);

                        if (control != null) return (IDataSource)control;
                    }
                    catch
                    {
                    }
                }
                return null;
            }
        }

        #endregion

        #region public string DataMember

        public string DataMember
        {
            get { return this.StringProperty("DataMember"); }
            set { this.ViewState["DataMember"] = value; }
        }

        #endregion

        #region public int TotalRecord

        [
        Browsable(false)
        ]
        public override int TotalRecord
        {
            set
            {
                base.TotalRecord = value;

                if (value != 0)
                {
                    int pageCount = value / this.PageSize;

                    if ((value % this.PageSize) != 0)
                        pageCount = pageCount + 1;

                    this.PageCount = pageCount;
                }
            }
        }

        #endregion

        public RepeaterPager()
        {
            Global.TimeIsValid();
        }

        protected void OnDataPropertyChanged()
        {
            if (this.initialized)
            {
                this.requiresDataBinding = true;
            }

            this.currentViewValid = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.initialized = true;

            this.ConnectToDataSourceView();

            if (!this.Page.IsPostBack)
            {
                this.requiresDataBinding = true;
            }
        }

        private void ConnectToDataSourceView()
        {
            if (!this.currentViewValid)
            {
                IDataSource dataSource = this.DataSource;

                if (dataSource == null) return;

                DataSourceView dataSourceView = dataSource.GetView(this.DataMember);

                this.currentView = dataSourceView;
                this.currentView.DataSourceViewChanged += new EventHandler(this.OnDataSourceViewChanged);

                this.currentViewValid = true;
            }
        }

        protected void OnDataSourceViewChanged(object sender, EventArgs e)
        {
            this.requiresDataBinding = true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.requiresDataBinding)
                this.DataBind();
        }

        public override void DataBind()
        {
            DataSourceView dataSourceView = this.DataSource.GetView(this.DataMember);
            dataSourceView.Select(this.CreateDataSourceSelectArguments(dataSourceView), new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
        }

        private DataSourceSelectArguments CreateDataSourceSelectArguments(DataSourceView dataSourceView)
        {
            DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();

            bool canPage = dataSourceView.CanPage;

            if (canPage)
            {
                if (dataSourceView.CanRetrieveTotalRowCount)
                {
                    dataSourceSelectArguments.RetrieveTotalRowCount = true;
                    dataSourceSelectArguments.MaximumRows = this.PageSize;
                }
                else
                {
                    dataSourceSelectArguments.MaximumRows = -1;
                }

                dataSourceSelectArguments.StartRowIndex = this.PageSize * (this.CurrentPage - 1);
            }

            return dataSourceSelectArguments;
        }

        protected override void OnPageIndexChanged(EventArgs e)
        {
            base.OnPageIndexChanged(e);

            this.requiresDataBinding = true;
        }

        protected override void OnPageSizeChanged(EventArgs e)
        {
            this.CurrentPage = 1;

            base.OnPageSizeChanged(e);

            this.requiresDataBinding = true;
        }

        private void OnDataSourceViewSelectCallback(IEnumerable data)
        {
            this.RepeaterControl.DataSource = data;
            this.RepeaterControl.DataBind();
        }
    }
}
