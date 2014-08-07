
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.TabStrip = function(element)
{
    Likol.Web.UI.WebControls.TabStrip.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._tabs = null;

    this._selectedIndex = null;
    this._selectedControlID = null;

    this._tabCssClass = null;
    this._tabHoverCssClass = null;
    this._tabSelectedCssClass = null;
}

Likol.Web.UI.WebControls.TabStrip.prototype =
{
    get_selectedIndex: function () { return this._selectedIndex; },
    set_selectedIndex: function (value) { this._selectedIndex = value; },

    get_selectedControlID: function () { return this._selectedControlID; },
    set_selectedControlID: function (value) { this._selectedControlID = value; },

    get_tabCssClass: function () { return this._tabCssClass; },
    set_tabCssClass: function (value) { this._tabCssClass = value; },

    get_tabHoverCssClass: function () { return this._tabHoverCssClass; },
    set_tabHoverCssClass: function (value) { this._tabHoverCssClass = value; },

    get_tabSelectedCssClass: function () { return this._tabSelectedCssClass; },
    set_tabSelectedCssClass: function (value) { this._tabSelectedCssClass = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.TabStrip.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._tabs = document.getElementById(this._controlID + "_Tabs");
    },
    dispose: function()
    {
        Likol.Web.UI.WebControls.TabStrip.callBaseMethod(this, "dispose");
    },
    mouseOver: function(tab, index)
    {
        if(index != this._selectedIndex)
            tab.className = this._tabHoverCssClass;
    },
    mouseOut: function(tab, index)
    {
        if(index != this._selectedIndex)
            tab.className = this._tabCssClass;
    },
    click: function(tab, index, controlID)
    {
        var currentIndex = this._selectedIndex + 1;
        var currentTab = this._tabs.cells[currentIndex];
        currentTab.className = this._tabCssClass;

        if (this._selectedControlID)
        {
            var currentControl = $get(this._selectedControlID);
            if (currentControl) currentControl.style.display = "none";
        }

        var newIndex = index + 1;
        var newTab = this._tabs.cells[newIndex];
        newTab.className = this._tabSelectedCssClass;

        currentControl = $get(controlID);
        if (currentControl) currentControl.style.display = "block";

        this._selectedIndex = parseInt(index);
        this._selectedControlID = controlID;
    }
}

Likol.Web.UI.WebControls.TabStrip.registerClass('Likol.Web.UI.WebControls.TabStrip', Sys.UI.Control);