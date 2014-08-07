using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Likol.Web.Resources;
using System.Drawing.Design;

[assembly: WebResource("Likol.Web.Resources.Images.PagerFirst.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerPrev.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerNext.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerLast.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerFirstOver.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerPrevOver.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerNextOver.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerLastOver.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.PagerSplit.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    public abstract class Pager : CompositeControl
    {
        private static readonly object EventPageSizeChanged;
        private static readonly object EventPageIndexChanged;

        #region public event EventHandler PageSizeChanged

        public event EventHandler PageSizeChanged
        {
            add { base.Events.AddHandler(Pager.EventPageSizeChanged, value); }
            remove { base.Events.RemoveHandler(Pager.EventPageSizeChanged, value); }
        }

        #endregion

        #region public event EventHandler PageIndexChanged

        public event EventHandler PageIndexChanged
        {
            add { base.Events.AddHandler(Pager.EventPageIndexChanged, value); }
            remove { base.Events.RemoveHandler(Pager.EventPageIndexChanged, value); }
        }

        #endregion

        #region static Pager()

        static Pager()
        {
            Pager.EventPageSizeChanged = new object();
            Pager.EventPageIndexChanged = new object();
        }

        #endregion

        private ImageButton ibFirst;
        private ImageButton ibPrev;
        private ImageButton ibNext;
        private ImageButton ibLast;

        private System.Web.UI.WebControls.DropDownList ddlPageSize;

        private Label lblTotalRecord;
        private Label lblCurrentPage;
        private Label lblTotalPage;

        #region public string FirstImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string FirstImageUrl
        {
            get { return this.StringProperty("FirstImageUrl"); }
            set { this.ViewState["FirstImageUrl"] = value; }
        }

        #endregion

        #region public string FirstOverImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string FirstOverImageUrl
        {
            get { return this.StringProperty("FirstOverImageUrl"); }
            set { this.ViewState["FirstOverImageUrl"] = value; }
        }

        #endregion

        #region public string PrevImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string PrevImageUrl
        {
            get { return this.StringProperty("PrevImageUrl"); }
            set { this.ViewState["PrevImageUrl"] = value; }
        }

        #endregion

        #region public string PrevOverImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string PrevOverImageUrl
        {
            get { return this.StringProperty("PrevOverImageUrl"); }
            set { this.ViewState["PrevOverImageUrl"] = value; }
        }

        #endregion

        #region public string NextImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string NextImageUrl
        {
            get { return this.StringProperty("NextImageUrl"); }
            set { this.ViewState["NextImageUrl"] = value; }
        }

        #endregion

        #region public string NextOverImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string NextOverImageUrl
        {
            get { return this.StringProperty("NextOverImageUrl"); }
            set { this.ViewState["NextOverImageUrl"] = value; }
        }

        #endregion

        #region public string LastImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string LastImageUrl
        {
            get { return this.StringProperty("LastImageUrl"); }
            set { this.ViewState["LastImageUrl"] = value; }
        }

        #endregion

        #region public string LastOverImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string LastOverImageUrl
        {
            get { return this.StringProperty("LastOverImageUrl"); }
            set { this.ViewState["LastOverImageUrl"] = value; }
        }

        #endregion

        #region public int TotalRecord

        [
        Browsable(false)
        ]
        public virtual int TotalRecord
        {
            set
            {
                this.EnsureChildControls();

                this.lblTotalRecord.Text = value.ToString();
            }
        }

        #endregion

        #region public int PageSize

        public int PageSize
        {
            get
            {
                this.EnsureChildControls();

                return Convert.ToInt16(this.ddlPageSize.SelectedValue);
            }
        }

        #endregion

        #region public int PageSizeSelectedIndex

        public int PageSizeSelectedIndex
        {
            get
            {
                this.EnsureChildControls();

                return this.ddlPageSize.SelectedIndex;
            }
            set
            {
                this.EnsureChildControls();

                this.ddlPageSize.SelectedIndex = value;
            }
        }

        #endregion

        #region protected int PageCount

        protected int PageCount
        {
            get
            {
                this.EnsureChildControls();

                return Convert.ToInt16(this.lblTotalPage.Text);
            }
            set
            {
                this.lblTotalPage.Text = value.ToString();
            }
        }

        #endregion

        #region public int CurrentPage

        public int CurrentPage
        {
            get
            {
                this.EnsureChildControls();

                if (this.lblCurrentPage.Text == "")
                    this.lblCurrentPage.Text = "1";

                return Convert.ToInt16(this.lblCurrentPage.Text);
            }
            set
            {
                this.lblCurrentPage.Text = value.ToString();
            }
        }

        #endregion

        #region public string TotalRecordText

        [
        DefaultValue("Total Record"),
        Category("Likol")
        ]
        public string TotalRecordText
        {
            get { return this.StringProperty("TotalRecordText", "Total Record"); }
            set { this.ViewState["TotalRecordText"] = value; }
        }

        #endregion

        #region public string RecordText

        [
        DefaultValue("Record"),
        Category("Likol")
        ]
        public string RecordText
        {
            get { return this.StringProperty("RecordText", "Record"); }
            set { this.ViewState["RecordText"] = value; }
        }

        #endregion

        #region public string PageText

        [
        DefaultValue("Page"),
        Category("Likol")
        ]
        public string PageText
        {
            get { return this.StringProperty("PageText", "Page"); }
            set { this.ViewState["PageText"] = value; }
        }

        #endregion

        #region public string PagesText

        [
        DefaultValue("Pages"),
        Category("Likol")
        ]
        public string PagesText
        {
            get { return this.StringProperty("PagesText", "Pages"); }
            set { this.ViewState["PagesText"] = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        #endregion

        #region protected void OnPagerButton_Click(object sender, ImageClickEventArgs e)

        protected void OnPagerButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibPager = sender as ImageButton;

            if (ibPager == null) return;

            switch (ibPager.CommandName)
            {
                case "First":

                    this.CurrentPage = 1;
                    break;

                case "Prev":

                    if (this.CurrentPage > 1) this.CurrentPage--;
                    break;

                case "Next":

                    if (this.CurrentPage < this.PageCount) this.CurrentPage++;
                    break;

                case "Last":

                    this.CurrentPage = this.PageCount;
                    break;
            }

            this.OnPageIndexChanged(e);
        }

        #endregion

        #region protected void OnPageSize_Changed(object sender, EventArgs e)

        protected void OnPageSize_Changed(object sender, EventArgs e)
        {
            this.OnPageSizeChanged(e);
        }

        #endregion

        #region protected virtual void OnPageSizeChanged(EventArgs e)
        
        protected virtual void OnPageSizeChanged(EventArgs e)
        {
            EventHandler eventHandler = (EventHandler)base.Events[Pager.EventPageSizeChanged];

            if (eventHandler != null) eventHandler(this, e);

            this.OnPageIndexChanged(e);
        }

        #endregion

        #region protected virtual void OnPageIndexChanged(EventArgs e)

        protected virtual void OnPageIndexChanged(EventArgs e)
        {
            EventHandler eventHandler = (EventHandler)base.Events[Pager.EventPageIndexChanged];

            if (eventHandler != null) eventHandler(this, e);
        }

        #endregion

        #region protected override void CreateChildControls()

        protected override void CreateChildControls()
        {
            TableCell buttonCell = new TableCell();
            this.CreatePagerButton(buttonCell);

            TableCell statusCell = new TableCell();
            statusCell.HorizontalAlign = HorizontalAlign.Right;
            statusCell.Style["padding-top"] = "1px";
            statusCell.Style["padding-right"] = "10px";

            this.lblTotalRecord = new Label();
            this.lblCurrentPage = new Label();
            this.lblTotalPage = new Label();

            statusCell.Controls.Add(new LiteralControl(string.Format("{0}： ", this.TotalRecordText)));

            statusCell.Controls.Add(this.lblTotalRecord);

            statusCell.Controls.Add(new LiteralControl(string.Format(" {0}　", this.RecordText)));
            statusCell.Controls.Add(new LiteralControl(string.Format("{0}： ", this.PagesText)));

            statusCell.Controls.Add(this.lblCurrentPage);

            statusCell.Controls.Add(new LiteralControl(" / "));

            statusCell.Controls.Add(this.lblTotalPage);

            statusCell.Controls.Add(new LiteralControl(string.Format(" {0} ", this.PageText)));

            TableRow mainRow = new TableRow();
            mainRow.Cells.Add(buttonCell);
            mainRow.Cells.Add(statusCell);

            this.Controls.Add(mainRow);

            this.Attributes["cellpadding"] = "0";
            this.Attributes["cellspacing"] = "0";
        }

        #endregion

        #region private void CreatePagerButton(TableCell buttonCell)

        private void CreatePagerButton(TableCell buttonCell)
        {
            this.ibFirst = new ImageButton();
            this.ibPrev = new ImageButton();
            this.ibNext = new ImageButton();
            this.ibLast = new ImageButton();

            this.ibFirst.CommandName = "First";
            this.ibPrev.CommandName = "Prev";
            this.ibNext.CommandName = "Next";
            this.ibLast.CommandName = "Last";

            this.ibFirst.ImageAlign = ImageAlign.AbsMiddle;
            this.ibPrev.ImageAlign = ImageAlign.AbsMiddle;
            this.ibNext.ImageAlign = ImageAlign.AbsMiddle;
            this.ibLast.ImageAlign = ImageAlign.AbsMiddle;

            this.ibFirst.Click += new ImageClickEventHandler(OnPagerButton_Click);
            this.ibPrev.Click += new ImageClickEventHandler(OnPagerButton_Click);
            this.ibNext.Click += new ImageClickEventHandler(OnPagerButton_Click);
            this.ibLast.Click += new ImageClickEventHandler(OnPagerButton_Click);

            if (this.FirstImageUrl != "")
            {
                this.ibFirst.ImageUrl = this.FirstImageUrl;
                this.ibFirst.HoverImageUrl = this.FirstOverImageUrl;
            }
            else
            {
                this.ibFirst.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerFirst");
                this.ibFirst.HoverImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerFirstOver");
            }

            if (this.PrevImageUrl != "")
            {
                this.ibPrev.ImageUrl = this.PrevImageUrl;
                this.ibPrev.HoverImageUrl = this.PrevOverImageUrl;
            }
            else
            {
                this.ibPrev.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerPrev");
                this.ibPrev.HoverImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerPrevOver");
            }

            if (this.NextImageUrl != "")
            {
                this.ibNext.ImageUrl = this.NextImageUrl;
                this.ibNext.HoverImageUrl = this.NextOverImageUrl;
            }
            else
            {
                this.ibNext.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerNext");
                this.ibNext.HoverImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerNextOver");
            }

            if (this.LastImageUrl != "")
            {
                this.ibLast.ImageUrl = this.LastImageUrl;
                this.ibLast.HoverImageUrl = this.LastOverImageUrl;
            }
            else
            {
                this.ibLast.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerLast");
                this.ibLast.HoverImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerLastOver");
            }

            this.InsertPagerControl(buttonCell, this.ibFirst);
            this.InsertPagerControl(buttonCell, this.ibPrev);
            this.InsertPagerControl(buttonCell, this.ibNext);
            this.InsertPagerControl(buttonCell, this.ibLast);

            this.InsertPagerButtonSplit(buttonCell);

            this.ddlPageSize = new System.Web.UI.WebControls.DropDownList();
            this.ddlPageSize.AutoPostBack = true;

            this.ddlPageSize.SelectedIndexChanged += new EventHandler(OnPageSize_Changed);

            ddlPageSize.Items.Add(new ListItem(string.Format("5 {0}", this.RecordText), "5"));
            ddlPageSize.Items.Add(new ListItem(string.Format("15 {0}", this.RecordText), "15"));
            ddlPageSize.Items.Add(new ListItem(string.Format("20 {0}", this.RecordText), "20"));
            ddlPageSize.Items.Add(new ListItem(string.Format("30 {0}", this.RecordText), "30"));
            ddlPageSize.Items.Add(new ListItem(string.Format("50 {0}", this.RecordText), "50"));

            this.ddlPageSize.SelectedIndex = 1;

            this.InsertPagerControl(buttonCell, this.ddlPageSize);
        }

        #endregion

        #region private void InsertPagerButtonSplit(TableCell buttonCell)

        private void InsertPagerButtonSplit(TableCell buttonCell)
        {
            Image imgSplit = new Image();
            imgSplit.ImageAlign = ImageAlign.AbsMiddle;
            imgSplit.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "PagerSplit");

            this.InsertPagerControl(buttonCell, imgSplit);
        }

        #endregion

        #region private void InsertPagerControl(TableCell buttonCell, Control control)

        private void InsertPagerControl(TableCell buttonCell, Control control)
        {
            buttonCell.Controls.Add(control);
            buttonCell.Controls.Add(new LiteralControl("\n"));
        }

        #endregion

        private class ImageButton : System.Web.UI.WebControls.ImageButton
        {
            #region public string HoverImageUrl

            [
            DefaultValue(""),
            Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
            ]
            public string HoverImageUrl
            {
                get
                {
                    object value = this.ViewState["HoverImageUrl"];

                    if (value != null)
                    {
                        return (string)value;
                    }

                    return string.Empty;
                }
                set { this.ViewState["HoverImageUrl"] = value; }
            }

            #endregion

            public ImageButton()
            {
                this.Width = 19;
                this.Height = 19;
            }

            #region protected override void AddAttributesToRender(HtmlTextWriter writer)

            protected override void AddAttributesToRender(HtmlTextWriter writer)
            {
                base.AddAttributesToRender(writer);

                if (this.HoverImageUrl != "")
                {
                    writer.AddAttribute("onmouseover", string.Format("this.src='{0}'", this.ResolveUrl(this.HoverImageUrl)));
                    writer.AddAttribute("onmouseout", string.Format("this.src='{0}'", this.ResolveUrl(this.ImageUrl)));
                }
            }

            #endregion
        }
    }
}