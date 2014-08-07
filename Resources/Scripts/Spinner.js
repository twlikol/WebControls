
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.Spinner = function(element)
{
    Likol.Web.UI.WebControls.Spinner.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._frontElement = null;
    this._backElement = null;

    this._pageRequestManager = null;

    this._beginRequestHandlerDelegate = null;
    this._endRequestHandlerDelegate = null;

    this._pageLoadedHandlerDelegate = null;
}

Likol.Web.UI.WebControls.Spinner.prototype =
{
    initialize: function()
    {
        Likol.Web.UI.WebControls.Spinner.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._frontElement = document.getElementById(this._controlID + "_Front");
        this._backElement = document.getElementById(this._controlID + "_Back");

        this._beginRequestHandlerDelegate = Function.createDelegate(this, this.handleBeginRequest);
    	this._endRequestHandlerDelegate = Function.createDelegate(this, this.handleEndRequest);

        this._pageLoadedHandlerDelegate = Function.createDelegate(this, this.handlePageLoaded);

    	if (Sys.WebForms && Sys.WebForms.PageRequestManager)
        {
           this._pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
    	}

    	if (this._pageRequestManager !== null )
        {
    	    this._pageRequestManager.add_beginRequest(this._beginRequestHandlerDelegate);
    	    this._pageRequestManager.add_endRequest(this._endRequestHandlerDelegate);

            this._pageRequestManager.add_pageLoaded(this._pageLoadedHandlerDelegate);
    	}
    },
    dispose: function()
    {
        if (this._pageRequestManager !== null )
        {
            this._pageRequestManager.remove_beginRequest(this._beginRequestHandlerDelegate);
    	    this._pageRequestManager.remove_endRequest(this._endRequestHandlerDelegate);

            this._pageRequestManager.remove_pageLoaded(this._pageLoadedHandlerDelegate);
        }

        Likol.Web.UI.WebControls.Spinner.callBaseMethod(this, "dispose");
    },
    handlePageLoaded: function()
    {
        
    },
    handleBeginRequest: function()
    {
        var windowWidth = Core.GetWindowsWidth();
        var windowHeight = Core.GetWindowsHeight();

        if (Sys.Browser.agent == Sys.Browser.InternetExplorer && Sys.Browser.version == 8)
        {

        }

        var top = 0;
        var left = 0;

        var frontBounds = Sys.UI.DomElement.getBounds(this._frontElement);

        left = (windowWidth / 2) - (frontBounds.width / 2);
        top = (windowHeight - frontBounds.height) / 2;

        with(this._frontElement)
        {
            style.position = 'absolute';
            style.zIndex = 102;

            style.top = top + 'px';
            style.left = left + 'px';
        }

        with(this._backElement)
        {
            style.position = 'absolute';
            style.top = '0px';
            style.left = '0px';
            style.zIndex = 101;

            style.width = windowWidth + "px";
            style.height = windowHeight + "px";
        }

        this._frontElement.style.display = "block";
        this._backElement.style.display = "block";
        this._controlElement.style.display = "block";
    },
    handleEndRequest: function()
    {
        if (Sys.Browser.agent == Sys.Browser.InternetExplorer && Sys.Browser.version == 8)
        {

        }

        this._frontElement.style.display = "none";
        this._backElement.style.display = "none";
        this._controlElement.style.display = "none";
    }
}

Likol.Web.UI.WebControls.Spinner.registerClass('Likol.Web.UI.WebControls.Spinner', Sys.UI.Control);