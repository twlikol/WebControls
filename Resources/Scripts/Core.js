
function Core(){}

Core.GetWindowsHeight = function()
{
    var _winHeight = 0;

    if (window.innerHeight) _winHeight = window.innerHeight;
    else if (document.documentElement.clientHeight) _winHeight = document.documentElement.clientHeight;
    else if (document.body.offsetHeight) _winHeight = document.body.offsetHeight;

    if (_winHeight == 0) return _winHeight;

    if (Sys.Browser.agent == Sys.Browser.InternetExplorer && Sys.Browser.version == 9)
    {
        _winHeight = _winHeight - 1;
    }
    
    return _winHeight;
}

Core.GetWindowsWidth = function()
{
    if (window.innerWidth) return window.innerWidth;
    else if (document.documentElement.clientWidth) return document.documentElement.clientWidth;
    else if (document.body.offsetWidth) return document.body.offsetWidth;
    else return _winWidth;
}

Core.GetElementWidth = function(element)
{
    var elementBounds = Sys.UI.DomElement.getBounds(element);
    
    return parseInt(elementBounds.width);
}

Core.GetElementHeight = function(element)
{
    var elementBounds = Sys.UI.DomElement.getBounds(element);
    
    return parseInt(elementBounds.height);
}

Core.CreateCookie = function(name, value, days)
{
    if (days)
    {
        var date = new Date();
        date.setTime(date.getTime()+(days*24*60*60*1000));

        var expires = ""; 

        expires = "" + date.toGMTString();
    }
    else var expires = "";

    document.cookie = name + "=" + value; + "; expires=" + expires;
}

Core.GetCookie = function(name)
{
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;

    var i = 0;

    while (i < clen)
    {
        var j = i + alen;

        if (document.cookie.substring(i, j) == arg)
            return Core.GetCookieValue(j);

        i = document.cookie.indexOf(" ", i) + 1;

        if (i == 0) break;
    }

    return null;
}

Core.GetCookieValue = function(offset)
{
    var endstr = document.cookie.indexOf(";", offset);

    if (endstr == -1)
        endstr = document.cookie.length;

    return unescape(document.cookie.substring(offset, endstr));
}

Core.DeleteCookie = function(name)
{
    Core.CreateCookie(name,"",-1);
}

function Dialog(){}

Dialog.Open = function(pageUrl, width, height, targetElementID)
{
    var parentWindow = window;
    var targetElement = document.getElementById(targetElementID);
    
    var windowWidth = window.screen.width
    
    var perfectTop = (window.screen.height - height - 30) / 2;
    var perfectLeft = (window.screen.width - width) / 2;
    
    if (pageUrl.indexOf('?') == -1)
        pageUrl += "?DateTime=" + new Date().toUTCString();
    else
        pageUrl += "&DateTime=" + new Date().toUTCString();
    
    var targetValue = null;
    
    if (targetElement)
    {
        targetValue = targetElement.value;

        window.dialogArguments = targetElement.value;
    }

    var option = "scroll=no" + 
        ";dialogWidth:" + width + "px" +
        ";dialogHeight:" + height + "px" +
        //";dialogTop:" + perfectTop +
        //";dialogLeft:" + perfectLeft +
        ";resizable:yes"
        "";
        
    var returnValue = parentWindow.showModalDialog(pageUrl, targetValue, option);

    if (window.returnValue)
    {
        returnValue = window.returnValue;

        window.returnValue = null;
    }
        
    if (returnValue)
    {
        if (targetElement) targetElement.value = returnValue;
    }

    return returnValue;
}

function Layout(){}

Layout = function()
{
    this.subscribes = new Array();
}

Layout.prototype.Subscribe = function(subscribeID)
{
    var hasSubscribe = false;

    for(var i = 0; i < this.subscribes.length; i++)
    {
        if (this.subscribes[i] == subscribeID)
        {
            hasSubscribe = true;
        }
    }    

    if (hasSubscribe == false)
        this.subscribes[this.subscribes.length] = subscribeID;
}

Layout.prototype.Resize = function()
{
    if (this.subscribes.length == 0) return;

    for(var i = 0; i < this.subscribes.length; i++)
    {
        var control = $find(this.subscribes[i]);

        if (control) control.handleResize();
    }
}

var LayoutManager = new Layout();