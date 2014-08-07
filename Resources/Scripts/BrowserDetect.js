
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.BrowserDetect = function(element)
{
    Likol.Web.UI.WebControls.BrowserDetect.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._iconElement = null;
    this._nameElement = null;
    this._versionElement = null;

    this._nameHiddenElement = null;
    this._versionHiddenElement = null;
}

Likol.Web.UI.WebControls.BrowserDetect.prototype =
{
    initialize: function()
    {
        Likol.Web.UI.WebControls.BrowserDetect.callBaseMethod(this, 'initialize');

        Sys.Browser.Chrome = {};
        if (navigator.userAgent.indexOf(' Chrome/') > -1) {
            Sys.Browser.agent = Sys.Browser.Chrome;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/ Chrome\/(\d+\.\d+)/)[1]);
            Sys.Browser.name = 'Chrome';
            Sys.Browser.hasDebuggerStatement = true;
        }

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._iconElement = $get(this._controlID + "_Icon");
        this._nameElement = $get(this._controlID + "_Name");
        this._versionElement = $get(this._controlID + "_Version");

        this._nameHiddenElement = $get(this._controlID + "_Name_Hidden");
        this._versionHiddenElement = $get(this._controlID + "_Version_Hidden");

        this._nameElement.innerHTML = Sys.Browser.name;
        this._versionElement.innerHTML = Sys.Browser.version;
    },
    dispose: function()
    {
        Likol.Web.UI.WebControls.BrowserDetect.callBaseMethod(this, "dispose");
    }
}

Likol.Web.UI.WebControls.BrowserDetect.registerClass('Likol.Web.UI.WebControls.BrowserDetect', Sys.UI.Control);