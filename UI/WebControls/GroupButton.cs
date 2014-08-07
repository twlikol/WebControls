using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:GroupButton runat=server></{0}:GroupButton>"),
    ParseChildren(true, "Items"),
    DefaultProperty("Items"),
    DefaultEvent("Click")
    ]
    public class GroupButton : CompositeControl, IPostBackEventHandler
    {
        private static readonly object EventClick;

        #region public event EventHandler Click

        [
        Category("Likol")
        ]
        public event EventHandler Click
        {
            add { base.Events.AddHandler(GroupButton.EventClick, value); }
            remove { base.Events.RemoveHandler(GroupButton.EventClick, value); }
        }

        #endregion

        #region static GroupButton()

        static GroupButton()
        {
            GroupButton.EventClick = new object();
        }

        #endregion

        private GroupButtonItemCollection items;

        #region public GroupButtonItemCollection Items

        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        DefaultValue((string)null),
        Category("Likol")
        ]
        public GroupButtonItemCollection Items
        {
            get
            {
                if (this.items == null)
                    this.items = new GroupButtonItemCollection();

                return this.items;
            }
        }

        #endregion

        #region public string ButtonCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string ButtonCssClass
        {
            get { return this.StringProperty("ButtonCssClass"); }
            set { this.ViewState["ButtonCssClass"] = value; }
        }

        #endregion

        #region public string ButtonHoverCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string ButtonHoverCssClass
        {
            get { return this.StringProperty("ButtonHoverCssClass"); }
            set { this.ViewState["ButtonHoverCssClass"] = value; }
        }

        #endregion

        #region public string ButtonSelectedCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string ButtonSelectedCssClass
        {
            get { return this.StringProperty("ButtonSelectedCssClass"); }
            set { this.ViewState["ButtonSelectedCssClass"] = value; }
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
                GroupButtonItem groupButtonItem = this.Items[this.SelectedIndex];

                if (groupButtonItem == null) return "";

                return groupButtonItem.Text;
            }
        }
        
        #endregion

        #region public GroupButtonMode Mode

        [
        DefaultValue(GroupButtonMode.Large),
        Category("Likol"),
        ]
        public GroupButtonMode Mode
        {
            get
            {
                object value = this.ViewState["Mode"];

                if (value == null) return GroupButtonMode.Large;

                return (GroupButtonMode)value;
            }
            set { this.ViewState["Mode"] = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        public GroupButton()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            TableRow tableRow = new TableRow();

            if (this.Items.Count != 0)
            {
                if (this.SelectedIndex == -1)
                {
                    this.SelectedIndex = 0;
                }

                for (int i = 0; i < this.Items.Count; i++)
                {
                    GroupButtonItem groupButtonItem = this.Items[i];

                    Image imgIcon = new Image();
                    imgIcon.ImageAlign = ImageAlign.AbsMiddle;
                    imgIcon.ImageUrl = groupButtonItem.ImageUrl;

                    Panel panelIcon = new Panel();
                    panelIcon.Controls.Add(imgIcon);

                    TableCell tableCell = new TableCell();
                    tableCell.HorizontalAlign = HorizontalAlign.Center;
                    tableCell.VerticalAlign = VerticalAlign.Top;
                    tableCell.Controls.Add(panelIcon);

                    if (this.SelectedIndex == i)
                    {
                        tableCell.CssClass = this.ButtonSelectedCssClass;
                    }
                    else
                    {
                        tableCell.CssClass = this.ButtonCssClass;

                        tableCell.Attributes["onmouseover"] = string.Format("this.className='{0}'", this.ButtonHoverCssClass);
                        tableCell.Attributes["onmouseout"] = string.Format("this.className='{0}'", this.ButtonCssClass);

                        string eventArgument = string.Format("{0}", groupButtonItem.CommandName);

                        PostBackOptions postBackOptions = new PostBackOptions(this);
                        postBackOptions.Argument = eventArgument;
                        postBackOptions.PerformValidation = this.CausesValidation;

                        tableCell.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(postBackOptions);
                    }

                    if (this.Mode == GroupButtonMode.Large)
                    {
                        Label lblText = new Label();
                        lblText.Text = groupButtonItem.Text;

                        Panel panelText = new Panel();
                        panelText.Controls.Add(lblText);

                        tableCell.Controls.Add(panelText);
                    }

                    tableRow.Cells.Add(tableCell);
                }
            }

            this.Controls.Add(tableRow);
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

        #region public void RaisePostBackEvent(string eventArgument)

        public void RaisePostBackEvent(string eventArgument)
        {
            string[] argumentArray = eventArgument.Split('_');

            if (argumentArray.Length == 1)
            {
                this.OnClick(argumentArray[0]);
            }
            else
            {
                throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
            }
        }

        #endregion

        #region private void OnClick(string commandName)

        private void OnClick(string commandName)
        {
            this.SelectedIndex = this.Items.GetItemIndex(commandName);

            EventHandler eventHandler = (EventHandler)base.Events[GroupButton.EventClick];

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }

            CommandEventArgs commandArgs = new CommandEventArgs(commandName, "");

            this.RaiseBubbleEvent(this, commandArgs);

            this.ChildControlsCreated = false;
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
                    this.items = (GroupButtonItemCollection)savedStateArray[1];
                }
            }
        }

        #endregion
    }
}
