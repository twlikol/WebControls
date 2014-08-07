using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.WindowPanel.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.WindowPanelClose.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.WindowPanelMax.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.WindowPanelMin.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:WindowPanel runat=server></{0}:WindowPanel>"),
    Designer("Likol.Design.Web.UI.WebControls.WindowPanelDesigner")
    ]
    public class WindowPanel : CompositeControl, IScriptControl
    {
        private static readonly object EventClose;
        private static readonly object EventRefresh;

        #region public event EventHandler Refresh

        [
        Category("Likol")
        ]
        public event EventHandler Refresh
        {
            add { base.Events.AddHandler(WindowPanel.EventRefresh, value); }
            remove { base.Events.RemoveHandler(WindowPanel.EventRefresh, value); }
        }

        #endregion

        #region public event EventHandler Close

        [
        Category("Likol")
        ]
        public event EventHandler Close
        {
            add { base.Events.AddHandler(WindowPanel.EventClose, value); }
            remove { base.Events.RemoveHandler(WindowPanel.EventClose, value); }
        }

        #endregion

        #region static WindowPanel()

        static WindowPanel()
        {
            WindowPanel.EventRefresh = new object();
            WindowPanel.EventClose = new object();
        }

        #endregion

        #region public string Title

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string Title
        {
            get { return this.StringProperty("Title"); }
            set { this.ViewState["Title"] = value; }
        }

        #endregion

        #region public string ControlPath

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string ControlPath
        {
            get { return this.StringProperty("ControlPath"); }
            set { this.ViewState["ControlPath"] = value; }
        }

        #endregion

        #region public int HeightFix

        [
        DefaultValue(4),
        Category("Likol")
        ]
        public int HeightFix
        {
            get { return this.IntProperty("HeightFix", 4); }
            set { this.ViewState["HeightFix"] = value; }
        }

        #endregion

        #region public int ZIndex

        [
        DefaultValue(100),
        Category("Likol")
        ]
        public int ZIndex
        {
            get { return this.IntProperty("ZIndex", 100); }
            set { this.ViewState["ZIndex"] = value; }
        }

        #endregion

        #region public string HeaderCssClass

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string HeaderCssClass
        {
            get { return this.StringProperty("HeaderCssClass"); }
            set { this.ViewState["HeaderCssClass"] = value; }
        }

        #endregion

        #region public string ContentCssClass

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string ContentCssClass
        {
            get { return this.StringProperty("ContentCssClass"); }
            set { this.ViewState["ContentCssClass"] = value; }
        }

        #endregion

        #region public override Unit Width
        
        [DefaultValue(640)]
        public override Unit Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        #endregion

        #region public override Unit Height
        
        [DefaultValue(480)]
        public override Unit Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

        #endregion

        #region public override bool Visible

        [
        DefaultValue(false)
        ]
        public override bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        #endregion

        private object dataObject = null;

        #region public object DataObject
        
        public object DataObject
        {
            set { this.dataObject = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        #endregion

        #region public WindowPanel()
        
        public WindowPanel()
        {
            this.Width = 640;
            this.Height = 480;

            this.Visible = false;

            Global.TimeIsValid();
        }

        #endregion

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (!this.Visible) return;

            Label lblTitle = new Label();
            lblTitle.Text = this.Title;

            System.Web.UI.WebControls.ImageButton ibClose = new System.Web.UI.WebControls.ImageButton();
            ibClose.Width = 14;
            ibClose.Height = 14;
            ibClose.CausesValidation = false;
            ibClose.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "WindowPanelClose");
            ibClose.Click += new ImageClickEventHandler(ibClose_Click);
            //ibClose.OnClientClick

            Image imgMax = new Image();
            imgMax.ID = "Max";
            imgMax.Width = 14;
            imgMax.Height = 14;
            imgMax.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "WindowPanelMax");
            
            Image imgMin = new Image();
            imgMin.ID = "Min";
            imgMin.Width = 14;
            imgMin.Height = 14;
            imgMin.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "WindowPanelMin");

            TableCell tableCellTitle = new TableCell();
            tableCellTitle.Controls.Add(lblTitle);

            TableCell tableCellIcon1 = new TableCell();
            tableCellIcon1.ID = "MaxCell";
            tableCellIcon1.Width = 20;
            tableCellIcon1.HorizontalAlign = HorizontalAlign.Center;

            TableCell tableCellIcon2 = new TableCell();
            tableCellIcon2.ID = "MinCell";
            tableCellIcon2.Width = 20;
            tableCellIcon2.HorizontalAlign = HorizontalAlign.Center;

            TableCell tableCellIcon3 = new TableCell();
            tableCellIcon3.Width = 20;
            tableCellIcon3.HorizontalAlign = HorizontalAlign.Center;

            tableCellIcon1.Controls.Add(imgMax);
            tableCellIcon2.Controls.Add(imgMin);
            tableCellIcon3.Controls.Add(ibClose);

            TableRow tableRowIcon = new TableRow();
            tableRowIcon.HorizontalAlign = HorizontalAlign.Right;
            tableRowIcon.Cells.Add(tableCellIcon1);
            tableRowIcon.Cells.Add(tableCellIcon2);
            tableRowIcon.Cells.Add(tableCellIcon3);

            Table tableIcon = new Table();
            tableIcon.CellSpacing = 0;
            tableIcon.CellPadding = 0;
            tableIcon.BorderWidth = 0;
            tableIcon.Rows.Add(tableRowIcon);

            TableCell tableCellIcon = new TableCell();
            tableCellIcon.HorizontalAlign = HorizontalAlign.Right;
            tableCellIcon.Controls.Add(tableIcon);

            TableRow tableRowHeader = new TableRow();
            tableRowHeader.Cells.Add(tableCellTitle);
            tableRowHeader.Cells.Add(tableCellIcon);

            Table table = new Table();
            table.CellSpacing = 1;
            table.CellPadding = 1;
            table.BorderWidth = 0;
            table.Width = Unit.Percentage(100);
            table.Rows.Add(tableRowHeader);

            Panel panelHeader = new Panel();
            panelHeader.ID = "Header";
            panelHeader.CssClass = this.HeaderCssClass;
            panelHeader.Controls.Add(table);

            Panel panelContent = new Panel();
            panelContent.ID = "Content";
            panelContent.Style["zoom"] = "1";
            panelContent.CssClass = this.ContentCssClass;

            this.Controls.Add(panelHeader);
            this.Controls.Add(panelContent);

            if (!this.DesignMode)
            {
                Panel panelControl = new Panel();
                panelControl.Style["zoom"] = "1";

                Control control = this.Page.LoadControl(this.ControlPath);

                if (control != null)
                {
                    panelControl.Controls.Add(control);

                    panelContent.Controls.Add(panelControl);

                    IWindowPanelContent windowPanelContent = control as IWindowPanelContent;

                    if (windowPanelContent != null && this.dataObject != null)
                        windowPanelContent.Initialize(this.dataObject);
                }
            }
        }

        #endregion

        #region public void ibClose_Click(object sender, ImageClickEventArgs e)
        
        public void ibClose_Click(object sender, ImageClickEventArgs e)
        {
            this.Hidden(false);
        }

        #endregion

        #region public void Show()

        public void Show()
        {
            this.Visible = true;

            this.ClearChildControlState();
            this.ClearChildViewState();

            this.ChildControlsCreated = false;
        }

        #endregion

        #region public void Hidden(bool refresh)

        public void Hidden(bool refresh)
        {
            this.OnClose();

            if (refresh)
                this.OnRefresh();

            this.Visible = false;

            this.ClearChildControlState();
            this.ClearChildViewState();
        }

        #endregion

        #region private void OnClose()

        private void OnClose()
        {
            EventHandler eventHandler = (EventHandler)base.Events[WindowPanel.EventClose];

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }

        #endregion

        #region private void OnRefresh()

        private void OnRefresh()
        {
            EventHandler eventHandler = (EventHandler)base.Events[WindowPanel.EventRefresh];

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }

        #endregion

        #region public static void Hidden(Control control, bool refresh)
        
        public static void Hidden(Control control, bool refresh)
        {
            Control controlParent = control.Parent.Parent.Parent;

            if (controlParent.GetType() == typeof(WindowPanel))
            {
                ((WindowPanel)controlParent).Hidden(refresh);
            }
        }

        #endregion

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "100");
        }

        #endregion

        #region protected override void OnPreRender(EventArgs e)

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this.DesignMode)
            {
                ResourceManager.RegisterCoreScriptResource(this.Page);

                ScriptManager.GetCurrent(this.Page).RegisterScriptControl(this);
            }
        }

        #endregion

        #region protected override void Render(HtmlTextWriter writer)

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);

            base.Render(writer);
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.WindowPanel",
                    this.ClientID);

            descriptor.AddProperty("heightFix", this.HeightFix.ToString());
            descriptor.AddProperty("zIndex", this.ZIndex.ToString());

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("WindowPanel");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
