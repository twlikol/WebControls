
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.SplitButton = function(element)
{
    Likol.Web.UI.WebControls.SplitButton.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._buttonElement = null;
    this._splitElement = null;

    this._cssClass = null;
    this._hoverCssClass = null;

    this._buttonCssClass = null;
    this._buttonHoverCssClass = null;

    this._splitCssClass = null;
    this._splitHoverCssClass = null;

    this._mouseOver = false;
    this._buttonMouseOver = false;
    this._splitMouseOver = false;

    this._contextControlID = null;

    this._contextPopUp = false;

    this._mouseOverHandlerDelegate = null;
    this._mouseOutHandlerDelegate = null;

    this._buttonMouseOverHandlerDelegate = null;
    this._buttonMouseOutHandlerDelegate = null;

    this._splitMouseOverHandlerDelegate = null;
    this._splitMouseOutHandlerDelegate = null;

    this._splitClickHandlerDelegate = null;
}

Likol.Web.UI.WebControls.SplitButton.prototype =
{
    get_cssClass: function () { return this._cssClass; },
    set_cssClass: function (value) { this._cssClass = value; },

    get_hoverCssClass: function () { return this._hoverCssClass; },
    set_hoverCssClass: function (value) { this._hoverCssClass = value; },

    get_buttonCssClass: function () { return this._buttonCssClass; },
    set_buttonCssClass: function (value) { this._buttonCssClass = value; },

    get_buttonHoverCssClass: function () { return this._buttonHoverCssClass; },
    set_buttonHoverCssClass: function (value) { this._buttonHoverCssClass = value; },

    get_splitCssClass: function () { return this._splitCssClass; },
    set_splitCssClass: function (value) { this._splitCssClass = value; },

    get_splitHoverCssClass: function () { return this._splitHoverCssClass; },
    set_splitHoverCssClass: function (value) { this._splitHoverCssClass = value; },

    get_contextControlID: function () { return this._contextControlID; },
    set_contextControlID: function (value) { this._contextControlID = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.SplitButton.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;
        
        this._buttonElement = document.getElementById(this._controlID + "_Button");
        this._splitElement = document.getElementById(this._controlID + "_Split");
        
        this._mouseOverHandlerDelegate = Function.createDelegate(this, this.mouseOver);
        this._mouseOutHandlerDelegate = Function.createDelegate(this, this.mouseOut);

        this._buttonMouseOverHandlerDelegate = Function.createDelegate(this, this.buttonMouseOver);
        this._buttonMouseOutHandlerDelegate = Function.createDelegate(this, this.buttonMouseOut);

        this._splitMouseOverHandlerDelegate = Function.createDelegate(this, this.splitMouseOver);
        this._splitMouseOutHandlerDelegate = Function.createDelegate(this, this.splitMouseOut);

        this._splitClickHandlerDelegate = Function.createDelegate(this, this.splitClick);

        $addHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $addHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        $addHandler(this._buttonElement, 'mouseover', this._buttonMouseOverHandlerDelegate);
        $addHandler(this._buttonElement, 'mouseout', this._buttonMouseOutHandlerDelegate);

        $addHandler(this._splitElement, 'mouseover', this._splitMouseOverHandlerDelegate);
        $addHandler(this._splitElement, 'mouseout', this._splitMouseOutHandlerDelegate);

        $addHandler(this._splitElement, 'click', this._splitClickHandlerDelegate);
    },
    dispose: function()
    {
        $removeHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $removeHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        $removeHandler(this._buttonElement, 'mouseover', this._buttonMouseOverHandlerDelegate);
        $removeHandler(this._buttonElement, 'mouseout', this._buttonMouseOutHandlerDelegate);

        $removeHandler(this._splitElement, 'mouseover', this._splitMouseOverHandlerDelegate);
        $removeHandler(this._splitElement, 'mouseout', this._splitMouseOutHandlerDelegate);

        $removeHandler(this._splitElement, 'click', this._splitClickHandlerDelegate);

        Likol.Web.UI.WebControls.SplitButton.callBaseMethod(this, "dispose");
    },
    mouseOver: function()
    {
        this._mouseOver = true;

        this._controlElement.className = this._hoverCssClass;
    },
    mouseOut: function()
    {
        this._mouseOver = false;

        if (!this._buttonMouseOver && !this._splitMouseOver && !this._contextPopUp)
        {
            this._controlElement.className = this._cssClass;
        }
    },
    buttonMouseOver: function()
    {
        this._buttonMouseOver = true;

        this._buttonElement.className = this._buttonHoverCssClass;
    },
    buttonMouseOut: function()
    {
        this._buttonMouseOver = false;

        if (!this._contextPopUp)
            this._buttonElement.className = this._buttonCssClass;
    },
    splitMouseOver: function()
    {
        this._splitMouseOver = true;

        this._splitElement.className = this._splitHoverCssClass;
    },
    splitMouseOut: function()
    {
        this._splitMouseOver = false;

        if (!this._contextPopUp)
            this._splitElement.className = this._splitCssClass;
    },
    splitClick: function()
    {
        if (this._contextControlID == "") return;

        var contextControl = $find(this._contextControlID);

        if (contextControl)
        {
            contextControl.PopUp(this._controlElement);
            contextControl.SetSource(this);

            this._buttonElement.className = this._buttonHoverCssClass;

            this._contextPopUp = true;
        }
    },
    PopupClose: function()
    {
        this._contextPopUp = false;

        if (!this._mouseOver)
            this._controlElement.className = this._cssClass;

        if (!this._buttonMouseOver)
            this._buttonElement.className = this._buttonCssClass;
        
        if (!this._splitMouseOver)
            this._splitElement.className = this._splitCssClass;
    }
}

Likol.Web.UI.WebControls.SplitButton.registerClass('Likol.Web.UI.WebControls.SplitButton', Sys.UI.Control);