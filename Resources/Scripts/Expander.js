
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.Expander = function (element)
{
    Likol.Web.UI.WebControls.Expander.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._expanderPanel = null;
    this._collapsePanel = null;
    this._expandPanel = null;
    this._contentPanel = null;

    this._statusField = null;

    this._mouseOverHandlerDelegate = null;
    this._mouseOutHandlerDelegate = null;

    this._collapseClickHandlerDelegate = null;
    this._expandClickHandlerDelegate = null;

    this._initialize = false;
}

Likol.Web.UI.WebControls.Expander.prototype =
{
    initialize: function()
    {
        Likol.Web.UI.WebControls.Expander.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._expanderPanel = document.getElementById(this._controlID + "_Expander");
        this._collapsePanel = document.getElementById(this._controlID + "_Collapse");
        this._expandPanel = document.getElementById(this._controlID + "_Expand");
        this._contentPanel = document.getElementById(this._controlID + "_Content");

        this._statusField = document.getElementById(this._controlID + "_Status");

        this._collapsePanel.style.display = "none";
        this._collapsePanel.style.position = "absolute";
        this._collapsePanel.style.zIndex = 200;

        var status = this._statusField.value;

        if (status == "Collapse") this.collapse();
        else this.expand();

        this._mouseOverHandlerDelegate = Function.createDelegate(this, this.mouseOver);
        this._mouseOutHandlerDelegate = Function.createDelegate(this, this.mouseOut);

        $addHandler(this._expanderPanel, 'mouseover', this._mouseOverHandlerDelegate);
        $addHandler(this._expanderPanel, 'mouseout', this._mouseOutHandlerDelegate);

        this._collapseClickHandlerDelegate = Function.createDelegate(this, this.collapse);
        this._expandClickHandlerDelegate = Function.createDelegate(this, this.expand);

        $addHandler(this._collapsePanel, 'click', this._collapseClickHandlerDelegate);
        $addHandler(this._expandPanel, 'click', this._expandClickHandlerDelegate);

        this._initialize = true;
    },
    dispose: function()
    {
        $removeHandler(this._expanderPanel, 'mouseover', this._mouseOverHandlerDelegate);
        $removeHandler(this._expanderPanel, 'mouseout', this._mouseOutHandlerDelegate);

        $removeHandler(this._collapsePanel, 'click', this._collapseClickHandlerDelegate);
        $removeHandler(this._expandPanel, 'click', this._expandClickHandlerDelegate);

        Likol.Web.UI.WebControls.Expander.callBaseMethod(this, "dispose");
    },
    mouseOver: function()
    {
        var status = this._statusField.value;

        if (status == "Expand")
        {
            with (this._collapsePanel)
            {
                //style.left = x + "px";
                //style.top = y + "px";
        
                style.display = "block";
            }

            var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);
            var collapseBounds = Sys.UI.DomElement.getBounds(this._collapsePanel);

            var x = controlBounds.x + controlBounds.width - collapseBounds.width;
            var y = controlBounds.y + controlBounds.height;

            if (Sys.Browser.agent == Sys.Browser.InternetExplorer)
            {
                if(window != window.top)
                {
                    x += -2;
                    y += -2;
                }
            }

            with (this._collapsePanel)
            {
                style.left = x + "px";
                style.top = y + "px";
        
                //style.display = "block";
            }
        }
    },
    mouseOut: function()
    {
        var status = this._statusField.value;

        if (status == "Expand")
            this._collapsePanel.style.display = "none";
    },
    collapse: function()
    {
        this._expandPanel.style.display = "block";
        this._contentPanel.style.display = "none";
        this._collapsePanel.style.display = "none";

        LayoutManager.Resize();

        this._statusField.value = "Collapse";
    },
    expand: function()
    {
        this._expandPanel.style.display = "none";
        this._contentPanel.style.display = "block";

        LayoutManager.Resize();

        if (this._initialize) this.mouseOver();

        this._statusField.value = "Expand";
    }
}

Likol.Web.UI.WebControls.Expander.registerClass('Likol.Web.UI.WebControls.Expander', Sys.UI.Control);