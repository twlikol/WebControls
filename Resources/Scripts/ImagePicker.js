
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.ImagePicker = function(element)
{
    Likol.Web.UI.WebControls.ImagePicker.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._valueElement = null;
    this._valueClientElement = null;
    this._buttonElement = null;
    this._imageElement = null;

    this._cssClass = null;
    this._hoverCssClass = null;

    this._buttonCssClass = null;
    this._buttonHoverCssClass = null;

    this._dialogWidth = null;
    this._dialogHeight = null;
    this._dialogUrl = null;

    this._imageWidth = null;
    this._imageHeight = null;

    this._readOnly = null;

    this._mouseOverHandlerDelegate = null;
    this._mouseOutHandlerDelegate = null;

    this._clickHandlerDelegate = null;
}

Likol.Web.UI.WebControls.ImagePicker.prototype =
{
    get_cssClass: function () { return this._cssClass; },
    set_cssClass: function (value) { this._cssClass = value; },

    get_hoverCssClass: function () { return this._hoverCssClass; },
    set_hoverCssClass: function (value) { this._hoverCssClass = value; },

    get_buttonCssClass: function () { return this._buttonCssClass; },
    set_buttonCssClass: function (value) { this._buttonCssClass = value; },

    get_buttonHoverCssClass: function () { return this._buttonHoverCssClass; },
    set_buttonHoverCssClass: function (value) { this._buttonHoverCssClass = value; },

    get_dialogWidth: function() { return this._dialogWidth; },
    set_dialogWidth: function(value) { this._dialogWidth = value; },

    get_dialogHeight: function() { return this._dialogHeight; },
    set_dialogHeight: function(value) { this._dialogHeight = value; },

    get_dialogUrl: function() { return this._dialogUrl; },
    set_dialogUrl: function(value) { this._dialogUrl = value; },

    get_imageWidth: function() { return this._imageWidth; },
    set_imageWidth: function(value) { this._imageWidth = value; },

    get_imageHeight: function() { return this._imageHeight; },
    set_imageHeight: function(value) { this._imageHeight = value; },

    get_readOnly: function() { return this._readOnly; },
    set_readOnly: function(value) { this._readOnly = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.ImagePicker.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._valueElement = $get(this._controlElement.id + "_Value");
        this._valueClientElement = $get(this._controlElement.id + "_Value_Client");
        this._buttonElement = $get(this._controlElement.id + "_Button");
        this._imageElement = $get(this._controlElement.id + "_Image");

        this._imageElement.style.display = "none";

        if (this._valueClientElement.value)
        {
            this._imageElement.src = this._valueClientElement.value;

            //this._buttonElement.style.display = "none";
            this._imageElement.style.display = "block";
        }

        if (this._readOnly) return;

        //var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

        //this._buttonElement.style.left = controlBounds.x + "px";
        //this._buttonElement.style.top = controlBounds.y + "px";

        this._mouseOverHandlerDelegate = Function.createDelegate(this, this.mouseOver);
        this._mouseOutHandlerDelegate = Function.createDelegate(this, this.mouseOut);

        $addHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $addHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        this._clickHandlerDelegate = Function.createDelegate(this, this.click);

        $addHandler(this._buttonElement, 'click', this._clickHandlerDelegate);
    },
    dispose: function()
    {
        if (!this._readOnly)
        {
            $removeHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
            $removeHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

            $removeHandler(this._buttonElement, 'click', this._clickHandlerDelegate);
        }

        Likol.Web.UI.WebControls.ImagePicker.callBaseMethod(this, "dispose");
    },
    mouseOver: function()
    {
        this._controlElement.className = this._hoverCssClass;
        this._buttonElement.className = this._buttonHoverCssClass;

        //this._buttonElement.style.display = "block";
    },
    mouseOut: function()
    {
        this._controlElement.className = this._cssClass;
        this._buttonElement.className = this._buttonCssClass;

        if (this._valueClientElement.value)
        {
            //this._buttonElement.style.display = "none";
        }
    },
    click: function()
    {
        var returnValue = Dialog.Open(this._dialogUrl, this._dialogWidth, this._dialogHeight);

        if (returnValue != null)
        {
            var valueArray = new String(returnValue).split(';');

            this._valueElement.value = valueArray[0];
            this._valueClientElement.value = valueArray[1];

            this._imageElement.src = this._valueClientElement.value;

            this._imageElement.style.display = "block";
        }
    }
}

Likol.Web.UI.WebControls.ImagePicker.registerClass('Likol.Web.UI.WebControls.ImagePicker', Sys.UI.Control);