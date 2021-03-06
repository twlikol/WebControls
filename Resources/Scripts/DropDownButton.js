﻿
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.DropDownButton = function(element)
{
    Likol.Web.UI.WebControls.DropDownButton.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._contextControlID = null;

    this._contextPopUp = false;

    this._mouseOver = false;

    this._cssClass = null;
    this._hoverCssClass = null;

    this._mouseOverHandlerDelegate = null;
    this._mouseOutHandlerDelegate = null;

    this._clickHandlerDelegate = null;
}

Likol.Web.UI.WebControls.DropDownButton.prototype =
{
    get_cssClass: function () { return this._cssClass; },
    set_cssClass: function (value) { this._cssClass = value; },

    get_hoverCssClass: function () { return this._hoverCssClass; },
    set_hoverCssClass: function (value) { this._hoverCssClass = value; },

    get_contextControlID: function () { return this._contextControlID; },
    set_contextControlID: function (value) { this._contextControlID = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.DropDownButton.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._mouseOverHandlerDelegate = Function.createDelegate(this, this.mouseOver);
        this._mouseOutHandlerDelegate = Function.createDelegate(this, this.mouseOut);

        $addHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $addHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        this._clickHandlerDelegate = Function.createDelegate(this, this.click);

        $addHandler(this._controlElement, 'click', this._clickHandlerDelegate);
    },
    dispose: function()
    {
        $removeHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $removeHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        $removeHandler(this._controlElement, 'click', this._clickHandlerDelegate);

        Likol.Web.UI.WebControls.DropDownButton.callBaseMethod(this, "dispose");
    },
    mouseOver: function()
    {
        this._mouseOver = true;

        this._controlElement.className = this._hoverCssClass;
    },
    mouseOut: function()
    {
        this._mouseOver = false;

        if (!this._contextPopUp)
            this._controlElement.className = this._cssClass;
    },
    click: function()
    {
        if (this._contextControlID == "") return;

        var contextControl = $find(this._contextControlID);

        if (contextControl)
        {
            contextControl.PopUp(this._controlElement);
            contextControl.SetSource(this);

            this._contextPopUp = true;
        }
    },
    PopupClose: function()
    {
        this._contextPopUp = false;

        if (!this._mouseOver)
            this._controlElement.className = this._cssClass;
    }
}

Likol.Web.UI.WebControls.DropDownButton.registerClass('Likol.Web.UI.WebControls.DropDownButton', Sys.UI.Control);