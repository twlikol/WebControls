﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class TabStripItem
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string CommandName { get; set; }
        public string ControlID { get; set; }
    }
}
