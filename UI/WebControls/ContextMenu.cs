using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using Likol.Web.Resources;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:ContextMenu runat=server></{0}:ContextMenu>"),
    ParseChildren(true, "Items"),
    DefaultProperty("Items"),
    DefaultEvent("MenuItemClick")
    ]
    public class ContextMenu : ContextControl, IPostBackEventHandler
    {
        private static readonly object EventMenuItemClick;

        #region public event ContextMenuItemClickEventHandler MenuItemClick
        
        [
        Category("Likol")
        ]
        public event ContextMenuItemClickEventHandler MenuItemClick
        {
            add { base.Events.AddHandler(ContextMenu.EventMenuItemClick, value); }
            remove { base.Events.RemoveHandler(ContextMenu.EventMenuItemClick, value); }
        }

        #endregion

        #region static ContextMenu()
        
        static ContextMenu()
        {
            ContextMenu.EventMenuItemClick = new object();
        }

        #endregion

        private ContextMenuItemCollection items;

        #region public ContextMenuItemCollection Items
        
        [
        Editor("Likol.Design.Web.UI.WebControls.ContextMenuItemCollectionEditor", typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null),
        Category("Likol")
        ]
        public ContextMenuItemCollection Items
        {
            get
            {
                if (this.items == null)
                    this.items = new ContextMenuItemCollection();

                return this.items;
            }
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

        #region public string ItemHoverCssClass
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ItemHoverCssClass
        {
            get { return this.StringProperty("ItemHoverCssClass"); }
            set { this.ViewState["ItemHoverCssClass"] = value; }
        }

        #endregion

        #region public string SplitCssClass
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string SplitCssClass
        {
            get { return this.StringProperty("SplitCssClass"); }
            set { this.ViewState["SplitCssClass"] = value; }
        }

        #endregion

        #region public Unit ImageWidth
        
        [
        DefaultValue(typeof(Unit), "20"),
        Category("Likol")
        ]
        public Unit ImageWidth
        {
            get { return this.UnitProperty("ImageWidth", Unit.Pixel(20)); }
            set { this.ViewState["ImageWidth"] = value; }
        }

        #endregion

        #region public bool AllowDescription
        
        [
        DefaultValue(false),
        Category("Likol")
        ]
        public bool AllowDescription
        {
            get { return this.BooleanProperty("AllowDescription", false); }
            set { this.ViewState["AllowDescription"] = value; }
        }

        #endregion

        #region public bool HasVisibleItem
        
        [
        Browsable(false)
        ]
        public bool HasVisibleItem
        {
            get
            {
                foreach (ContextMenuItem menuItem in this.Items)
                {
                    if (!(menuItem is ContextMenuSplitItem) && menuItem.Visible)
                        return true;
                }

                return false;
            }
        }

        #endregion

        public ContextMenu()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            if (this.DesignMode && this.Parent is DataControlFieldCell) return;

            foreach (ContextMenuItem menuItem in this.GetVisibleItems())
            {
                Panel panelMenuItem = this.CreateMenuItemPanel(menuItem);
                
                menuItem.MakeClientClickScript(this.DesignMode, this, panelMenuItem);

                if (!(menuItem is ContextMenuSplitItem))
                {
                    panelMenuItem.Attributes["CommandName"] = menuItem.CommandName;
                    panelMenuItem.DataBinding += new EventHandler(MenuItem_DataBinding);
                }

                Panel panelFrame = new Panel();
                panelFrame.Style["padding"] = "1px";
                panelFrame.Controls.Add(panelMenuItem);

                this.Controls.Add(panelFrame);
            }
        }

        #endregion

        #region protected void MenuItem_DataBinding(object sender, EventArgs e)
        
        protected void MenuItem_DataBinding(object sender, EventArgs e)
        {
            Panel panelMenuItem = sender as Panel;
            ContextMenuItem menuItem = this.Items[panelMenuItem.Attributes["CommandName"]];

            menuItem.MakeDataBindingData(this.DesignMode, this, panelMenuItem);
        }

        #endregion

        #region private Panel CreateMenuItemPanel(ContextMenuItem menuItem)
        
        private Panel CreateMenuItemPanel(ContextMenuItem menuItem)
        {
            if (menuItem is ContextMenuSplitItem)
            {
                Panel panelSplitContent = new Panel();
                panelSplitContent.CssClass = this.SplitCssClass;

                Panel panleSplit = new Panel();
                panleSplit.Controls.Add(panelSplitContent);

                return panleSplit;
            }

            TableRow rowMenuItem = new TableRow();
            rowMenuItem.Cells.Add(this.CreateMenuItemIconCell(menuItem));
            rowMenuItem.Cells.Add(this.CreateMenuItemTextCell(menuItem));

            Table tableMenuItem = new Table();
            tableMenuItem.CellSpacing = 0;
            tableMenuItem.CellPadding = 0;
            tableMenuItem.BorderWidth = 0;
            tableMenuItem.Rows.Add(rowMenuItem);

            Panel panelMenuItem = new Panel();
            panelMenuItem.CssClass = this.ItemCssClass;

            panelMenuItem.Attributes["onmouseover"] = "this.className='" + this.ItemHoverCssClass + "';";

            panelMenuItem.Attributes["onmouseout"] = "this.className='" + this.ItemCssClass + "';";

            panelMenuItem.Controls.Add(tableMenuItem);

            return panelMenuItem;
        }

        #endregion

        #region private TableCell CreateMenuItemIconCell(ContextMenuItem menuItem)
        
        private TableCell CreateMenuItemIconCell(ContextMenuItem menuItem)
        {
            TableCell cellIcon = new TableCell();
            cellIcon.VerticalAlign = VerticalAlign.Middle;
            cellIcon.HorizontalAlign = HorizontalAlign.Center;

            cellIcon.Width = this.ImageWidth;

            if (menuItem.ImageUrl != "")
            {
                Image imgIcon = new Image();
                imgIcon.AlternateText = menuItem.Text;
                imgIcon.ImageUrl = menuItem.ImageUrl;

                cellIcon.Controls.Add(imgIcon);
            }

            return cellIcon;
        }

        #endregion

        #region private TableCell CreateMenuItemTextCell(ContextMenuItem menuItem)
        
        private TableCell CreateMenuItemTextCell(ContextMenuItem menuItem)
        {
            TableCell cellText = new TableCell();
            cellText.CssClass = "Text";

            cellText.Controls.Add(new LiteralControl(menuItem.Text));

            if (this.AllowDescription)
            {
                cellText.VerticalAlign = VerticalAlign.Top;

                Label lblDesc = new Label();
                lblDesc.Text = menuItem.Description;
                lblDesc.ForeColor = System.Drawing.ColorTranslator.FromHtml("#666666");

                Panel panelDesc = new Panel();
                panelDesc.Style["padding-top"] = "3px";

                panelDesc.Controls.Add(lblDesc);
                cellText.Controls.Add(panelDesc);
            }

            return cellText;
        }

        #endregion

        #region private ContextMenuItemCollection GetVisibleItems()
        
        private ContextMenuItemCollection GetVisibleItems()
        {
            bool blockSpitItem = true;

            ContextMenuItemCollection visibleItems = new ContextMenuItemCollection();

            foreach (ContextMenuItem menuItem in this.Items)
            {
                if (blockSpitItem && menuItem is ContextMenuSplitItem) continue;

                if (!menuItem.Visible) continue;

                if (menuItem is ContextMenuSplitItem)
                {
                    blockSpitItem = true;
                }
                else if (menuItem.Visible)
                {
                    blockSpitItem = false;
                }

                visibleItems.Add(menuItem);
            }

            if (visibleItems.Count > 0)
            {
                int lastItemIndex = visibleItems.Count - 1;

                if (visibleItems[lastItemIndex] is ContextMenuSplitItem)
                    visibleItems.RemoveAt(lastItemIndex);
            }

            return visibleItems;
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
                    this.items = (ContextMenuItemCollection)savedStateArray[1];
                }
            }
        }

        #endregion

        #region public void RaisePostBackEvent(string eventArgument)
        
        public void RaisePostBackEvent(string eventArgument)
        {
            string[] argumentArray = eventArgument.Split('_');

            if (argumentArray.Length == 3)
            {
                ContextMenuItemClickEventArgs args = new ContextMenuItemClickEventArgs();

                if (argumentArray[0] == "true")
                    this.Page.Validate();

                args.Text = argumentArray[1];
                args.CommandName = argumentArray[2];

                ContextMenuItemClickEventHandler eventHandler = (ContextMenuItemClickEventHandler)base.Events[ContextMenu.EventMenuItemClick];

                if (eventHandler != null)
                {
                    eventHandler(this, args);
                }

                CommandEventArgs commandArgs = new CommandEventArgs(args.CommandName, "");

                this.RaiseBubbleEvent(this, commandArgs);
            }
            else
            {
                throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
            }
        }

        #endregion
    }
}
