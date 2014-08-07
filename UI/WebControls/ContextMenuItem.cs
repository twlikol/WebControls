using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public abstract class ContextMenuItem
    {
        protected string commandName;

        protected string imageUrl;
        protected string text;
        protected string description;
        protected bool visible;

        #region public virtual string CommandName
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public virtual string CommandName
        {
            get { return this.commandName; }
            set { this.commandName = value; }
        }

        #endregion

        #region public virtual string ImageUrl
        
        [
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor)),
        DefaultValue(""),
        Category("Likol")
        ]
        public virtual string ImageUrl
        {
            get { return this.imageUrl; }
            set { this.imageUrl = value; }
        }

        #endregion

        #region public virtual string Text
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public virtual string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        #endregion

        #region public virtual string Description
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public virtual string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        #endregion

        #region public virtual bool Visible
        
        [
        DefaultValue(true),
        Category("Likol")
        ]
        public virtual bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        #endregion

        #region public ContextMenuItem()
        
        public ContextMenuItem()
        {
            this.commandName = "";

            this.imageUrl = "";
            this.text = "";
            this.description = "";
            this.visible = true;
        }

        #endregion

        public virtual void MakeClientClickScript(bool designMode, ContextMenu contextMenu, Panel panel)
        {
        
        }

        public virtual void MakeDataBindingData(bool designMode, ContextMenu contextMenu, Panel panel)
        {

        }
    }
}
