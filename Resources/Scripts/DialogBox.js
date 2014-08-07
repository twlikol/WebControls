
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.DialogBox = function(element)
{
    Likol.Web.UI.WebControls.DialogBox.initializeBase(this, [element]);

    this._controlElement = null;

    this._dialogWidth = null;
    this._dialogHeight = null;
    this._dialogUrl = null;

    this._txtText = null;
    
    this._lbText = null;
    this._txtBox = null;

    this._lcButton = null;

    this._hfText = null;
    this._hfValue = null;

    this._clickHandlerDelegate = null;
}

Likol.Web.UI.WebControls.DialogBox.prototype =
{
    get_dialogWidth: function() { return this._dialogWidth; },
    set_dialogWidth: function(value) { this._dialogWidth = value; },

    get_dialogHeight: function() { return this._dialogHeight; },
    set_dialogHeight: function(value) { this._dialogHeight = value; },

    get_dialogUrl: function() { return this._dialogUrl; },
    set_dialogUrl: function(value) { this._dialogUrl = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.DialogBox.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();

        this._txtText = $get(this._controlElement.id + "_TextBox");

        this._lbText = $get(this._controlElement.id + "_Text");
        this._txtBox = $get(this._controlElement.id + "_Box");
        
        this._hfText = $get(this._controlElement.id + "_hfText");
        this._hfValue = $get(this._controlElement.id + "_hfValue");

        this._lcButton = $get(this._controlElement.id + "_Button");

        if (!this._lbText.innerText)
        {
            this._txtText.style.height = "16px";
        }

        if (this._lcButton)
        {
            this._clickHandlerDelegate = Function.createDelegate(this, this.click);

            $addHandler(this._lcButton, 'click', this._clickHandlerDelegate);
        }
    },
    dispose: function()
    {
        if (this._lcButton)
            $removeHandler(this._lcButton, 'click', this._clickHandlerDelegate);

        Likol.Web.UI.WebControls.DialogBox.callBaseMethod(this, "dispose");
    },
    click: function()
    {
        var returnValue = Dialog.Open(this._dialogUrl, this._dialogWidth, this._dialogHeight);

        if (returnValue != null)
        {
            var valueArray = new String(returnValue).split(';');

            this._lbText.innerHTML = valueArray[0];

            this._hfText.value = valueArray[0];
            this._hfValue.value = valueArray[1];

            this._txtText.style.height = null;
        }
    }
}

Likol.Web.UI.WebControls.DialogBox.registerClass('Likol.Web.UI.WebControls.DialogBox', Sys.UI.Control);