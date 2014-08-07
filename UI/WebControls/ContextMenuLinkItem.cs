using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using Likol.Web.UI;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class ContextMenuLinkItem : ContextMenuItem
    {
        private string navigateUrl;
        private string urlKey;

        private string dataNavigateUrlFields;
        private string dataNavigateUrlFormatString;

        #region public string NavigateUrl
        
        [
        Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor)),
        DefaultValue(""),
        Category("Likol")
        ]
        public string NavigateUrl
        {
            get { return this.navigateUrl; }
            set { this.navigateUrl = value; }
        }

        #endregion

        #region public string UrlKey
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string UrlKey
        {
            get { return this.urlKey; }
            set { this.urlKey = value; }
        }

        #endregion

        #region public string DataNavigateUrlFields
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string DataNavigateUrlFields
        {
            get { return this.dataNavigateUrlFields; }
            set { this.dataNavigateUrlFields = value; }
        }

        #endregion

        #region public string DataNavigateUrlFormatString
        
        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string DataNavigateUrlFormatString
        {
            get { return this.dataNavigateUrlFormatString; }
            set { this.dataNavigateUrlFormatString = value; }
        }

        #endregion

        #region public ContextMenuLinkItem()
        
        public ContextMenuLinkItem()
        {
            this.navigateUrl = "";
            this.urlKey = "";

            this.dataNavigateUrlFields = "";
            this.dataNavigateUrlFormatString = "";
        }

        #endregion

        #region public override void MakeClientClickScript(bool designMode, ContextMenu contextMenu, Panel panel)
        
        public override void MakeClientClickScript(bool designMode, ContextMenu contextMenu, Panel panel)
        {
            if (designMode) return;

            string linkUrl = contextMenu.ResolveUrl(this.navigateUrl);

            if (this.urlKey != "")
            {
                string urlFormatString = "";
                string urlKeyValue = contextMenu.Page.Request.QueryString[this.urlKey];

                if (linkUrl.IndexOf('?') != -1)
                {
                    urlFormatString = "${0}={1}";
                }
                else
                {
                    urlFormatString = "?{0}={1}";
                }

                linkUrl += string.Format(urlFormatString, this.urlKey, urlKeyValue);
            }

            panel.Attributes["onclick"] = string.Format("location.href='{0}'", linkUrl);
        }

        #endregion

        #region public override void MakeDataBindingData(bool designMode, ContextMenu contextMenu, Panel panel)
        
        public override void MakeDataBindingData(bool designMode, ContextMenu contextMenu, Panel panel)
        {
            if (designMode) return;

            if (this.dataNavigateUrlFields != "" && this.dataNavigateUrlFormatString != "")
            {
                string linkUrl = DataBindingUtil.GetNavigateUrlFormat(
                    contextMenu.NamingContainer, designMode,
                    this.dataNavigateUrlFields, this.dataNavigateUrlFormatString);

                panel.Attributes["onclick"] = string.Format("location.href='{0}'", linkUrl);
            }
        }

        #endregion
    }
}
