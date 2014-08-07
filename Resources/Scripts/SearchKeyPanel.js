
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.SearchKeyPanel = function(element)
{
    Likol.Web.UI.WebControls.SearchKeyPanel.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._contextMenu = null;
    this._contextMenuPopUp = false;

    this._clickHandlerDelegate = null;
}

Likol.Web.UI.WebControls.SearchKeyPanel.prototype =
{
    initialize: function()
    {
        Likol.Web.UI.WebControls.SearchKeyPanel.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._clickHandlerDelegate = Function.createDelegate(this, this.click);

        $addHandler(this._controlElement, 'click', this._clickHandlerDelegate);
    },
    dispose: function()
    {
        $removeHandler(this._controlElement, 'click', this._clickHandlerDelegate);

        Likol.Web.UI.WebControls.SearchKeyPanel.callBaseMethod(this, "dispose");
    },
    click: function(evt)
    {
        if (this._contextMenuPopUp) return;

        ev = this.getMouseEvent(evt);

        var x = ev.clientX;
        var y = ev.clientY;

        var contextMenu = $find(this._controlElement.id + "_KeysMenu");

        if (contextMenu)
        {
            contextMenu.PopUp(this._controlElement, x, y);
            contextMenu.SetSource(this);

            this._contextMenuPopUp = true;
        }
    },
    PopupClose: function()
    {
        this._contextMenuPopUp = false;
    },
    getMouseEvent: function (evt)
    {
        return window.event || evt; 
    }
}

Likol.Web.UI.WebControls.SearchKeyPanel.registerClass('Likol.Web.UI.WebControls.SearchKeyPanel', Sys.UI.Control);