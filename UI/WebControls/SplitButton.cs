using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.SplitButton.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.SplitButtonArrow.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:SplitButton runat=server></{0}:SplitButton>"),
    DefaultEvent("Click")
    ]
    public class SplitButton : CompositeControl, IScriptControl, IPostBackEventHandler
    {
        private static readonly object EventClick;

        #region public event EventHandler Click

        [
        Category("Likol")
        ]
        public event EventHandler Click
        {
            add { base.Events.AddHandler(SplitButton.EventClick, value); }
            remove { base.Events.RemoveHandler(SplitButton.EventClick, value); }
        }

        #endregion

        #region static SplitButton()

        static SplitButton()
        {
            SplitButton.EventClick = new object();
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

        #region public string SplitCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string SplitCssClass
        {
            get { return this.StringProperty("SplitCssClass"); }
            set { this.ViewState["SplitCssClass"] = value; }
        }

        #endregion

        #region public string SplitHoverCssClass

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string SplitHoverCssClass
        {
            get { return this.StringProperty("SplitHoverCssClass"); }
            set { this.ViewState["SplitHoverCssClass"] = value; }
        }

        #endregion

        #region public SplitButtonMode Mode

        [
        DefaultValue(SplitButtonMode.Large),
        Category("Likol"),
        ]
        public SplitButtonMode Mode
        {
            get
            {
                object value = this.ViewState["Mode"];

                if (value == null) return SplitButtonMode.Large;

                return (SplitButtonMode)value;
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

        #region public string Text

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string Text
        {
            get { return this.StringProperty("Text"); }
            set { this.ViewState["Text"] = value; }
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

        #region public string ImageUrl

        [
        DefaultValue(""),
        Category("Likol"),
        ]
        public string ImageUrl
        {
            get { return this.StringProperty("ImageUrl"); }
            set { this.ViewState["ImageUrl"] = value; }
        }

        #endregion

        #region public string ContextControlID

        public string ContextControlID
        {
            get { return this.StringProperty("ContextControlID"); }
            set { this.ViewState["ContextControlID"] = value; }
        }

        #endregion

        #region private ContextControl ContextControl

        private IContextControl ContextControl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ContextControlID))
                {
                    try
                    {
                        Control control = WebControlUtil.FindControl(this, this.ContextControlID);

                        if (control != null) return (IContextControl)control;
                    }
                    catch
                    {
                    }
                }
                return null;
            }
        }

        #endregion

        public SplitButton()
        {
            Global.TimeIsValid();
        }

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

            Image imgButton = new Image();
            imgButton.ImageAlign = ImageAlign.AbsMiddle;
            imgButton.ImageUrl = this.ImageUrl;

            Label lblText = new Label();
            lblText.Text = this.Text;

            string arrowImageUrl = this.ArrowImageUrl;

            if (arrowImageUrl == "")
            {
                arrowImageUrl = ResourceManager.GetImageWebResourceUrl(this, "SplitButtonArrow");
            }

            Image imgArrow = new Image();
            imgArrow.ImageUrl = arrowImageUrl;

            Panel panelArrow = new Panel();
            panelArrow.Controls.Add(imgArrow);

            TableCell tableCellButton = new TableCell();
            tableCellButton.ID = "Button";
            tableCellButton.CssClass = this.ButtonCssClass;
            tableCellButton.HorizontalAlign = HorizontalAlign.Center;
            tableCellButton.VerticalAlign = VerticalAlign.Middle;

            string eventArgument = string.Format("{0}", this.CommandName);

            PostBackOptions postBackOptions = new PostBackOptions(this);
            postBackOptions.Argument = eventArgument;
            postBackOptions.PerformValidation = this.CausesValidation;

            tableCellButton.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(postBackOptions);

            TableCell tableCellSplit = new TableCell();
            tableCellSplit.ID = "Split";
            tableCellSplit.CssClass = this.SplitCssClass;
            tableCellSplit.HorizontalAlign = HorizontalAlign.Center;

            if (this.Mode == SplitButtonMode.Large)
            {
                Panel panelText = new Panel();
                panelText.Controls.Add(lblText);

                tableCellButton.Controls.Add(imgButton);

                tableCellSplit.Controls.Add(panelText);
                tableCellSplit.Controls.Add(panelArrow);

                TableRow tableRowButton = new TableRow();
                tableRowButton.Cells.Add(tableCellButton);

                TableRow tableRowSplit = new TableRow();
                tableRowSplit.Cells.Add(tableCellSplit);

                this.Controls.Add(tableRowButton);
                this.Controls.Add(tableRowSplit);
            }
            else
            {
                tableCellButton.Controls.Add(imgButton);
                tableCellButton.Controls.Add(new LiteralControl(" "));
                tableCellButton.Controls.Add(lblText);

                tableCellSplit.Controls.Add(panelArrow);

                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(tableCellButton);
                tableRow.Cells.Add(tableCellSplit);

                this.Controls.Add(tableRow);
            }            
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

        #region private void OnClick()
        
        private void OnClick()
        {
            EventHandler eventHandler = (EventHandler)base.Events[SplitButton.EventClick];

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }

            CommandEventArgs commandArgs = new CommandEventArgs(this.CommandName, "");

            this.RaiseBubbleEvent(this, commandArgs);
        }

        #endregion

        #region public void RaisePostBackEvent(string eventArgument)

        public void RaisePostBackEvent(string eventArgument)
        {
            string[] argumentArray = eventArgument.Split('_');

            if (argumentArray.Length == 1)
            {
                this.OnClick();
            }
            else
            {
                throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
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
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.SplitButton",
                    this.ClientID);

            string contextControlID = "";

            if (this.ContextControl != null)
                contextControlID = this.ContextControl.GetClientID();

            descriptor.AddProperty("cssClass", this.CssClass);
            descriptor.AddProperty("hoverCssClass", this.HoverCssClass);
            descriptor.AddProperty("buttonCssClass", this.ButtonCssClass);
            descriptor.AddProperty("buttonHoverCssClass", this.ButtonHoverCssClass);
            descriptor.AddProperty("splitCssClass", this.SplitCssClass);
            descriptor.AddProperty("splitHoverCssClass", this.SplitHoverCssClass);
            descriptor.AddProperty("contextControlID", contextControlID);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("SplitButton");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}