using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web.UI.WebControls
{
    public class NavigationBarMenuItemClickEventArgs : EventArgs
    {
        private string commandName;
        private string controlPath;

        public string CommandName
        {
            get { return this.commandName; }
            set { this.commandName = value; }
        }

        public string ControlPath
        {
            get { return this.controlPath; }
            set { this.controlPath = value; }
        }

        public NavigationBarMenuItemClickEventArgs()
        {
            this.controlPath = "";
            this.commandName = "";
        }
    }
}
