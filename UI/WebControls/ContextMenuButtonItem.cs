using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class ContextMenuButtonItem : ContextMenuItem
    {
        private bool causesValidation;
        private string validationGroup;

        private string onClientClick;

        #region public bool CausesValidation
        
        [
        DefaultValue(true),
        Category("Likol")
        ]
        public bool CausesValidation
        {
            get { return this.causesValidation; }
            set { this.causesValidation = value; }
        }

        #endregion

        #region public string ValidationGroup
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string ValidationGroup
        {
            get { return this.validationGroup; }
            set { this.validationGroup = value; }
        }

        #endregion

        #region public string OnClientClick
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string OnClientClick
        {
            get { return this.onClientClick; }
            set { this.onClientClick = value; }
        }

        #endregion

        #region public ContextMenuButtonItem()
        
        public ContextMenuButtonItem()
        {
            this.causesValidation = true;
            this.validationGroup = "";

            this.onClientClick = "";
        }

        #endregion

        #region public override void MakeClientClickScript(bool designMode, ContextMenu contextMenu, Panel panel)
        
        public override void MakeClientClickScript(bool designMode, ContextMenu contextMenu, Panel panel)
        {
            string postBack = this.onClientClick;

            string eventArgument = string.Format("{0}_{1}_{2}",
                this.causesValidation.ToString().ToLower(), this.text, this.commandName);

            if (this.causesValidation)
            {
                PostBackOptions postBackOptions = new PostBackOptions(
                    contextMenu, eventArgument);

                postBackOptions.ValidationGroup = this.validationGroup;
                postBackOptions.PerformValidation = true;

                postBack += contextMenu.Page.ClientScript.GetPostBackEventReference(
                    postBackOptions, false);
            }
            else
            {
                postBack += contextMenu.Page.ClientScript.GetPostBackEventReference(
                    contextMenu, eventArgument);
            }

            panel.Attributes["onclick"] = postBack;
        }

        #endregion
    }
}
