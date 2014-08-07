using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:ImageView runat=server></{0}:ImageView>")
    ]
    public class ImageView : System.Web.UI.WebControls.Repeater, IPostBackEventHandler
    {
        private static readonly object EventItemClick;

        #region public event ImageViewItemClickEventHandler ItemClick

        [
        Category("Likol")
        ]
        public event ImageViewItemClickEventHandler ItemClick
        {
            add { base.Events.AddHandler(ImageView.EventItemClick, value); }
            remove { base.Events.RemoveHandler(ImageView.EventItemClick, value); }
        }

        #endregion

        #region static ImageView()

        static ImageView()
        {
            ImageView.EventItemClick = new object();
        }

        #endregion

        private ITemplate textTemplate;

        #region public string CssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string CssClass
        {
            get
            {
                object value = this.ViewState["CssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["CssClass"] = value; }
        }

        #endregion

        #region public ITemplate TextTemplate

        [
        TemplateContainer(typeof(ImagePanelContainer)),
        PersistenceMode(PersistenceMode.InnerProperty),
        Browsable(false)
        ]
        public ITemplate TextTemplate
        {
            get { return this.textTemplate; }
            set { this.textTemplate = value; }
        }

        #endregion

        #region public Unit ImageWidth

        [
        DefaultValue(60),
        Category("Likol")
        ]
        public Unit ImageWidth
        {
            get
            {
                object value = this.ViewState["ImageWidth"];

                if (value != null)
                {
                    return (Unit)value;
                }

                return 60;
            }
            set { this.ViewState["ImageWidth"] = value; }
        }

        #endregion

        #region public Unit ImageHeight

        [
        DefaultValue(60),
        Category("Likol")
        ]
        public Unit ImageHeight
        {
            get
            {
                object value = this.ViewState["ImageHeight"];

                if (value != null)
                {
                    return (Unit)value;
                }

                return 60;
            }
            set { this.ViewState["ImageHeight"] = value; }
        }

        #endregion

        #region public string ImagePanelCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ImagePanelCssClass
        {
            get
            {
                object value = this.ViewState["ImagePanelCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ImagePanelCssClass"] = value; }
        }

        #endregion

        #region public string ImagePanelHoverCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ImagePanelHoverCssClass
        {
            get
            {
                object value = this.ViewState["ImagePanelHoverCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ImagePanelHoverCssClass"] = value; }
        }

        #endregion

        #region public string ImageCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ImageCssClass
        {
            get
            {
                object value = this.ViewState["ImageCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ImageCssClass"] = value; }
        }

        #endregion

        #region public string TextCssClass

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string TextCssClass
        {
            get
            {
                object value = this.ViewState["TextCssClass"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["TextCssClass"] = value; }
        }

        #endregion        

        #region public string ImageUrlField

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ImageUrlField
        {
            get
            {
                object value = this.ViewState["ImageUrlField"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["ImageUrlField"] = value; }
        }

        #endregion

        #region public string DataField

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string DataField
        {
            get
            {
                object value = this.ViewState["DataField"];

                if (value != null)
                {
                    return (string)value;
                }

                return string.Empty;
            }
            set { this.ViewState["DataField"] = value; }
        }

        #endregion

        public ImageView()
        {
            Global.TimeIsValid();
        }

        #region protected override void OnInit(EventArgs e)
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.HeaderTemplate = new ImageViewHeader(this.CssClass);

            ImageViewItem imageViewItem = new ImageViewItem(this);
            imageViewItem.ID = "Item";

            this.ItemTemplate = imageViewItem;

            this.FooterTemplate = new ImageViewFooter();
        }

        #endregion

        #region protected override void InitializeItem(RepeaterItem item)
        
        protected override void InitializeItem(RepeaterItem item)
        {
            ITemplate itemTemplate = null;

            switch (item.ItemType)
            {
                case ListItemType.Header:
                    itemTemplate = this.HeaderTemplate;
                    break;

                case ListItemType.Footer:
                    itemTemplate = this.FooterTemplate;
                    break;

                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    itemTemplate = this.GetItemTemplate(item);
                    break;

                case ListItemType.Separator:
                    itemTemplate = this.SeparatorTemplate;
                    break;

                default:
                    break;
            }

            if (itemTemplate != null)
                itemTemplate.InstantiateIn(item);
        }

        #endregion

        #region private ITemplate GetItemTemplate(RepeaterItem item)
        
        private ITemplate GetItemTemplate(RepeaterItem item)
        {
            ImagePanel imagePanel = new ImagePanel();
            imagePanel.ID = "ipImage";
            imagePanel.AutoPostBack = false;
            imagePanel.ImageWidth = this.ImageHeight;
            imagePanel.ImageHeight = this.ImageHeight;
            imagePanel.CssClass = this.ImagePanelCssClass;
            imagePanel.HoverCssClass = this.ImagePanelHoverCssClass;
            imagePanel.ImageCssClass = this.ImageCssClass;
            imagePanel.TextCssClass = this.TextCssClass;
            imagePanel.TextTemplate = this.TextTemplate;

            imagePanel.DataBinding += new EventHandler(ImagePanel_DataBinding);

            return imagePanel;
        }

        #endregion

        #region private void ImagePanel_DataBinding(object sender, EventArgs e)

        private void ImagePanel_DataBinding(object sender, EventArgs e)
        {
            ImagePanel imagePanel = (ImagePanel)sender;

            RepeaterItem repeaterItem = (RepeaterItem)imagePanel.Parent;

            if (!this.DesignMode)
            {
                object data = DataBinder.GetPropertyValue(repeaterItem.DataItem, this.DataField);
                object imageUrl = DataBinder.GetPropertyValue(repeaterItem.DataItem, this.ImageUrlField);

                if (data != null) imagePanel.CommandArgument = data.ToString();
                if (imageUrl != null) imagePanel.ImageUrl = imageUrl.ToString();

                imagePanel.DataBinding += new EventHandler(ImagePanel_DataBinding);

                PostBackOptions postBackOptions = new PostBackOptions(this);
                postBackOptions.Argument = string.Format("Select${0}", imagePanel.CommandArgument);
                postBackOptions.PerformValidation = false;

                imagePanel.OnClientClick = this.Page.ClientScript.GetPostBackEventReference(postBackOptions);
            }
        }

        #endregion

        #region public void RaisePostBackEvent(string eventArgument)

        public void RaisePostBackEvent(string eventArgument)
        {
            string[] argumentArray = eventArgument.Split('$');

            if (argumentArray.Length == 2)
            {
                this.OnItemClick(argumentArray[1]);
            }
            else
            {
                throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
            }
        }

        #endregion

        #region private void OnItemClick(string argument)

        private void OnItemClick(string argument)
        {
            ImageViewItemClickEventHandler eventHandler = (ImageViewItemClickEventHandler)base.Events[ImageView.EventItemClick];

            ImageViewItemClickEventArgs eventArgs = new ImageViewItemClickEventArgs();
            eventArgs.DataArgument = argument;

            if (eventHandler != null)
            {
                eventHandler(this, eventArgs);
            }
        }

        #endregion

        #region private class ImageViewHeader
        
        private class ImageViewHeader : ITemplate
        {
            private string CssClass { get; set; }

            public ImageViewHeader(string cssClass)
            {
                this.CssClass = cssClass;
            }

            public void InstantiateIn(Control container)
            {
                container.Controls.Add(
                    new LiteralControl(
                        string.Format("<div class=\"{0}\">", this.CssClass)
                    )
                );
            }
        }

        #endregion

        #region private class ImageViewItem

        private class ImageViewItem : Control, ITemplate
        {
            private ImageView ImageView { get; set; }

            public ImageViewItem(ImageView imageView)
            {
                this.ImageView = imageView;
            }

            public void InstantiateIn(Control container)
            {
                ImagePanel imagePanel = new ImagePanel();
                imagePanel.ID = "ipImage";
                imagePanel.AutoPostBack = false;
                imagePanel.ImageWidth = this.ImageView.ImageHeight;
                imagePanel.ImageHeight = this.ImageView.ImageHeight;
                imagePanel.CssClass = this.ImageView.ImagePanelCssClass;
                imagePanel.HoverCssClass = this.ImageView.ImagePanelHoverCssClass;
                imagePanel.ImageCssClass = this.ImageView.ImageCssClass;
                imagePanel.TextCssClass = this.ImageView.TextCssClass;
                imagePanel.TextTemplate = this.ImageView.TextTemplate;

                imagePanel.DataBinding += new EventHandler(ImagePanel_DataBinding);

                PostBackOptions postBackOptions = new PostBackOptions(this);
                postBackOptions.Argument = "Select";
                postBackOptions.PerformValidation = false;

                imagePanel.OnClientClick = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true);

                container.Controls.Add(imagePanel);
            }

            #region private void ImagePanel_DataBinding(object sender, EventArgs e)

            private void ImagePanel_DataBinding(object sender, EventArgs e)
            {
                ImagePanel imagePanel = (ImagePanel)sender;

                RepeaterItem repeaterItem = (RepeaterItem)imagePanel.Parent;

                if (!this.DesignMode)
                {
                    object data = DataBinder.GetPropertyValue(repeaterItem.DataItem, this.ImageView.DataField);
                    object imageUrl = DataBinder.GetPropertyValue(repeaterItem.DataItem, this.ImageView.ImageUrlField);

                    if (data != null) imagePanel.CommandArgument = data.ToString();
                    if (imageUrl != null) imagePanel.ImageUrl = imageUrl.ToString();
                }
            }

            #endregion
        }

        #endregion

        #region private class ImageViewFooter
        
        private class ImageViewFooter : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                container.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div></div>"));
            }
        }

        #endregion

        public class ImagePanelContainer : System.Web.UI.WebControls.Panel, INamingContainer
        {
        }

        private class ImagePanel : CompositeControl, IPostBackEventHandler, ITemplate
        {
            private static readonly object EventClick;

            #region public event EventHandler Click

            [
            Category("Likol")
            ]
            public event EventHandler Click
            {
                add { base.Events.AddHandler(ImagePanel.EventClick, value); }
                remove { base.Events.RemoveHandler(ImagePanel.EventClick, value); }
            }

            #endregion

            #region static ImagePanel()

            static ImagePanel()
            {
                ImagePanel.EventClick = new object();
            }

            #endregion

            private Image imgIcon;
            private ITemplate textTemplate;

            #region public ITemplate TextTemplate

            [
            TemplateContainer(typeof(ImagePanelContainer)),
            PersistenceMode(PersistenceMode.InnerProperty),
            Browsable(false)
            ]
            public ITemplate TextTemplate
            {
                get { return this.textTemplate; }
                set { this.textTemplate = value; }
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

            #region public string ImageCssClass

            [
            DefaultValue(""),
            Category("Likol")
            ]
            public string ImageCssClass
            {
                get { return this.StringProperty("ImageCssClass"); }
                set { this.ViewState["ImageCssClass"] = value; }
            }

            #endregion

            #region public string TextCssClass

            [
            DefaultValue(""),
            Category("Likol")
            ]
            public string TextCssClass
            {
                get { return this.StringProperty("TextCssClass"); }
                set { this.ViewState["TextCssClass"] = value; }
            }

            #endregion

            #region public string ImageUrl

            [
            DefaultValue(""),
            Category("Likol"),
            Bindable(true)
            ]
            public string ImageUrl
            {
                get { return this.StringProperty("ImageUrl"); }
                set
                {
                    this.EnsureChildControls();

                    this.ViewState["ImageUrl"] = value;

                    this.imgIcon.ImageUrl = value;
                }
            }

            #endregion

            #region public Unit ImageWidth

            [
            DefaultValue(80),
            Category("Likol")
            ]
            public Unit ImageWidth
            {
                get { return this.UnitProperty("ImageWidth", 80); }
                set { this.ViewState["ImageWidth"] = value; }
            }

            #endregion

            #region public Unit ImageHeight

            [
            DefaultValue(80),
            Category("Likol")
            ]
            public Unit ImageHeight
            {
                get { return this.UnitProperty("ImageHeight", 80); }
                set { this.ViewState["ImageHeight"] = value; }
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

            #region public string CommandArgument

            [
            DefaultValue(""),
            Category("Likol"),
            ]
            public string CommandArgument
            {
                get { return this.StringProperty("CommandArgument"); }
                set { this.ViewState["CommandArgument"] = value; }
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

            #region public bool AutoPostBack

            [
            DefaultValue(true),
            Category("Likol")
            ]
            public bool AutoPostBack
            {
                get { return this.BooleanProperty("AutoPostBack", true); }
                set { this.ViewState["AutoPostBack"] = value; }
            }

            #endregion

            #region public string OnClientClick

            [
            DefaultValue(""),
            Category("Likol"),
            ]
            public string OnClientClick
            {
                get { return this.StringProperty("OnClientClick"); }
                set { this.ViewState["OnClientClick"] = value; }
            }

            #endregion

            #region protected override HtmlTextWriterTag TagKey

            protected override HtmlTextWriterTag TagKey
            {
                get { return HtmlTextWriterTag.Div; }
            }

            #endregion

            #region protected override void CreateChildControls()

            protected override void CreateChildControls()
            {
                base.CreateChildControls();

                this.imgIcon = new Image();
                this.imgIcon.ImageUrl = this.ImageUrl;

                Panel panelImage = new Panel();
                panelImage.Width = this.ImageWidth;
                panelImage.Height = this.ImageHeight;
                panelImage.CssClass = this.ImageCssClass;

                panelImage.Controls.Add(this.imgIcon);

                ImagePanelContainer container = new ImagePanelContainer();
                container.Width = this.ImageWidth;
                container.CssClass = this.TextCssClass;
                container.Style[HtmlTextWriterStyle.Overflow] = "hidden";

                if (this.TextTemplate != null)
                    this.TextTemplate.InstantiateIn(container);

                this.Controls.Add(panelImage);
                this.Controls.Add(container);
            }

            #endregion

            #region protected override void AddAttributesToRender(HtmlTextWriter writer)

            protected override void AddAttributesToRender(HtmlTextWriter writer)
            {
                base.AddAttributesToRender(writer);

                if (this.HoverCssClass != "")
                {
                    writer.AddAttribute("onmouseover", string.Format("this.className='{0}'", this.HoverCssClass));
                    writer.AddAttribute("onmouseout", string.Format("this.className='{0}'", this.CssClass));
                }

                if (this.AutoPostBack)
                {
                    string eventArgument = string.Format("{0}${1}", this.CommandName, this.CommandArgument);

                    PostBackOptions postBackOptions = new PostBackOptions(this);
                    postBackOptions.Argument = eventArgument;
                    postBackOptions.PerformValidation = this.CausesValidation;

                    writer.AddAttribute("onclick", this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true));
                }
                else if (this.OnClientClick != "")
                {
                    writer.AddAttribute("onclick", this.OnClientClick);
                }
            }

            #endregion

            #region protected override void OnPreRender(EventArgs e)

            protected override void OnPreRender(EventArgs e)
            {
                base.OnPreRender(e);

                //string eventArgument = string.Format("{0}${1}", this.CommandName, this.CommandArgument);

                //PostBackOptions postBackOptions = new PostBackOptions(this);
                //postBackOptions.Argument = eventArgument;
                //postBackOptions.PerformValidation = this.CausesValidation;

                //this.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(postBackOptions);
            }

            #endregion

            #region public void RaisePostBackEvent(string eventArgument)

            public void RaisePostBackEvent(string eventArgument)
            {
                string[] argumentArray = eventArgument.Split('$');

                if (argumentArray.Length == 2)
                {
                    this.OnClick();
                }
                else
                {
                    throw new ArgumentException(string.Format("事件引發錯誤：參數長度不符合. Argument='{0}'", eventArgument));
                }
            }

            #endregion

            #region private void OnClick()

            private void OnClick()
            {
                EventHandler eventHandler = (EventHandler)base.Events[ImagePanel.EventClick];

                if (eventHandler != null)
                {
                    eventHandler(this, EventArgs.Empty);
                }

                CommandEventArgs commandArgs = new CommandEventArgs(this.CommandName, this.CommandArgument);

                this.RaiseBubbleEvent(this, commandArgs);
            }

            #endregion

            #region public void InstantiateIn(Control container)

            public void InstantiateIn(Control container)
            {
                container.Controls.Add(this);
            }

            #endregion
        }
    }
}
