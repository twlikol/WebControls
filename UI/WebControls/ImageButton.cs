using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:ImageButton runat=server></{0}:ImageButton>"),
    DefaultEvent("Click")
    ]
    public class ImageButton : CompositeControl, IPostBackEventHandler
    {
        private static readonly object EventClick;

        #region public event EventHandler Click

        [
        Category("Likol")
        ]
        public event EventHandler Click
        {
            add { base.Events.AddHandler(ImageButton.EventClick, value); }
            remove { base.Events.RemoveHandler(ImageButton.EventClick, value); }
        }

        #endregion

        #region static ImageButton()

        static ImageButton()
        {
            ImageButton.EventClick = new object();
        }

        #endregion

        private Image imgIcon;
        private Label lblText;

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

        #region public string Text

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string Text
        {
            get { return this.StringProperty("Text"); }
            set { this.ViewState["Text"] = value; }
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

        #region public string OnClientClick

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string OnClientClick
        {
            get { return this.StringProperty("OnClientClick"); }
            set { this.ViewState["OnClientClick"] = value; }
        }

        #endregion

        #region public ImageButtonMode Mode

        [
        DefaultValue(ImageButtonMode.Large),
        Category("Likol"),
        ]
        public ImageButtonMode Mode
        {
            get
            {
                object value = this.ViewState["Mode"];

                if (value == null) return ImageButtonMode.Large;

                return (ImageButtonMode)value;
            }
            set { this.ViewState["Mode"] = value; }
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

        #region public string CommandName

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string CommandName
        {
            get { return this.StringProperty("CommandName"); }
            set { this.ViewState["CommandName"] = value; }
        }

        #endregion

        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #endregion

        #region protected override void CreateChildControls()

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.imgIcon = new Image();
            this.imgIcon.ImageAlign = ImageAlign.AbsMiddle;
            this.imgIcon.ImageUrl = this.ImageUrl;

            this.lblText = new Label();
            this.lblText.Text = this.Text;

            TableCell tableCellIcon = new TableCell();
            tableCellIcon.VerticalAlign = VerticalAlign.Middle;
            tableCellIcon.HorizontalAlign = HorizontalAlign.Center;
            tableCellIcon.Controls.Add(this.imgIcon);

            TableCell tableCellText = new TableCell();
            tableCellText.VerticalAlign = VerticalAlign.Middle;
            tableCellText.HorizontalAlign = HorizontalAlign.Center;
            tableCellText.Controls.Add(this.lblText);

            TableCell tableCellSpace = new TableCell();
            tableCellSpace.Text = "&nbsp;";

            if (this.Mode == ImageButtonMode.Small)
            {
                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(tableCellIcon);
                tableRow.Cells.Add(tableCellText);
                tableRow.Cells.Add(tableCellSpace);

                this.Controls.Add(tableRow);
            }
            else
            {
                TableRow tableRowIcon = new TableRow();
                tableRowIcon.Controls.Add(tableCellIcon);

                TableRow tableRowText = new TableRow();
                tableRowText.Controls.Add(tableCellText);

                TableRow tableRowSpace = new TableRow();
                tableRowSpace.Controls.Add(tableCellSpace);

                this.Controls.Add(tableRowIcon);
                this.Controls.Add(tableRowText);
                this.Controls.Add(tableRowSpace);
            }

            string eventArgument = string.Format("{0}", this.CommandName);

            PostBackOptions postBackOptions = new PostBackOptions(this);
            postBackOptions.Argument = eventArgument;
            postBackOptions.PerformValidation = this.CausesValidation;

            this.Attributes["onclick"] = this.OnClientClick + this.Page.ClientScript.GetPostBackEventReference(postBackOptions);
        }

        #endregion

        #region protected override void AddAttributesToRender(HtmlTextWriter writer)

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");

            if (this.HoverCssClass != "")
            {
                writer.AddAttribute("onmouseover", string.Format("this.className='{0}'", this.HoverCssClass));
                writer.AddAttribute("onmouseout", string.Format("this.className='{0}'", this.CssClass));
            }
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
            EventHandler eventHandler = (EventHandler)base.Events[ImageButton.EventClick];

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }

            CommandEventArgs commandArgs = new CommandEventArgs(commandName, "");

            this.RaiseBubbleEvent(this, commandArgs);

            this.ChildControlsCreated = false;
        }

        #endregion
    }
}
