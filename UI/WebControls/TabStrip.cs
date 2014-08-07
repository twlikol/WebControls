using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.TabStrip.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:TabStrip runat=server></{0}:TabStrip>"),
    ParseChildren(true, "Tabs"),
    DefaultProperty("Tabs")
    ]
    public class TabStrip : CompositeControl, IPostBackEventHandler, IScriptControl
    {
        private static readonly object EventSelectedIndexChanged;

        #region public event EventHandler SelectedIndexChanged

        [
        Category("Likol")
        ]
        public event EventHandler SelectedIndexChanged
        {
            add { base.Events.AddHandler(TabStrip.EventSelectedIndexChanged, value); }
            remove { base.Events.RemoveHandler(TabStrip.EventSelectedIndexChanged, value); }
        }

        #endregion

        #region static TabStrip()

        static TabStrip()
        {
            TabStrip.EventSelectedIndexChanged = new object();
        }

        #endregion

        private TabStripItemCollection tabs;

        #region public TabControlItemCollection Tabs

        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null),
        Category("Likol")
        ]
        public TabStripItemCollection Tabs
        {
            get
            {
                if (this.tabs == null)
                    this.tabs = new TabStripItemCollection();

                return this.tabs;
            }
        }

        #endregion

        #region public string StartTabCssClass

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string StartTabCssClass
        {
            get { return this.StringProperty("StartTabCssClass"); }
            set { this.ViewState["StartTabCssClass"] = value; }
        }

        #endregion

        #region public string EndTabCssClass
        
        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string EndTabCssClass
        {
            get { return this.StringProperty("EndTabCssClass"); }
            set { this.ViewState["EndTabCssClass"] = value; }
        }

        #endregion

        #region public string TabCssClass
        
        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string TabCssClass
        {
            get { return this.StringProperty("TabCssClass"); }
            set { this.ViewState["TabCssClass"] = value; }
        }

        #endregion

        #region public string TabHoverCssClass
        
        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string TabHoverCssClass
        {
            get { return this.StringProperty("TabHoverCssClass"); }
            set { this.ViewState["TabHoverCssClass"] = value; }
        }

        #endregion

        #region public string TabSelectedCssClass
        
        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string TabSelectedCssClass
        {
            get { return this.StringProperty("TabSelectedCssClass"); }
            set { this.ViewState["TabSelectedCssClass"] = value; }
        }

        #endregion

        #region public override Unit Width

        [
        DefaultValue(typeof(Unit), "100%")
        ]
        public override Unit Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        #endregion

        #region public TabStripMode Mode

        [
        DefaultValue(TabStripMode.Client),
        Category("Likol"),
        ]
        public TabStripMode Mode
        {
            get
            {
                object value = this.ViewState["Mode"];

                if (value == null) return TabStripMode.Client;

                return (TabStripMode)value;
            }
            set { this.ViewState["Mode"] = value; }
        }

        #endregion

        #region public int SelectedIndex

        [
        DefaultValue(-1),
        Category("Likol")
        ]
        public int SelectedIndex
        {
            get
            {
                this.EnsureChildControls();

                return this.IntProperty("SelectedIndex", -1);
            }
            set
            {
                this.ChildControlsCreated = false;

                this.ViewState["SelectedIndex"] = value;
            }
        }

        #endregion

        #region public TabStripItem SelectedTab

        public TabStripItem SelectedTab
        {
            get
            {
                if (this.SelectedIndex != -1)
                {
                    return this.Tabs[this.SelectedIndex];
                }

                return null;
            }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

        #region public TabStrip()
        
        public TabStrip()
        {
            this.Width = Unit.Percentage(100);

            Global.TimeIsValid();
        }

        #endregion

        #region protected override void OnInit(EventArgs e)

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (this.Page != null)
            {
                this.Page.LoadComplete += new EventHandler(this.Page_LoadComplete);
            }
        }

        #endregion

        #region private void Page_LoadComplete(object sender, EventArgs e)

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            if (this.Mode == TabStripMode.Client) return;

            for (int i = 0; i < this.Tabs.Count; i++)
            {
                TabStripItem tabStripItem = this.Tabs[i];

                Control control = WebControlUtil.FindControl(this, tabStripItem.ControlID);

                if (control == null) continue;

                if (i == this.SelectedIndex) control.Visible = true;
                else control.Visible = false;
            }
        }

        #endregion

        #region protected override void CreateChildControls()

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            TableRow tableRow = new TableRow();
            tableRow.ID = "Tabs";

            TableCell tableCellStart = new TableCell();
            tableCellStart.CssClass = this.StartTabCssClass;
            tableCellStart.Text = "&nbsp;";

            tableRow.Cells.Add(tableCellStart);

            if (this.Tabs.Count != 0)
            {
                if (this.SelectedIndex == -1)
                {
                    this.SelectedIndex = 0;
                }

                for (int i = 0; i < this.Tabs.Count; i++)
                {
                    TabStripItem tabStripItem = this.Tabs[i];

                    TableCell tableCell = new TableCell();

                    string controlID = "";

                    Control control = WebControlUtil.FindControl(this, tabStripItem.ControlID);

                    if (control != null)
                        controlID = control.ClientID;

                    if (i == this.SelectedIndex)
                    {
                        tableCell.CssClass = this.TabSelectedCssClass;
                    }
                    else
                    {
                        tableCell.CssClass = this.TabCssClass;

                        if (this.Mode == TabStripMode.Client)
                            ((WebControl)control).Style[HtmlTextWriterStyle.Display] = "none";
                    }

                    if (this.Mode == TabStripMode.Client)
                    {
                        tableCell.Attributes["onmouseover"] = string.Format("$find('{0}').mouseOver(this, {1})", this.ClientID, i.ToString());
                        tableCell.Attributes["onmouseout"] = string.Format("$find('{0}').mouseOut(this, {1})", this.ClientID, i.ToString());
                        
                        tableCell.Attributes["onclick"] = string.Format("$find('{0}').click(this, {1}, '{2}')",
                            this.ClientID, i.ToString(), controlID);
                    }
                    else
                    {
                        if (i != this.SelectedIndex)
                        {
                            tableCell.Attributes["onmouseover"] = string.Format("this.className='{0}'", this.TabHoverCssClass);
                            tableCell.Attributes["onmouseout"] = string.Format("this.className='{0}'", this.TabCssClass);

                            PostBackOptions postBackOptions = new PostBackOptions(this);
                            postBackOptions.Argument = tabStripItem.CommandName;

                            tableCell.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(postBackOptions);
                        }
                        else
                        {
                            
                        }
                    }

                    if (tabStripItem.ImageUrl != "")
                    {
                        Image imgIcon = new Image();
                        imgIcon.ImageAlign = ImageAlign.AbsMiddle;
                        imgIcon.ImageUrl = tabStripItem.ImageUrl;

                        tableCell.Controls.Add(imgIcon);
                    }

                    tableCell.Controls.Add(new LiteralControl(" "));

                    Label lblText = new Label();
                    lblText.Text = tabStripItem.Text;

                    tableCell.Controls.Add(lblText);

                    tableRow.Cells.Add(tableCell);
                }
            }

            TableCell tableCellEnd = new TableCell();
            tableCellEnd.CssClass = this.EndTabCssClass;
            tableCellEnd.Text = "&nbsp;";

            tableRow.Cells.Add(tableCellEnd);

            Unit unitWidth = this.Width;

            if (unitWidth == Unit.Empty) unitWidth = Unit.Percentage(100);

            Table table = new Table();
            table.Width = unitWidth;
            table.CellSpacing = 0;
            table.CellPadding = 0;
            table.BorderWidth = 0;

            table.Rows.Add(tableRow);

            this.Controls.Add(table);
        }

        #endregion

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
        }

        #endregion

        #region protected override void OnPreRender(EventArgs e)

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this.DesignMode && this.Mode == TabStripMode.Client)
            {
                ResourceManager.RegisterCoreScriptResource(this.Page);

                ScriptManager.GetCurrent(this.Page).RegisterScriptControl(this);
            }
        }

        #endregion

        #region protected override void Render(HtmlTextWriter writer)

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode && this.Mode == TabStripMode.Client)
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);

            base.Render(writer);
        }

        #endregion

        #region protected override object SaveViewState()

        protected override object SaveViewState()
        {
            object[] savedState = new object[2];
            savedState[0] = base.SaveViewState();

            if (this.Tabs.Count != 0)
                savedState[1] = this.tabs;

            return savedState;
        }

        #endregion

        #region protected override void LoadViewState(object savedState)

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] savedStateArray = (object[])savedState;

                if (savedStateArray[0] != null)
                {
                    base.LoadViewState(savedStateArray[0]);
                }

                if (savedStateArray[1] != null)
                {
                    this.tabs = (TabStripItemCollection)savedStateArray[1];
                }
            }
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.TabStrip",
                    this.ClientID);

            string selectedControlID = "";

            if (this.Tabs.Count != 0)
            {
                TabStripItem tabStripItem = this.SelectedTab;

                Control control = WebControlUtil.FindControl(this, tabStripItem.ControlID);

                if (control != null)
                    selectedControlID = control.ClientID;
            }

            descriptor.AddProperty("selectedIndex", this.SelectedIndex);
            descriptor.AddProperty("selectedControlID", selectedControlID);
            descriptor.AddProperty("tabCssClass", this.TabCssClass);
            descriptor.AddProperty("tabHoverCssClass", this.TabHoverCssClass);
            descriptor.AddProperty("tabSelectedCssClass", this.TabSelectedCssClass);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("TabStrip");
            return new ScriptReference[] { scriptReference };
        }

        #endregion

        #region public void RaisePostBackEvent(string eventArgument)

        public void RaisePostBackEvent(string eventArgument)
        {
            string[] argumentArray = eventArgument.Split('_');

            if (argumentArray.Length == 1)
            {
                string commandName = argumentArray[0];

                this.SelectedIndex = this.Tabs.GetItemIndex(commandName);

                EventHandler eventHandler = (EventHandler)base.Events[TabStrip.EventSelectedIndexChanged];

                if (eventHandler != null)
                {
                    eventHandler(this, EventArgs.Empty);
                }

                this.ChildControlsCreated = false;
            }
            else
            {
                throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
            }
        }

        #endregion
    }
}
