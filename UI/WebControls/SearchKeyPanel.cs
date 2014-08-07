using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Likol.Web.UI;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.SearchKeyPanel.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.SearchKeyPanelClear.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:SearchKeyPanel runat=server></{0}:SearchKeyPanel>"),
    ParseChildren(true, "Keys"),
    DefaultProperty("Keys")
    ]
    public class SearchKeyPanel : CompositeControl, IScriptControl
    {
        private static readonly object EventMenuItemClick;

        #region public event ContextMenuItemClickEventHandler MenuItemClick

        [
        Category("Likol")
        ]
        public event ContextMenuItemClickEventHandler MenuItemClick
        {
            add { base.Events.AddHandler(SearchKeyPanel.EventMenuItemClick, value); }
            remove { base.Events.RemoveHandler(SearchKeyPanel.EventMenuItemClick, value); }
        }

        #endregion

        #region static SearchKeyPanel()

        static SearchKeyPanel()
        {
            SearchKeyPanel.EventMenuItemClick = new object();
        }

        #endregion

        private SearchKeyItemCollection keys;
        private Dictionary<string, object> keyValues;

        #region public SearchKeyItemCollection Keys
        
        [
        Editor("Likol.Design.Web.UI.WebControls.SearchKeyItemCollectionEditor", typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null),
        Category("Likol")
        ]
        public SearchKeyItemCollection Keys
        {
            get
            {
                if (this.keys == null)
                    this.keys = new SearchKeyItemCollection();

                return this.keys;
            }
        }

        #endregion

        #region public string ClearImageUrl
        
        [
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor)),
        DefaultValue(""),
        Category("Likol")
        ]
        public string ClearImageUrl
        {
            get { return this.StringProperty("ClearImageUrl"); }
            set { this.ViewState["ClearImageUrl"] = value; }
        }

        #endregion

        #region public string HoverCssClass
        
        [
        DefaultValue(""),
        Category("Likol")
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
        Category("Likol")
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

        #region public string MenuSplitCssClass
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string MenuSplitCssClass
        {
            get { return this.StringProperty("MenuSplitCssClass"); }
            set { this.ViewState["MenuSplitCssClass"] = value; }
        }

        #endregion

        #region public string Title

        [
        DefaultValue("Conditional"),
        Category("Likol")
        ]
        public string Title
        {
            get { return this.StringProperty("Title", "Conditional"); }
            set { this.ViewState["Title"] = value; }
        }

        #endregion

        #region public string ClearText

        [
        DefaultValue("Clear"),
        Category("Likol")
        ]
        public string ClearText
        {
            get { return this.StringProperty("ClearText", "Clear"); }
            set { this.ViewState["ClearText"] = value; }
        }

        #endregion

        #region public string ClearAllText

        [
        DefaultValue("ClearAll"),
        Category("Likol")
        ]
        public string ClearAllText
        {
            get { return this.StringProperty("ClearAllText", "ClearAll"); }
            set { this.ViewState["ClearAllText"] = value; }
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

        public SearchKeyPanel()
        {
            Global.TimeIsValid();
        }

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

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            this.Controls.Add(new LiteralControl(string.Format("{0} {{ ", this.Title)));

            string keyFormat = " [ {0} = '{1}' ] ";

            ContextMenu contextMenu = new ContextMenu();
            contextMenu.ID = "KeysMenu";
            contextMenu.CssClass = this.MenuCssClass;
            contextMenu.ItemCssClass = this.MenuItemCssClass;
            contextMenu.ItemHoverCssClass = this.MenuItemHoverCssClass;
            contextMenu.SplitCssClass = this.MenuSplitCssClass;

            contextMenu.MenuItemClick += new ContextMenuItemClickEventHandler(ContextMenu_MenuItemClick);

            foreach (SearchKeyItem searchKeyItem in this.Keys)
            {
                Control targetControl = WebControlUtil.FindControl(this, searchKeyItem.ControlID);

                if (!(targetControl is ISearchKeyControl))
                {
                    string keyValueText = "";

                    if (!this.DesignMode)
                        keyValueText = this.keyValues[searchKeyItem.Key].ToString();
                    else
                        keyValueText = "DataBound";

                    if (keyValueText == "") continue;

                    this.Controls.Add(new LiteralControl(string.Format(keyFormat, searchKeyItem.Text, keyValueText)));

                    if (!searchKeyItem.AllowRemove) continue;

                    ContextMenuButtonItem contextMenuButtonItem = new ContextMenuButtonItem();
                    contextMenuButtonItem.CommandName = searchKeyItem.Key;
                    contextMenuButtonItem.Text = string.Format("{0} {1}", this.ClearText, searchKeyItem.Text);

                    contextMenu.Items.Add(contextMenuButtonItem);
                }
                else
                {
                    SearchKeyValueCollection searchKeyValues;

                    if (!this.DesignMode)
                    {
                        searchKeyValues = (SearchKeyValueCollection)this.keyValues[searchKeyItem.Key];
                    }
                    else
                    {
                        searchKeyValues = new SearchKeyValueCollection();

                        //foreach (BaseField baseField in ((ISearchKeyControl)targetControl).Fields)
                        //{
                        //    searchKeyValues.Add(baseField.FieldName, true, baseField.Name, "DataBound");
                        //}
                    }

                    foreach (SearchKeyValue searchKeyValue in searchKeyValues)
                    {
                        if (searchKeyValue.Text == "") continue;

                        this.Controls.Add(new LiteralControl(string.Format(keyFormat, searchKeyValue.Name, searchKeyValue.Text)));

                        if (!searchKeyValue.AllowEmpty) continue;

                        ContextMenuButtonItem contextMenuButtonItem = new ContextMenuButtonItem();
                        contextMenuButtonItem.CommandName = string.Format("{0}:{1}", searchKeyItem.Key, searchKeyValue.Key); ;
                        contextMenuButtonItem.Text = string.Format("{0} {1}", this.ClearText, searchKeyValue.Name);

                        contextMenu.Items.Add(contextMenuButtonItem);
                    }
                }
            }

            this.Controls.Add(new LiteralControl(" }"));

            if (this.Controls.Count > 2)
            {
                if (contextMenu.Items.Count > 0)
                {
                    ContextMenuSplitItem contextMenuSplitItem = new ContextMenuSplitItem();
                    contextMenu.Items.Add(contextMenuSplitItem);

                    ContextMenuButtonItem contextMenuButtonItem = new ContextMenuButtonItem();
                    contextMenuButtonItem.CommandName = "ClearAll";
                    contextMenuButtonItem.Text = this.ClearAllText;
                    contextMenuButtonItem.ImageUrl = this.ClearImageUrl;

                    if (contextMenuButtonItem.ImageUrl == "")
                    {
                        contextMenuButtonItem.ImageUrl = ResourceManager.GetImageWebResourceUrl(this, "SearchKeyPanelClear");
                    }

                    contextMenu.Items.Add(contextMenuButtonItem);

                    if (!this.DesignMode)
                    {
                        this.Controls.Add(contextMenu);
                    }
                }

                this.Style[HtmlTextWriterStyle.Cursor] = "pointer";
                this.Style[HtmlTextWriterStyle.Display] = "block";
            }
            else
            {
                this.Controls.Clear();

                this.Style[HtmlTextWriterStyle.Display] = "none";
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

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)
        
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (this.Controls.Count > 0)
            {
                writer.AddAttribute("onmouseover", "this.className='" + this.HoverCssClass + "'");
                writer.AddAttribute("onmouseout", "this.className='" + this.CssClass + "'");
            }
        }

        #endregion

        #region private void Page_LoadComplete(object sender, EventArgs e)
        
        private void Page_LoadComplete(object sender, EventArgs e)
        {
            this.UpdateValues();
        }

        #endregion

        #region private void ContextMenu_MenuItemClick(object sender, ContextMenuItemClickEventArgs e)
        
        private void ContextMenu_MenuItemClick(object sender, ContextMenuItemClickEventArgs e)
        {
            if (e.CommandName == "ClearAll")
            {
                this.ClearValues();
            }
            else
            {
                string[] commandNames = e.CommandName.Split(':');

                if (commandNames.Length == 1)
                {
                    SearchKeyItem searchKeyItem = this.Keys[e.CommandName];

                    this.SetValue(searchKeyItem, string.Empty);
                }
                else
                {
                    SearchKeyItem searchKeyItem = this.Keys[commandNames[0]];

                    ISearchKeyControl searchKeyControl = WebControlUtil.FindControl(this, searchKeyItem.ControlID) as ISearchKeyControl;

                    string searchKeyValueKey = commandNames[1];

                    searchKeyControl.SetValue(searchKeyValueKey, string.Empty);
                }
            }

            ContextMenuItemClickEventHandler eventHandler = (ContextMenuItemClickEventHandler)base.Events[SearchKeyPanel.EventMenuItemClick];

            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        #endregion

        #region protected override object SaveViewState()
        
        protected override object SaveViewState()
        {
            object[] savedState = new object[3];
            savedState[0] = base.SaveViewState();

            if (this.Keys.Count != 0)
                savedState[1] = this.Keys;

            if (this.keyValues.Count != 0)
                savedState[2] = this.keyValues;

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
                    this.keys = (SearchKeyItemCollection)savedStateArray[1];
                }

                if (savedStateArray[2] != null)
                {
                    this.keyValues = (Dictionary<string, object>)savedStateArray[2];
                }
            }
        }

        #endregion

        #region private void UpdateValues()
        
        private void UpdateValues()
        {
            bool valueChanged = false;

            if (this.keyValues == null) this.keyValues = new Dictionary<string, object>();

            foreach (SearchKeyItem searchKeyItem in this.Keys)
            {
                object oldValue = string.Empty;

                if (this.keyValues.TryGetValue(searchKeyItem.Key, out oldValue))
                {
                    object newValue = this.GetValue(searchKeyItem);

                    this.keyValues[searchKeyItem.Key] = this.GetValue(searchKeyItem);

                    if (oldValue.ToString() != newValue.ToString()) valueChanged = true;
                }
                else
                {
                    this.keyValues.Add(searchKeyItem.Key, this.GetValue(searchKeyItem));
                }
            }

            if (valueChanged) this.ChildControlsCreated = false;
        }

        #endregion

        #region private void ClearValues()
        
        private void ClearValues()
        {
            foreach (SearchKeyItem searchKeyItem in this.Keys)
            {
                this.SetValue(searchKeyItem, string.Empty);
            }
        }

        #endregion

        #region private void SetValue(SearchKeyItem searchKeyItem, object value)
        
        private void SetValue(SearchKeyItem searchKeyItem, object value)
        {
            Control targetControl = WebControlUtil.FindControl(this, searchKeyItem.ControlID);

            PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(searchKeyItem.ValueProperty, targetControl);

            propertyDescriptor.SetValue(targetControl, value);
        }

        #endregion

        #region private object GetValue(SearchKeyItem searchKeyItem)

        private object GetValue(SearchKeyItem searchKeyItem)
        {
            if (this.DesignMode) return "資料繫結";

            Control targetControl = WebControlUtil.FindControl(this, searchKeyItem.ControlID);

            if (targetControl == null)
            {
                throw new InvalidOperationException(string.Format("資料聯繫錯誤：未發現控制項. ControlID={0}", searchKeyItem.ControlID));
            }

            if (targetControl is ISearchKeyControl)
            {
                return ((ISearchKeyControl)targetControl).SearchValues;
            }

            string text = DataBinder.Eval(targetControl, searchKeyItem.TextProperty).ToString();
            string value = DataBinder.Eval(targetControl, searchKeyItem.ValueProperty).ToString();

            if (!string.IsNullOrEmpty(value)) return text;

            return "";
        }

        #endregion

        #region private PropertyDescriptor GetPropertyDescriptor(string propertyName, Control control)
        
        private PropertyDescriptor GetPropertyDescriptor(string propertyName, Control control)
        {
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(control);

            foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
            {
                if (propertyDescriptor.Name.ToUpper() == propertyName.ToUpper())
                {
                    return propertyDescriptor;
                }
            }

            return null;
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        
        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.SearchKeyPanel",
                    this.ClientID);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()
        
        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("SearchKeyPanel");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
