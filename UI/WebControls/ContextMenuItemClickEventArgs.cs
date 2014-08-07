using System;
using System.Collections.Generic;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    public class ContextMenuItemClickEventArgs : EventArgs
    {
        private string text;

        private string commandName;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public string CommandName
        {
            get { return this.commandName; }
            set { this.commandName = value; }
        }

        public ContextMenuItemClickEventArgs()
        {
            this.text = "";

            this.commandName = "";
        }
    }
}
