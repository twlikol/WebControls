using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class ContextMenuSplitItem : ContextMenuItem
    {
        [
        Browsable(false)
        ]
        public override string ImageUrl
        {
            get { return base.ImageUrl; }
            set { base.ImageUrl = value; }
        }

        [
        Browsable(false),
        DefaultValue("----------")
        ]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [
        Browsable(false)
        ]
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }

        [
        Browsable(false)
        ]
        public override string CommandName
        {
            get { return base.CommandName; }
            set { base.CommandName = value; }
        }

        [
        Browsable(false)
        ]
        public override bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public ContextMenuSplitItem()
        {
            this.Text = "----------";
        }
    }
}
