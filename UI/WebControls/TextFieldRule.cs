using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    public class TextFieldRule
    {
        public string Value { get; set; }
        public string Text { get; set; }

        [
        DefaultValue(typeof(Color), "Black")
        ]
        public Color Color { get; set; }
    }
}
