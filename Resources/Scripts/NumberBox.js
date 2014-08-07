
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.NumberBox = function(element)
{
    Likol.Web.UI.WebControls.NumberBox.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._type = null;

    this._keyPressHandlerDelegate = null;
    this._changeHandlerDelegate = null;
}

Likol.Web.UI.WebControls.NumberBox.prototype =
{
    get_type: function () { return this._type; },
    set_type: function (value) { this._type = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.NumberBox.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;
        
        this._textBoxElement = document.getElementById(this._controlID + "_Value");

        this._keyPressHandlerDelegate = Function.createDelegate(this, this.handleKeyPress);
        this._changeHandlerDelegate = Function.createDelegate(this, this.handleChange);

        $addHandler(this._textBoxElement, 'keypress', this._keyPressHandlerDelegate);
        $addHandler(this._textBoxElement, 'change', this._changeHandlerDelegate);
    },
    dispose: function()
    {
        $removeHandler(this._textBoxElement, 'keypress', this._keyPressHandlerDelegate);
        $removeHandler(this._textBoxElement, 'change', this._changeHandlerDelegate);

        Likol.Web.UI.WebControls.NumberBox.callBaseMethod(this, "dispose");
    },
    handleChange: function()
    {
    },
    handleKeyPress: function()
    {
        if(event.keyCode != 46)
        {
            if((event.keyCode < 48) || (event.keyCode > 57)) event.returnValue = 0;
        }
        else
        {
            if (this._type == 0)
                event.returnValue = 0;
            else if(this._textBoxElement.value.indexOf('.') >= 0)
                event.returnValue = 0;
        }
    }
}

Likol.Web.UI.WebControls.NumberBox.registerClass('Likol.Web.UI.WebControls.NumberBox', Sys.UI.Control);