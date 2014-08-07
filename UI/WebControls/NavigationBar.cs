using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.NavigationBar.js", "application/x-javascript")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:NavigationBar runat=server></{0}:NavigationBar>"),
    ParseChildren(true, "Items"),
    DefaultProperty("Items")
    ]
    public class NavigationBar : CompositeControl, IScriptControl, IPostBackEventHandler
    {
        private static readonly object EventMenuItemClick;

        #region public event NavigationBarMenuItemClickEventHandler MenuItemClick

        [
        Category("Likol")
        ]
        public event NavigationBarMenuItemClickEventHandler MenuItemClick
        {
            add { base.Events.AddHandler(NavigationBar.EventMenuItemClick, value); }
            remove { base.Events.RemoveHandler(NavigationBar.EventMenuItemClick, value); }
        }

        #endregion

        #region static NavigationBar()

        static NavigationBar()
        {
            NavigationBar.EventMenuItemClick = new object();
        }

        #endregion

        private Panel panelHeader;
        private Panel panelContainer;
        private Panel panelItems;

        private Label lblHeaderText;

        private NavigationBarItemCollection items;

        #region public NavigationBarItemCollection Items

        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null),
        Category("Likol")
        ]
        public NavigationBarItemCollection Items
        {
            get
            {
                if (this.items == null)
                    this.items = new NavigationBarItemCollection();

                return this.items;
            }
        }

        #endregion

        #region public string HeaderCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string HeaderCssClass
        {
            get { return this.StringProperty("HeaderCssClass"); }
            set { this.ViewState["HeaderCssClass"] = value; }
        }

        #endregion

        #region public string ContainerCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ContainerCssClass
        {
            get { return this.StringProperty("ContainerCssClass"); }
            set { this.ViewState["ContainerCssClass"] = value; }
        }

        #endregion

        #region public string ItemsCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ItemsCssClass
        {
            get { return this.StringProperty("ItemsCssClass"); }
            set { this.ViewState["ItemsCssClass"] = value; }
        }

        #endregion

        #region public string MenuItemCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string MenuItemCssClass
        {
            get { return this.StringProperty("MenuItemCssClass"); }
            set { this.ViewState["MenuItemCssClass"] = value; }
        }

        #endregion

        #region public string MenuItemHoverCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string MenuItemHoverCssClass
        {
            get { return this.StringProperty("MenuItemHoverCssClass"); }
            set { this.ViewState["MenuItemHoverCssClass"] = value; }
        }

        #endregion

        #region public PanelSizeMode SizeMode

        [DefaultValue(PanelHeightType.FullPage)]
        public PanelSizeMode SizeMode
        {
            get
            {
                object value = this.ViewState["SizeMode"];

                if (value != null) return (PanelSizeMode)value;

                return PanelSizeMode.FullPage;
            }
            set { this.ViewState["SizeMode"] = value; }
        }

        #endregion

        #region public int ParentLevel

        [
        DefaultValue(1),
        Category("Likol")
        ]
        public int ParentLevel
        {
            get { return this.IntProperty("ParentLevel", 1); }
            set { this.ViewState["ParentLevel"] = value; }
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
            set { this.ViewState["SelectedIndex"] = value; }
        }

        #endregion        

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

        public NavigationBar()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.lblHeaderText = new Label();
            this.lblHeaderText.Text = "Header";

            this.panelHeader = new Panel();
            this.panelHeader.ID = "Header";
            this.panelHeader.CssClass = this.HeaderCssClass;
            this.panelHeader.Controls.Add(this.lblHeaderText);

            this.panelContainer = new Panel();
            this.panelContainer.ID = "Container";
            this.panelContainer.CssClass = this.ContainerCssClass;
            this.panelContainer.Style[HtmlTextWriterStyle.Overflow] = "auto";

            this.panelItems = new Panel();
            this.panelItems.ID = "Items";
            this.panelItems.CssClass = this.ItemsCssClass;

            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelItems);

            if (this.Items.Count != 0)
            {
                if (this.SelectedIndex == -1)
                {
                    this.SelectedIndex = 0;
                }

                for (int i = 0; i < this.Items.Count; i++)
                {
                    NavigationBarItem navigationBarItem = this.Items[i];

                    Panel panelItem = new Panel();
                    panelItem.CssClass = this.MenuItemCssClass;

                    panelItem.Attributes["onmouseover"] = string.Format("this.className='{0}'", this.MenuItemHoverCssClass);
                    panelItem.Attributes["onmouseout"] = string.Format("this.className='{0}'", this.MenuItemCssClass);

                    Image imgIcon = new Image();
                    imgIcon.ImageAlign = ImageAlign.AbsMiddle;
                    imgIcon.ImageUrl = navigationBarItem.ImageUrl;

                    TableCell tableCellIcon = new TableCell();
                    tableCellIcon.Controls.Add(imgIcon);

                    Label lblText = new Label();
                    lblText.Text = navigationBarItem.Text;

                    TableCell tableCellText = new TableCell();
                    tableCellText.Style["padding-left"] = "5px";
                    tableCellText.Controls.Add(lblText);

                    TableRow tableRow = new TableRow();
                    tableRow.Cells.Add(tableCellIcon);
                    tableRow.Cells.Add(tableCellText);

                    Table table = new Table();
                    table.CellSpacing = 0;
                    table.CellPadding = 1;
                    table.BorderWidth = 0;
                    table.Rows.Add(tableRow);

                    panelItem.Controls.Add(table);

                    this.panelItems.Controls.Add(panelItem);

                    string eventArgument = string.Format("{0}_{1}", navigationBarItem.CommandName, navigationBarItem.ControlPath);

                    panelItem.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(this, eventArgument);

                    if (i == this.SelectedIndex)
                    {
                        this.lblHeaderText.Text = navigationBarItem.Text;

                        if (!this.DesignMode)
                        {
                            string controlPath = navigationBarItem.ControlPath;

                            Control control = this.Page.LoadControl(controlPath);

                            this.panelContainer.Controls.Add(control);
                        }
                    }
                }
            }
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
            if (!this.DesignMode) ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);

            base.Render(writer);
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.NavigationBar",
                    this.ClientID);

            descriptor.AddProperty("sizeMode", this.SizeMode);
            descriptor.AddProperty("parentLevel", this.ParentLevel);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("NavigationBar");
            return new ScriptReference[] { scriptReference };
        }

        #endregion

        #region protected override object SaveViewState()

        protected override object SaveViewState()
        {
            object[] savedState = new object[2];
            savedState[0] = base.SaveViewState();

            if (this.Items.Count != 0)
                savedState[1] = this.items;

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
                    this.items = (NavigationBarItemCollection)savedStateArray[1];
                }
            }
        }

        #endregion

        #region public void RaisePostBackEvent(string eventArgument)

        public void RaisePostBackEvent(string eventArgument)
        {
            string[] argumentArray = eventArgument.Split('_');

            if (argumentArray.Length == 2)
            {
                NavigationBarMenuItemClickEventArgs args = new NavigationBarMenuItemClickEventArgs();
                args.CommandName = argumentArray[0];
                args.ControlPath = argumentArray[1];

                NavigationBarMenuItemClickEventHandler eventHandler = (NavigationBarMenuItemClickEventHandler)base.Events[NavigationBar.EventMenuItemClick];

                if (eventHandler != null)
                {
                    eventHandler(this, args);
                }

                NavigationBarItem navigationBarItem = this.Items[args.CommandName];

                if (navigationBarItem == null) return;

                int itemIndex = this.Items.GetItemIndex(navigationBarItem);

                if (itemIndex == -1) return;

                this.SelectedIndex = itemIndex;

                this.ChildControlsCreated = false;

                //CommandEventArgs commandArgs = new CommandEventArgs(args.CommandName, "");

                //this.RaiseBubbleEvent(this, commandArgs);
            }
            else
            {
                throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
            }
        }

        #endregion
    }
}
