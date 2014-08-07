
Type.registerNamespace('Likol.Web.UI.WebControls');

Likol.Web.UI.WebControls.ScrollGridView = function(element)
{
    Likol.Web.UI.WebControls.ScrollGridView.initializeBase(this, [element]);

    this._controlElement = null;
    this._controlID = null;

    this._headerElement = null;
    this._headerTableElement = null;
    
    this._itemElement = null;
    this._itemTableElement = null;

    this._itemElementHeight = null;

    this._sizeMode = null;
    this._parentLevel = null;
    this._removeElementIDs = null;

    this._scrollHandlerDelegate = null;
    this._resizeHandlerDelegate = null;

    this._copyHeaderRow = null;
}

Likol.Web.UI.WebControls.ScrollGridView.prototype =
{
    get_sizeMode: function () { return this._sizeMode; },
    set_sizeMode: function (value) { this._sizeMode = value; },

    get_parentLevel: function () { return this._parentLevel; },
    set_parentLevel: function (value) { this._parentLevel = value; },

    get_removeElementIDs: function() { return this._removeElementIDs; },
    set_removeElementIDs: function(value) { this._removeElementIDs = value; },

    initialize: function()
    {
        Likol.Web.UI.WebControls.ScrollGridView.callBaseMethod(this, 'initialize');

        this._controlElement = this.get_element();
        this._controlID = this._controlElement.id;

        this._controlElement.parentNode.style.zoom = 1;

        this._headerElement = $get(this._controlElement.id + "_Header");
        this._headerTableElement = $get(this._controlElement.id + "_Header_Table");

        this._itemElement = $get(this._controlElement.id + "_Item");
        this._itemTableElement = this._controlElement;

        if (Sys.Browser.agent == Sys.Browser.Firefox)
        {
            this._headerTableElement.style.height = "34px";
        }

        if (this._itemTableElement.rows.length != 1)
        {
            this._copyHeaderRow = this._itemTableElement.rows[0].cloneNode(true);

            if (this._headerTableElement.childNodes[0] &&
                this._headerTableElement.childNodes[0].tagName == "TBODY")
            {
                this._headerTableElement.childNodes[0].appendChild(this._copyHeaderRow);
            }
            else
            {
                this._headerTableElement.appendChild(this._copyHeaderRow);
            }
        }

        this._itemElementHeight = Core.GetElementHeight(this._itemElement);

        this.handleResize();

        this._scrollHandlerDelegate = Function.createDelegate(this, this.handleScroll);
        this._resizeHandlerDelegate = Function.createDelegate(this, this.handleResize);
        
        $addHandler(this._itemElement, 'scroll', this._scrollHandlerDelegate);
        $addHandler(window, 'resize', this._resizeHandlerDelegate);

        LayoutManager.Subscribe(this._controlID);
    },
    dispose: function()
    {
        $removeHandler(this._itemElement, 'scroll', this._scrollHandlerDelegate);
        $removeHandler(window, 'resize', this._resizeHandlerDelegate);

        Likol.Web.UI.WebControls.ScrollGridView.callBaseMethod(this, "dispose");
    },
    handleResize: function()
    {
        var bodyOverflow = document.body.style.overflow;

        document.body.style.overflow = "hidden";

        this.handleHeightResize();

        this.handleWidthResize();

        document.body.style.overflow = bodyOverflow;
    },
    handleHeightResize: function()
    {
        var itemElementHeight = Core.GetWindowsHeight();

        if (this._sizeMode == 2)
        {
            itemElementHeight = this._itemElementHeight;
        }

        if (this._removeElementIDs != "")
        {
            var removeElementIDs = this._removeElementIDs.split(';');

            for(var i = 0; i < removeElementIDs.length; i++)
            {
                var element = document.getElementById(removeElementIDs[i]);

                if (!element) continue;

                itemElementHeight -= Core.GetElementHeight(element);
            }
        }

        itemElementHeight -= Core.GetElementHeight(this._headerElement);

        this._itemElement.style.height = itemElementHeight + "px";
    },
    handleWidthResize: function()
    {
        if (this._itemTableElement.rows.length == 1) return;

        this._itemTableElement.rows[0].style.display = "";

        var cellCount = this._itemTableElement.rows[0].cells.length;

        for(var i = 0 ; i <= cellCount - 1; i++)
        {
            var headerCellDiv = this._itemTableElement.rows[0].cells[i].childNodes[0];
            var itemCellDiv = this._itemTableElement.rows[1].cells[i].childNodes[0];

            headerCellDiv.style.width = null;
            itemCellDiv.style.width = null;
        }

        for(var i = 0 ; i <= cellCount - 1; i++)
        {
            var headerCellDiv = this._itemTableElement.rows[0].cells[i].childNodes[0];
            var itemCellDiv = this._itemTableElement.rows[1].cells[i].childNodes[0];

            var headerCellBounds = Sys.UI.DomElement.getBounds(headerCellDiv);
            var itemCellBounds = Sys.UI.DomElement.getBounds(itemCellDiv);

            if (headerCellBounds.width > itemCellBounds.width)
            {
                var maxWidth = (headerCellBounds.width + 0) + "px";

                headerCellDiv.style.width = maxWidth;
                itemCellDiv.style.width = maxWidth;

                this._copyHeaderRow.cells[i].childNodes[0].style.width = maxWidth;
            }
            else
            {
                var maxWidth = (itemCellBounds.width + 0) + "px";

                headerCellDiv.style.width = maxWidth;
                itemCellDiv.style.width = maxWidth;

                this._copyHeaderRow.cells[i].childNodes[0].style.width = maxWidth;
            }
        }

        this._itemTableElement.rows[0].style.display = "none";
    },
    handleScroll: function()
    {
        this._headerElement.scrollLeft = this._itemElement.scrollLeft;
    }
}

Likol.Web.UI.WebControls.ScrollGridView.registerClass('Likol.Web.UI.WebControls.ScrollGridView', Sys.UI.Control);