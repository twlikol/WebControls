using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Likol.Web.Resources;
using System.Drawing.Design;

[assembly: WebResource("Likol.Web.Resources.Scripts.DropDownList.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.DropDownListArrow.gif", "image/gif")]
[assembly: WebResource("Likol.Web.Resources.Images.DropDownListSelected.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>"),
    ParseChildren(true, "Items"),
    DefaultProperty("Items"),
    DefaultEvent("SelectedIndexChanged")
    ]
    public class DropDownList : CompositeControl, IScriptControl
    {
        private Image imgIcon;
        private Label lblText;
        private HiddenField hfValue;
        private Image imgArrow;

        private static readonly object EventSelectedIndexChanged;

        #region public event EventHandler SelectedIndexChanged

        [
        Category("Likol")
        ]
        public event EventHandler SelectedIndexChanged
        {
            add { base.Events.AddHandler(DropDownList.EventSelectedIndexChanged, value); }
            remove { base.Events.RemoveHandler(DropDownList.EventSelectedIndexChanged, value); }
        }

        #endregion

        #region static DropDownList()

        static DropDownList()
        {
            DropDownList.EventSelectedIndexChanged = new object();
        }

        #endregion

        private DropDownItemCollection items;

        #region public DropDownItemCollection Items

        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null),
        Category("Likol")
        ]
        public DropDownItemCollection Items
        {
            get
            {
                if (this.items == null)
                    this.items = new DropDownItemCollection();

                return this.items;
            }
        }

        #endregion

        #region public string ImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string ImageUrl
        {
            get { return this.StringProperty("ImageUrl"); }
            set { this.ViewState["ImageUrl"] = value; }
        }

        #endregion

        #region public string SelectedImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string SelectedImageUrl
        {
            get { return this.StringProperty("SelectedImageUrl"); }
            set { this.ViewState["SelectedImageUrl"] = value; }
        }

        #endregion

        #region public string ArrowImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))
        ]
        public string ArrowImageUrl
        {
            get { return this.StringProperty("ArrowImageUrl"); }
            set { this.ViewState["ArrowImageUrl"] = value; }
        }

        #endregion

        #region public string HoverCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string HoverCssClass
        {
            get { return this.StringProperty("HoverCssClass"); }
            set { this.ViewState["HoverCssClass"] = value; }
        }

        #endregion

        #region public string MenuCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string MenuCssClass
        {
            get { return this.StringProperty("MenuCssClass"); }
            set { this.ViewState["MenuCssClass"] = value; }
        }

        #endregion

        #region public string MenuItemCssClass

        [
        DefaultValue(""),
        Category("Likol"),
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
        Category("Likol"),
        ]
        public string MenuItemHoverCssClass
        {
            get { return this.StringProperty("MenuItemHoverCssClass"); }
            set { this.ViewState["MenuItemHoverCssClass"] = value; }
        }

        #endregion

        #region public string MenuSplitCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string MenuSplitCssClass
        {
            get { return this.StringProperty("MenuSplitCssClass"); }
            set { this.ViewState["MenuSplitCssClass"] = value; }
        }

        #endregion

        #region public bool CausesValidation

        [
        DefaultValue(true),
        Category("Likol")
        ]
        public bool CausesValidation
        {
            get { return this.BooleanProperty("CausesValidation", true); }
            set { this.ViewState["CausesValidation"] = value; }
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

        #region public string SelectedText
        
        public string SelectedText
        {
            get
            {
                if (this.SelectedIndex != -1)
                {
                    return this.Items[this.SelectedIndex].Text;
                }

                return "";
            }
        }

        #endregion

        #region public string SelectedValue
        
        public string SelectedValue
        {
            get
            {
                if (this.SelectedIndex != -1)
                {
                    return this.Items[this.SelectedIndex].Value;
                }

                return "";
            }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        public DropDownList()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.imgIcon = new Image();
            this.imgIcon.ImageAlign = ImageAlign.AbsMiddle;
            this.imgIcon.ImageUrl = this.ImageUrl;

            DropDownItem dropDownItemSelected = null;

            this.lblText = new Label();

            this.hfValue = new HiddenField();

            TableCell tableCell = new TableCell();

            if (this.Items.Count != 0)
            {
                if (this.SelectedIndex == -1)
                {
                    dropDownItemSelected = this.Items[0];

                    this.SelectedIndex = 0;
                }
                else
                {
                    dropDownItemSelected = this.Items[this.SelectedIndex];
                }

                this.lblText.Text = dropDownItemSelected.Text;
                this.hfValue.Value = dropDownItemSelected.Text;

                ContextMenu contextMenu = new ContextMenu();
                contextMenu.ID = "ItemsMenu";
                contextMenu.CssClass = this.MenuCssClass;
                contextMenu.ItemCssClass = this.MenuItemCssClass;
                contextMenu.ItemHoverCssClass = this.MenuItemHoverCssClass;
                contextMenu.SplitCssClass = this.MenuSplitCssClass;

                contextMenu.MenuItemClick += new ContextMenuItemClickEventHandler(ContextMenu_MenuItemClick);

                for (int i = 0; i < this.Items.Count; i++)
                {
                    DropDownItem dropDownItem = this.Items[i];

                    ContextMenuButtonItem contextMenuButtonItem = new ContextMenuButtonItem();
                    contextMenuButtonItem.CommandName = i.ToString();
                    contextMenuButtonItem.CausesValidation = this.CausesValidation;
                    contextMenuButtonItem.Text = dropDownItem.Text;

                    if (i == this.SelectedIndex)
                    {
                        string selectedImageUrl = this.SelectedImageUrl;

                        if (selectedImageUrl == "")
                        {
                            selectedImageUrl = ResourceManager.GetImageWebResourceUrl(this, "DropDownListSelected");
                        }

                        contextMenuButtonItem.ImageUrl = selectedImageUrl;
                    }

                    contextMenu.Items.Add(contextMenuButtonItem);
                }

                if (!this.DesignMode)
                    tableCell.Controls.Add(contextMenu);
            }

            this.imgArrow = new Image();
            this.imgArrow.ImageAlign = ImageAlign.AbsMiddle;

            string arrowImageUrl = this.ArrowImageUrl;

            if (arrowImageUrl == "")
            {
                arrowImageUrl = ResourceManager.GetImageWebResourceUrl(this, "DropDownListArrow");
            }

            this.imgArrow.ImageUrl = arrowImageUrl;

            TableCell tableCellIcon = new TableCell();
            tableCellIcon.Controls.Add(this.imgIcon);

            TableCell tableCellText = new TableCell();
            tableCellText.Controls.Add(this.lblText);
            tableCellText.Controls.Add(this.hfValue);

            TableCell tableCellArrow = new TableCell();
            tableCellArrow.HorizontalAlign = HorizontalAlign.Right;
            tableCellArrow.Controls.Add(this.imgArrow);

            TableRow tableRowControl = new TableRow();
            tableRowControl.Cells.Add(tableCellIcon);
            tableRowControl.Cells.Add(tableCellText);
            tableRowControl.Cells.Add(tableCellArrow);

            Table tableControl = new Table();
            tableControl.Width = this.Width;
            tableControl.CellSpacing = 0;
            tableControl.CellPadding = 2;
            tableControl.BorderWidth = 0;
            tableControl.Rows.Add(tableRowControl);

            tableCell.Controls.AddAt(0, tableControl);

            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(tableCell);

            this.Controls.Add(tableRow);
        }

        #endregion

        #region private void ContextMenu_MenuItemClick(object sender, ContextMenuItemClickEventArgs e)

        private void ContextMenu_MenuItemClick(object sender, ContextMenuItemClickEventArgs e)
        {
            int selectedIndex = Convert.ToInt32(e.CommandName);

            this.SelectedIndex = selectedIndex;

            EventHandler eventHandler = (EventHandler)base.Events[DropDownList.EventSelectedIndexChanged];

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }

            this.ChildControlsCreated = false;
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

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
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
                    this.items = (DropDownItemCollection)savedStateArray[1];
                }
            }
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.DropDownList",
                    this.ClientID);

            descriptor.AddProperty("cssClass", this.CssClass);
            descriptor.AddProperty("hoverCssClass", this.HoverCssClass);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("DropDownList");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
