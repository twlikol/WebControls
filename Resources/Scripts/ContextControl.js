
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.ContextControl = function(element)
{
    Likol.Web.UI.WebControls.ContextControl.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._sourceControl = null;

    this._hasMouseOver = false;
    this._mouseOver = false;

    this._enableWait = true;

    this._mouseOverHandlerDelegate = null;
    this._mouseOutHandlerDelegate = null;

    this._clickHandlerDelegate = null;
}

Likol.Web.UI.WebControls.ContextControl.prototype =
{
    get_enableWait: function() { return this._enableWait; },
    set_enableWait: function(value) { this._enableWait = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.ContextControl.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._mouseOverHandlerDelegate = Function.createDelegate(this, this.mouseOver);
        this._mouseOutHandlerDelegate = Function.createDelegate(this, this.mouseOut);

        this._clickHandlerDelegate = Function.createDelegate(this, this.click);

        $addHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $addHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        $addHandler(this._controlElement, 'click', this._clickHandlerDelegate);
    },
    dispose: function()
    {
        $removeHandler(this._controlElement, 'mouseover', this._mouseOverHandlerDelegate);
        $removeHandler(this._controlElement, 'mouseout', this._mouseOutHandlerDelegate);

        $removeHandler(this._controlElement, 'click', this._clickHandlerDelegate);

        Likol.Web.UI.WebControls.ContextControl.callBaseMethod(this, "dispose");
    },
    click: function(evt)
    {
        ev = this.getMouseEvent(evt);

        var x = ev.clientX;
        var y = ev.clientY;

        var srcElement = ev.srcElement;

        var findInside = PopUpManager.FindInside(this._controlElement, srcElement);

        if(findInside) PopUpManager.SkipDocumentClick = true;

        //var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

        //if(this.containsPoint(controlBounds, x, y))
        //{
        //    PopUpManager.SkipDocumentClick = true;
        //}
    },
    mouseOver: function()
    {
        this._hasMouseOver = true;

        this._mouseOver = true;
    },
    mouseOut: function(evt)
    {
        this._mouseOver = false;

//        if (!this._hasMouseOver) return;

//        ev = this.getMouseEvent(evt);

//        var x = ev.clientX;
//        var y = ev.clientY;

//        var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

//        if(!this.containsPoint(controlBounds, x, y))
//        {
//            this.Close();
//        }
    },
    wait: function()
    {
        if (!PopUpManager.Current) return;

        if (PopUpManager.Current._hasMouseOver && !PopUpManager.Current._mouseOver)
        {
            PopUpManager.Current.Close();
        }
        else
        {
            PopUpManager.Current._hasMouseOver = true;

            PopUpManager.CurrentTimer = window.setTimeout(PopUpManager.Current.wait, 2000);
        }
    },
    SetSource: function(sourceControl)
    {
        this._sourceControl = sourceControl;
    },
    PopUp: function(sender, x, y)
    {
        if (PopUpManager.Current != null)
        {
            PopUpManager.Current.Close();
        }

        PopUpManager.SkipDocumentClick = true;

        if (!x || !y)
        {
            var senderBounds = Sys.UI.DomElement.getBounds(sender);

            x = senderBounds.x;
            y = senderBounds.y + senderBounds.height;
        }

        if (Sys.Browser.agent == Sys.Browser.InternetExplorer)
        {
            if(window != window.top)
            {
                x += -2;
                y += -2;
            }
        }

        with (this._controlElement)
        {
            style.left = x + "px";
            style.top = y + "px";
        
            style.display = "block";
        }

        PopUpManager.Current = this;

        if (this._enableWait)
            PopUpManager.CurrentTimer = window.setTimeout(PopUpManager.Current.wait, 2000);
    },
    Close: function()
    {
        window.clearTimeout(PopUpManager.CurrentTimer);

        PopUpManager.Current = null;

        with (this._controlElement)
        {
            style.display = "none";
        }

        if (this._sourceControl)
        {
            this._sourceControl.PopupClose();
        }

        this._hasMouseOver = false;
    },
    getMouseEvent: function (evt)
    {
        return window.event || evt; 
    },
    containsPoint: function(rect, x, y)
    {
        return x >= rect.x && x < (rect.x + rect.width) && y >= rect.y && y < (rect.y + rect.height);
    }
}

Likol.Web.UI.WebControls.ContextControl.registerClass('Likol.Web.UI.WebControls.ContextControl', Sys.UI.Control);

function PopUp(){}

PopUp = function()
{
    this.Current = null;
    this.SkipDocumentClick = false;
    this.CurrentTimer = null;

    this.documentClickHandlerDelegate = null;
}

PopUp.prototype.DocumentClick = function()
{
    if (PopUpManager.SkipDocumentClick)
    {
        PopUpManager.SkipDocumentClick = false;
        
        return;
    }
    
    if (PopUpManager.Current)
        PopUpManager.Current.Close();
}

PopUp.prototype.FindInside = function(popUpElement, srcElement)
{
    if (popUpElement && srcElement)
    {
        if (popUpElement.id == srcElement.id)
            return true;

        for (var i = 0; i < popUpElement.childNodes.length; i++)
        {
            var childElement = popUpElement.childNodes[i];
            
            if (childElement.id == srcElement.id)
                return true;
                
            var findInside = this.FindInside(childElement, srcElement);
            
            if (findInside)
                return true;
        }
    }
    
    return false;
}

var PopUpManager = new PopUp();

Sys.Application.add_init(popUpManagerInit);
Sys.Application.add_load(popUpManagerLoad);

function popUpManagerInit(sender, args)
{
    this.documentClickHandlerDelegate = null;

    PopUpManager.documentClickHandlerDelegate = Function.createDelegate(PopUpManager, PopUpManager.DocumentClick);

    $addHandler(document, 'click', PopUpManager.documentClickHandlerDelegate);
}

function popUpManagerLoad(sender, args)
{
    if (PopUpManager.Current)
        PopUpManager.Current.Close();
}