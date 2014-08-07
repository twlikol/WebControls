
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

    this._parnetElement = null;

    this._sizeMode = null;
    this._parentLevel = null;
    this._removeElementIDs = null;

    this._scrollHandlerDelegate = null;
    this._resizeHandlerDelegate = null;

    this._copyHeaderRow = null;

    this._floatHeaderTableElement = null;
    this._floatHeaderRow = null;

    this._floatItemTableElement = null;
    this._floatItemRow = null;

    this._floatScrollHandlerDelegate = null;

    this._freezeColumns = 0;
    this._freezeHeaderCssClass = "";
    this._freezeItemCssClass = "";
}

Likol.Web.UI.WebControls.ScrollGridView.prototype =
{
    get_sizeMode: function () { return this._sizeMode; },
    set_sizeMode: function (value) { this._sizeMode = value; },

    get_parentLevel: function () { return this._parentLevel; },
    set_parentLevel: function (value) { this._parentLevel = value; },

    get_removeElementIDs: function() { return this._removeElementIDs; },
    set_removeElementIDs: function(value) { this._removeElementIDs = value; },

    get_freezeColumns: function() { return this._freezeColumns; },
    set_freezeColumns: function(value) { this._freezeColumns = value; },

    get_freezeHeaderCssClass: function() { return this._freezeHeaderCssClass; },
    set_freezeHeaderCssClass: function(value) { this._freezeHeaderCssClass = value; },

    get_freezeItemCssClass: function() { return this._freezeItemCssClass; },
    set_freezeItemCssClass: function(value) { this._freezeItemCssClass = value; },

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

        this._floatHeaderElement = $get(this._controlElement.id + "_Float_Header");
        this._floatHeaderElement.style.position = "absolute";

        this._floatHeaderTableElement = $get(this._controlElement.id + "_Float_Header_Table");
        
        this._floatItemElement = $get(this._controlElement.id + "_Float_Item");
        this._floatItemElement.style.position = "absolute";

        if (Sys.Browser.agent == Sys.Browser.Firefox)
        {
            this._headerTableElement.style.height = "34px";
            this._floatHeaderTableElement.style.height = "34px";
        }

        if (this._itemTableElement.rows.length != 1)
        {
            this._copyHeaderRow = this._itemTableElement.rows[0].cloneNode(true);
            this._floatHeaderRow = this._itemTableElement.rows[0].cloneNode(true);            

            if (this._headerTableElement.childNodes[0] &&
                this._headerTableElement.childNodes[0].tagName == "TBODY")
            {
                this._headerTableElement.childNodes[0].appendChild(this._copyHeaderRow);

                this._floatHeaderTableElement.childNodes[0].appendChild(this._floatHeaderRow);
            }
            else
            {
                this._headerTableElement.appendChild(this._copyHeaderRow);

                this._floatHeaderTableElement.appendChild(this._floatHeaderRow);
            }

            this._floatItemTableElement = this._itemTableElement.cloneNode(true);
            this._floatItemTableElement.id = "temp";
            this._floatItemTableElement.control = null;

            this._floatItemElement.appendChild(this._floatItemTableElement);

            this._floatItemTableElement.style.width = null;

            for(var i = this._floatHeaderRow.cells.length - 1; i >= this._freezeColumns; i--)
            {
                this._floatHeaderRow.deleteCell(i);
            }

            for(var i = 0; i < this._floatItemTableElement.rows.length; i++)
            {
                var floatRow = this._floatItemTableElement.rows[i];

                for(var j = floatRow.cells.length - 1; j >= this._freezeColumns; j--)
                {
                    floatRow.deleteCell(j);
                }

                for(var k = 0; k < this._freezeColumns; k++)
                {
                    floatRow.cells[k].style.backgroundColor = "#EFEFEF";
                }
                
                if (i == 1) this._floatItemRow = floatRow;
            }

            for(var i = 0; i < this._freezeColumns; i++)
            {
                this._floatHeaderRow.cells[i].style.backgroundColor = "#EFEFEF";
            }
        }

        this._itemElementHeight = Core.GetElementHeight(this._itemElement);

        this._parnetElement = this._controlElement;

        for(var i = 0; i < this._parentLevel; i++)
        {
            this._parnetElement = this._parnetElement.parentNode;
        }

        this.handleResize();

        this._scrollHandlerDelegate = Function.createDelegate(this, this.handleScroll);
        this._resizeHandlerDelegate = Function.createDelegate(this, this.handleResize);
        
        $addHandler(this._itemElement, 'scroll', this._scrollHandlerDelegate);
        $addHandler(window, 'resize', this._resizeHandlerDelegate);

        if (this._freezeColumns != 0)
        {
            this._floatScrollHandlerDelegate = Function.createDelegate(this, this.handleScroll);
            $addHandler(this._floatItemElement, 'scroll', this._floatScrollHandlerDelegate);
        }

        LayoutManager.Subscribe(this._controlID);
    },
    dispose: function()
    {
        $removeHandler(this._itemElement, 'scroll', this._scrollHandlerDelegate);
        $removeHandler(window, 'resize', this._resizeHandlerDelegate);

        if (this._freezeColumns != 0)
            $removeHandler(this._floatItemElement, 'scroll', this._floatScrollHandlerDelegate);

        Likol.Web.UI.WebControls.ScrollGridView.callBaseMethod(this, "dispose");
    },
    handleResize: function()
    {
        var bodyOverflow = document.body.style.overflow;

        document.body.style.overflow = "hidden";

        this.handleHeightResize();

        this.handleWidthResize();

        this.handleScroll();

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

        if (this._freezeColumns == 0) return;

        var scrollbarHeight = 17;

        if (Sys.Browser.agent == Sys.Browser.InternetExplorer && Sys.Browser.version == 8)
        {
            scrollbarHeight = 16;
        }

        this._floatItemElement.style.height = (itemElementHeight - scrollbarHeight) + "px";
    },
    handleWidthResize: function()
    {
        if (this._itemTableElement.rows.length == 1) return;

        if (this._sizeMode == 1)
        {
            var parentElementWidth = Core.GetElementWidth(this._parnetElement);

            this._headerElement.style.width = parentElementWidth + "px";
            this._itemElement.style.width = parentElementWidth + "px";
        }

        this._itemTableElement.rows[0].style.display = "";

        var cellCount = this._itemTableElement.rows[0].cells.length;

        for(var i = 0 ; i < cellCount; i++)
        {
            var headerCellDiv = this._itemTableElement.rows[0].cells[i].childNodes[0];
            var itemCellDiv = this._itemTableElement.rows[1].cells[i].childNodes[0];

            headerCellDiv.style.width = null;
            itemCellDiv.style.width = null;
        }

        for(var i = 0 ; i < cellCount; i++)
        {
            var headerCell = this._itemTableElement.rows[0].cells[i];
            var itemCell = this._itemTableElement.rows[1].cells[i];

            var headerCellDiv = this._itemTableElement.rows[0].cells[i].childNodes[0];
            var itemCellDiv = this._itemTableElement.rows[1].cells[i].childNodes[0];

            var headerCellDivBounds = Sys.UI.DomElement.getBounds(headerCellDiv);
            var itemCellDivBounds = Sys.UI.DomElement.getBounds(itemCellDiv);

            if (headerCellDivBounds.width > itemCellDivBounds.width)
            {
                var maxWidth = (headerCellDivBounds.width + 0) + "px";

                headerCellDiv.style.width = maxWidth;
                itemCellDiv.style.width = maxWidth;

                this._copyHeaderRow.cells[i].childNodes[0].style.width = maxWidth;
            }
            else
            {
                var maxWidth = (itemCellDivBounds.width + 0) + "px";

                headerCellDiv.style.width = maxWidth;
                itemCellDiv.style.width = maxWidth;

                this._copyHeaderRow.cells[i].childNodes[0].style.width = maxWidth;
            }
        }

        for(var i = 0 ; i < this._freezeColumns; i++)
        {
            // [BUG] Rows must have 3+
            var headerCellDiv2 = this._itemTableElement.rows[3].cells[i].childNodes[0];

            var headerCellDiv2Width = headerCellDiv2.offsetWidth;

            var maxWidth2 = (headerCellDiv2Width + 0) + "px";

            this._floatHeaderRow.cells[i].childNodes[0].style.width = maxWidth2;
            this._floatItemRow.cells[i].childNodes[0].style.width = maxWidth2;
        }

        this._itemTableElement.rows[0].style.display = "none";

        this._floatItemTableElement.rows[0].style.display = "none";
    },
    handleScroll: function()
    {
        this._headerElement.scrollLeft = this._itemElement.scrollLeft;
        this._floatItemElement.scrollTop = this._itemElement.scrollTop;
    }
}

Likol.Web.UI.WebControls.ScrollGridView.registerClass('Likol.Web.UI.WebControls.ScrollGridView', Sys.UI.Control);