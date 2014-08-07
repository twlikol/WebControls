
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.ScrollPanel = function(element)
{
    Likol.Web.UI.WebControls.ScrollPanel.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._containerElement = null;

    this._controlElementHeight = null;

    this._parnetElement = null;

    this._sizeMode = null;
    this._parentLevel = null;
    this._removeElementIDs = null;

    this._resizeHandlerDelegate = null;
}

Likol.Web.UI.WebControls.ScrollPanel.prototype =
{
    get_sizeMode: function () { return this._sizeMode; },
    set_sizeMode: function (value) { this._sizeMode = value; },

    get_parentLevel: function () { return this._parentLevel; },
    set_parentLevel: function (value) { this._parentLevel = value; },

    get_removeElementIDs: function() { return this._removeElementIDs; },
    set_removeElementIDs: function(value) { this._removeElementIDs = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.ScrollPanel.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._containerElement = document.getElementById(this._controlID + "_Container");

        this._controlElementHeight = Core.GetElementHeight(this._controlElement);

        this._parnetElement = this._controlElement;

        for(var i = 0; i < this._parentLevel; i++)
        {
            this._parnetElement = this._parnetElement.parentNode;
        }
        
        this.handleResize();

        this._resizeHandlerDelegate = Function.createDelegate(this, this.handleResize);
        
        $addHandler(window, 'resize', this._resizeHandlerDelegate);

        LayoutManager.Subscribe(this._controlID);
    },
    dispose: function()
    {
        $removeHandler(window, 'resize', this._resizeHandlerDelegate);

        Likol.Web.UI.WebControls.ScrollPanel.callBaseMethod(this, "dispose");
    },
    handleResize: function()
    {
        this.handleHeightResize();

        this.handleWidthResize();
    },
    handleHeightResize: function()
    {
        this._controlElement.style.display = "none";

        var elementHeight = Core.GetWindowsHeight();

        if (this._sizeMode == 1)
        {
            elementHeight = Core.GetElementHeight(this._parnetElement);
        }
        else if (this._sizeMode == 2)
        {
            elementHeight = this._controlElementHeight;
        }

        if (this._removeElementIDs != "")
        {
            var removeElementIDs = this._removeElementIDs.split(';');

            for(var i = 0; i < removeElementIDs.length; i++)
            {
                var element = document.getElementById(removeElementIDs[i]);

                if (!element) continue;

                elementHeight -= Core.GetElementHeight(element);
            }
        }

        this._controlElement.style.height = elementHeight + "px";
        this._containerElement.style.height = elementHeight + "px";

        this._controlElement.style.overflow = "auto";

        this._controlElement.style.display = "block";
    },
    handleWidthResize: function()
    {
        if (this._sizeMode == 1)
        {
            var parentElementWidth = Core.GetElementWidth(this._parnetElement);

            //this._controlElement.style.width = parentElementWidth + "px";
        }
    }
}

Likol.Web.UI.WebControls.ScrollPanel.registerClass('Likol.Web.UI.WebControls.ScrollPanel', Sys.UI.Control);