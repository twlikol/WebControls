
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.NavigationBar = function(element)
{
    Likol.Web.UI.WebControls.NavigationBar.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._headerElement = null;
    this._containerElement = null;
    this._itemsElement = null;

    this._parnetElement = null;

    this._sizeMode = null;
    this._parentLevel = null;

    this._resizeHandlerDelegate = null;
}

Likol.Web.UI.WebControls.NavigationBar.prototype =
{
    get_sizeMode: function () { return this._sizeMode; },
    set_sizeMode: function (value) { this._sizeMode = value; },

    get_parentLevel: function () { return this._parentLevel; },
    set_parentLevel: function (value) { this._parentLevel = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.NavigationBar.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._headerElement = $get(this._controlElement.id + "_Header");
        this._containerElement = $get(this._controlElement.id + "_Container");
        this._itemsElement = $get(this._controlElement.id + "_Items");

        this._parnetElement = this._controlElement;

        for(var i = 0; i < this._parentLevel; i++)
        {
            this._parnetElement = this._parnetElement.parentNode;
        }

        this.handleResize();

        this._resizeHandlerDelegate = Function.createDelegate(this, this.handleResize);

        $addHandler(window, 'resize', this._resizeHandlerDelegate);
    },
    dispose: function()
    {
        $removeHandler(window, 'resize', this._resizeHandlerDelegate);

        Likol.Web.UI.WebControls.NavigationBar.callBaseMethod(this, "dispose");
    },
    handleResize: function ()
    {
        var panelWidth = 0;
        var panelHeight = 0;

        if (this._sizeMode == 0)
        {
            //panelWidth = Core.GetWindowsWidth();
            panelWidth = -1;
            panelHeight = Core.GetWindowsHeight();
        }
        else if (this._sizeMode == 1)
        {
            panelWidth = -1;
            panelHeight = Core.GetElementHeight(this._parnetElement);
        }

        if (panelWidth != -1)
            this._controlElement.style.width = panelWidth + "px";

        if (panelHeight != -1)
        {
            this._controlElement.style.height = panelHeight + "px";

            var containerHeight = panelHeight - Core.GetElementHeight(this._headerElement)
                - Core.GetElementHeight(this._itemsElement)

            this._containerElement.style.height = containerHeight + "px";
        }
    }
}

Likol.Web.UI.WebControls.NavigationBar.registerClass('Likol.Web.UI.WebControls.NavigationBar', Sys.UI.Control);