
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.SplitterPanel = function (element)
{
    Likol.Web.UI.WebControls.SplitterPanel.initializeBase(this, [element]);

    this._controlID = null;
    this._controlElement = null;

    this._masterElement = null;
    this._splitterElement = null;
    this._detailElement = null;

    this._masterContainerElement = null;
    this._detailContainerElement = null;

    this._splitterMouseOverHandlerDelegate = null;
    this._splitterMouseOutHandlerDelegate = null;
    this._splitterMouseDownHandlerDelegate = null;
    this._splitterMouseUpHandlerDelegate = null;

    this._bodyMouseMoveHandlerDelegate = null;
    this._bodyMouseUpHandlerDelegate = null;

    this._isMouseDown = false;

    this._currentClientX = 0;
    this._currentMasterWidth = 0;
    this._currentSplitWidth = 0;

    this._minMasterWidth = 0;
    this._minDetailWidth = 0;

    this._splitterCssClass = null;
    this._splitterMouseOverCssClass = null;

    this._resizeHandlerDelegate = null;

    this._panelType = null;
    this._sizeMode = null;

    this._removeElementIDs = null;
}

Likol.Web.UI.WebControls.SplitterPanel.prototype =
{
    get_splitterCssClass: function () { return this._splitterCssClass; },
    set_splitterCssClass: function (value) { this._splitterCssClass = value; },

    get_splitterMouseOverCssClass: function () { return this._splitterMouseOverCssClass; },
    set_splitterMouseOverCssClass: function (value) { this._splitterMouseOverCssClass = value; },

    get_panelType: function () { return this._panelType; },
    set_panelType: function (value) { this._panelType = value; },

    get_sizeMode: function () { return this._sizeMode; },
    set_sizeMode: function (value) { this._sizeMode = value; },

    get_removeElementIDs: function () { return this._removeElementIDs; },
    set_removeElementIDs: function (value) { this._removeElementIDs = value; },

    get_minMasterWidth: function () { return this._minMasterWidth; },
    set_minMasterWidth: function (value) { this._minMasterWidth = value; },

    get_minDetailWidth: function () { return this._minDetailWidth; },
    set_minDetailWidth: function (value) { this._minDetailWidth = value; },

    initialize: function ()
    {
        Likol.Web.UI.WebControls.SplitterPanel.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._masterElement = document.getElementById(this._controlID + "_Master");
        this._splitterElement = document.getElementById(this._controlID + "_Splitter");
        this._detailElement = document.getElementById(this._controlID + "_Detail");

        this._currentMasterWidth = Core.GetElementWidth(this._masterElement);
        this._currentSplitWidth = Core.GetElementWidth(this._splitterElement);

        this._masterContainerElement = document.getElementById(this._controlID + "_MasterContainer");
        this._detailContainerElement = document.getElementById(this._controlID + "_DetailContainer");

        this.handleResize();

        this._resizeHandlerDelegate = Function.createDelegate(this, this.handleResize);

        $addHandler(window, 'resize', this._resizeHandlerDelegate);

        this._splitterMouseOverHandlerDelegate = Function.createDelegate(this, this.handleSplitterMouseOver);
        this._splitterMouseOutHandlerDelegate = Function.createDelegate(this, this.handleSplitterMouseOut);
        this._splitterMouseDownHandlerDelegate = Function.createDelegate(this, this.handleSplitterMouseDown);
        this._splitterMouseUpHandlerDelegate = Function.createDelegate(this, this.handleSplitterMouseUp);

        this._bodyMouseMoveHandlerDelegate = Function.createDelegate(this, this.handleBodyMouseMove);
        this._bodyMouseUpHandlerDelegate = Function.createDelegate(this, this.handleSplitterMouseUp);

        $addHandler(this._splitterElement, 'mouseover', this._splitterMouseOverHandlerDelegate);
        $addHandler(this._splitterElement, 'mouseout', this._splitterMouseOutHandlerDelegate);
        $addHandler(this._splitterElement, 'mousedown', this._splitterMouseDownHandlerDelegate);
        $addHandler(this._splitterElement, 'mouseup', this._splitterMouseUpHandlerDelegate);

        $addHandler(document.body, 'mousemove', this._bodyMouseMoveHandlerDelegate);
        $addHandler(document.body, 'mouseup', this._bodyMouseUpHandlerDelegate);
    },
    dispose: function ()
    {
        $removeHandler(window, 'resize', this._resizeHandlerDelegate);

        $removeHandler(this._splitterElement, 'mouseover', this._splitterMouseOverHandlerDelegate);
        $removeHandler(this._splitterElement, 'mouseout', this._splitterMouseOutHandlerDelegate);
        $removeHandler(this._splitterElement, 'mousedown', this._splitterMouseDownHandlerDelegate);
        $removeHandler(this._splitterElement, 'mouseup', this._splitterMouseUpHandlerDelegate);

        $removeHandler(document.body, 'mousemove', this._bodyMouseMoveHandlerDelegate);
        $removeHandler(document.body, 'mouseup', this._bodyMouseUpHandlerDelegate);

        Likol.Web.UI.WebControls.SplitterPanel.callBaseMethod(this, "dispose");
    },
    handleResize: function ()
    {
        var panelWidth = 0;
        var panelHeight = 0;

        if (this._sizeMode == 0)
        {
            panelWidth = Core.GetWindowsWidth();
            panelHeight = Core.GetWindowsHeight();
        }
        else if (this._sizeMode == 1)
        {
            var parnetElement = this._controlElement.parentNode;

            panelWidth = Core.GetElementWidth(parnetElement);
            panelHeight = Core.GetElementHeight(parnetElement);
        }

        this._controlElement.style.width = panelWidth + 'px';

        var removeElementIDs = this._removeElementIDs.split(';');

        for(var i = 0; i < removeElementIDs.length; i++)
        {
            var element = document.getElementById(removeElementIDs[i]);

            if (!element) continue;

            panelHeight -= Core.GetElementHeight(element);
        }

        this._masterElement.style.height = panelHeight + "px";
        this._masterContainerElement.style.height = panelHeight + "px";

        this._detailElement.style.height = panelHeight + "px";
        this._detailContainerElement.style.height = panelHeight + "px";

        this._detailContainerElement.style.display = "none";

        var newDetailWidth = panelWidth - this._currentMasterWidth - this._currentSplitWidth;

        this._masterElement.style.width = this._currentMasterWidth + 'px';
        this._masterContainerElement.style.width = this._currentMasterWidth + 'px';

        this._detailElement.style.width = newDetailWidth + 'px';
        this._detailContainerElement.style.width = newDetailWidth + 'px';

        this._detailContainerElement.style.display = "block";
    },
    handleSplitterMouseOver: function ()
    {
        this._splitterElement.className = this._splitterMouseOverCssClass;
    },
    handleSplitterMouseOut: function ()
    {
        this._splitterElement.className = this._splitterCssClass;
    },
    handleSplitterMouseDown: function (evt)
    {
        evt = (evt) ? evt : event;

        this._currentClientX = evt.clientX;
        this._currentMasterWidth = Core.GetElementWidth(this._masterElement);

        this._isMouseDown = true;

        this._detailContainerElement.style.display = "none";

        document.body.style.cursor = 'e-resize';
    },
    handleSplitterMouseUp: function ()
    {
        this._isMouseDown = false;

        this._detailContainerElement.style.display = "block";

        document.body.style.cursor = 'auto';

        this._currentMasterWidth = Core.GetElementWidth(this._masterElement);
    },
    handleBodyMouseMove: function(evt)
    {
        if (!this._isMouseDown) return true;

        evt = (evt) ? evt : event;

        var panelWidth = Core.GetWindowsWidth();

        if (this._sizeMode == 1)
        {
            var parnetElement = this._controlElement.parentNode;

            panelWidth = Core.GetElementWidth(parnetElement);
        }

        var newClientX = evt.clientX;
        var movedSize = parseInt(newClientX - this._currentClientX);

        var newMasterWidth = parseInt(this._currentMasterWidth + movedSize);
        var newDetailWidth = panelWidth - newMasterWidth - this._currentSplitWidth;

        if (newMasterWidth >= this._minMasterWidth && newDetailWidth >= this._minDetailWidth)
        {
            this._masterElement.style.width = newMasterWidth + 'px';
            this._masterContainerElement.style.width = newMasterWidth + 'px';

            this._detailElement.style.width = newDetailWidth + 'px';
            this._detailContainerElement.style.width = newDetailWidth + 'px';
        }

        return false;
    }
}

Likol.Web.UI.WebControls.SplitterPanel.registerClass('Likol.Web.UI.WebControls.SplitterPanel', Sys.UI.Control);