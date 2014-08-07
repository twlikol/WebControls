
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.WindowPanel = function(element)
{
    Likol.Web.UI.WebControls.WindowPanel.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._headerPanel = null;
    this._contentPanel = null;

    this._heightFix = null;
    this._zIndex = null;

    this._maxImageCell = null;
    this._minImageCell = null;

    this._maxImage = null;
    this._minImage = null;

    this._controlWidth = 0;
    this._controlHeight = 0;

    this._maxClickHandlerDelegate = null;
    this._minClickHandlerDelegate = null;

    this._resizeHandlerDelegate = null;

    this._initialize = false;
}

Likol.Web.UI.WebControls.WindowPanel.prototype =
{
    get_heightFix: function () { return this._heightFix; },
    set_heightFix: function (value) { this._heightFix = value; },

    get_zIndex: function () { return this._zIndex; },
    set_zIndex: function (value) { this._zIndex = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.WindowPanel.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._headerPanel = document.getElementById(this._controlID + "_Header");
        this._contentPanel = document.getElementById(this._controlID + "_Content");

        this._maxImageCell = document.getElementById(this._controlID + "_MaxCell");
        this._minImageCell = document.getElementById(this._controlID + "_MinCell");

        this._maxImage = document.getElementById(this._controlID + "_Max");
        this._minImage = document.getElementById(this._controlID + "_Min");

        this._minImageCell.style.display = "none";

        var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

        this._controlWidth = controlBounds.width;
        this._controlHeight = controlBounds.height;

        this._controlElement.style.width = this._controlWidth + "px";
        this._controlElement.style.height = this._controlHeight + "px";

        this.handleResize();

        var cookieName = document.location.href + "-" + this._controlID;
        var sizeMax = Core.GetCookie(cookieName);

        if (sizeMax == "1") this.maxClick();

        this._maxClickHandlerDelegate = Function.createDelegate(this, this.maxClick);
        this._minClickHandlerDelegate = Function.createDelegate(this, this.minClick);
        this._resizeHandlerDelegate = Function.createDelegate(this, this.handleResize);

        $addHandler(this._maxImage, 'click', this._maxClickHandlerDelegate);
        $addHandler(this._minImage, 'click', this._minClickHandlerDelegate);
        $addHandler(window, 'resize', this._resizeHandlerDelegate);

        this._initialize = true;
    },
    dispose: function()
    {
        $removeHandler(this._maxImage, 'click', this._maxClickHandlerDelegate);
        $removeHandler(this._minImage, 'click', this._minClickHandlerDelegate);
        $removeHandler(window, 'resize', this._resizeHandlerDelegate);

        Likol.Web.UI.WebControls.WindowPanel.callBaseMethod(this, "dispose");
    },
    handleResize: function()
    {
        var windowWidth = Core.GetWindowsWidth();
        var windowHeight = Core.GetWindowsHeight();

        if (windowWidth < this._controlWidth || windowHeight < this._controlHeight)
        {
            this.maxClick();
        }

        this.resizeWindow();
        this.resetPosition();
    },
    resetPosition: function()
    {
        var windowWidth = Core.GetWindowsWidth();
        var windowHeight = Core.GetWindowsHeight();

        var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

        var left = 0;
        var top = 0;

        if (windowWidth > controlBounds.width && windowHeight > controlBounds.height)
        {
            left = (windowWidth - controlBounds.width) / 2;
            top = (windowHeight - controlBounds.height) / 2;
        }

        this._controlElement.style.left = left + "px";
        this._controlElement.style.top = top + "px";
        this._controlElement.style.zIndex = this._zIndex;
    },
    resizeWindow: function()
    {
        this._contentPanel.style.display = "none";

        var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

        var headerWidth = Core.GetElementWidth(this._headerPanel);
        var headerHeight = Core.GetElementHeight(this._headerPanel);

        this._contentPanel.style.width = headerWidth + "px";
        this._contentPanel.style.height = (controlBounds.height - headerHeight - this._heightFix) + "px";

        this._contentPanel.style.display = "block";

        WindowsManager.EnableScreen(this._controlID);

        if (this._initialize) LayoutManager.Resize();
    },
    maxClick: function()
    {
        var windowWidth = Core.GetWindowsWidth();
        var windowHeight = Core.GetWindowsHeight();

        var controlBounds = Sys.UI.DomElement.getBounds(this._controlElement);

        this._maxImageCell.style.display = "none";
        this._minImageCell.style.display = "block";

        this._controlElement.style.width = (windowWidth - 10) + "px";
        this._controlElement.style.height = (windowHeight - 10) + "px";

        this.resizeWindow();
        this.resetPosition();

        var cookieName = document.location.href + "-" + this._controlID;

        Core.CreateCookie(cookieName, "1", 1);
    },
    minClick: function()
    {
        this._maxImageCell.style.display = "block";
        this._minImageCell.style.display = "none";

        var windowWidth = Core.GetWindowsWidth();
        var windowHeight = Core.GetWindowsHeight();

        if (windowWidth < this._controlWidth || windowHeight < this._controlHeight)
        {
            return;
        }

        this._controlElement.style.width = this._controlWidth + "px";
        this._controlElement.style.height = this._controlHeight + "px";

        this.resizeWindow();
        this.resetPosition();

        var cookieName = document.location.href + "-" + this._controlID;

        Core.DeleteCookie(cookieName);
    }
}

Likol.Web.UI.WebControls.WindowPanel.registerClass('Likol.Web.UI.WebControls.WindowPanel', Sys.UI.Control);

function Windows(){}

Windows = function()
{
    this.targetElementID = null;
    this.screenElement = null;
}

Windows.prototype.EnableScreen = function(targetElementID)
{
    this.targetElementID = targetElementID;

    document.body.style.overflow = "hidden";

    var windowWidth = Core.GetWindowsWidth();
    var windowHeight = Core.GetWindowsHeight();

    if(!WindowsManager.screenElement)
    {
        WindowsManager.screenElement = document.createElement("DIV");

        document.body.appendChild(WindowsManager.screenElement);
        
        with(WindowsManager.screenElement)
        {
            style.backgroundColor = "#EFEFEF";
            style.display = 'none';
            style.position = 'absolute';
            style.top = '0px';
            style.left = '0px';
            style.filter = "alpha(opacity=70); BACKGROUND-COLOR: #EFEFEF";
            style.opacity = "0.7";
            style.zIndex = 99;
        }

        WindowsManager.screenElement.onmousedown = function () { return false; }
    }

    with(WindowsManager.screenElement)
    {
        style.width = windowWidth + "px";
        style.height = windowHeight + "px";
    }

    WindowsManager.screenElement.style.display = "block";
}

Windows.prototype.DisableScreen = function()
{
    document.body.style.overflow = "auto";

    WindowsManager.screenElement.style.display = "none";
}

var WindowsManager = new Windows();

Sys.Application.add_load(windowsManagerLoad);

function windowsManagerLoad(sender, args)
{
    var targetElement = document.getElementById(WindowsManager.targetElementID);

    if (!targetElement)
    {
        WindowsManager.DisableScreen();

        LayoutManager.Resize();
    }
}