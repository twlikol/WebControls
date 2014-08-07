using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using Likol.Web.Resources;

[assembly: WebResource("Likol.Web.Resources.Scripts.Spinner.js", "application/x-javascript")]

[assembly: WebResource("Likol.Web.Resources.Images.Spinner.gif", "image/gif")]

namespace Likol.Web.UI.WebControls
{
    [
    ToolboxData("<{0}:Spinner runat=server></{0}:Spinner>")
    ]
    public class Spinner : CompositeControl, IScriptControl
    {
        #region protected override HtmlTextWriterTag TagKey

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

        #region public string ImageUrl
        
        [
        Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor)),
        DefaultValue("")
        ]
        public string ImageUrl
        {
            get { return this.StringProperty("ImageUrl"); }
            set { this.ViewState["ImageUrl"] = value; }
        }

        #endregion

        #region public string Text
        
        [
        DefaultValue("")
        ]
        public string Text
        {
            get { return this.StringProperty("Text"); }
            set { this.ViewState["Text"] = value; }
        }

        #endregion

        #region public string FrontCssClass

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string FrontCssClass
        {
            get { return this.StringProperty("FrontCssClass"); }
            set { this.ViewState["FrontCssClass"] = value; }
        }

        #endregion

        #region public string BackCssClass

        [
        Category("Likol"),
        DefaultValue("")
        ]
        public string BackCssClass
        {
            get { return this.StringProperty("BackCssClass"); }
            set { this.ViewState["BackCssClass"] = value; }
        }

        #endregion

        public Spinner()
        {
            Global.TimeIsValid();
        }

        #region protected override void CreateChildControls()
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Literal ltText = new Literal();
            ltText.Text = this.Text;

            Image imgIcon = new Image();

            string imageUrl = this.ResolveClientUrl(this.ImageUrl);

            if (imageUrl == "")
                imageUrl = ResourceManager.GetImageWebResourceUrl(this, "Spinner");

            imgIcon.ImageUrl = imageUrl;

            TableCell tableCellImage = new TableCell();
            tableCellImage.Controls.Add(imgIcon);

            TableCell tableCellText = new TableCell();
            tableCellText.Controls.Add(ltText);

            TableRow tableRowSpinner = new TableRow();
            tableRowSpinner.Cells.Add(tableCellImage);
            tableRowSpinner.Cells.Add(tableCellText);

            Table tableSpinner = new Table();
            tableSpinner.CellSpacing = 0;
            tableSpinner.CellPadding = 3;
            tableSpinner.ID = "Front";
            tableSpinner.Style["position"] = "absolute";
            tableSpinner.Style["display"] = "none";
            tableSpinner.CssClass = this.FrontCssClass;
            tableSpinner.Rows.Add(tableRowSpinner);

            this.Controls.Add(tableSpinner);

            Panel panelBack = new Panel();
            panelBack.ID = "Back";
            panelBack.Style["position"] = "absolute";
            panelBack.Style["display"] = "none";
            panelBack.CssClass = this.BackCssClass;
            panelBack.Controls.Add(new LiteralControl("&nbsp;"));

            this.Controls.Add(panelBack);
        }

        #endregion

        #region protected override void OnPreRender(EventArgs e)

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.Style["display"] = "none";

            if (!this.DesignMode)
            {
                ResourceManager.RegisterCoreScriptResource(this.Page);

                ScriptManager.GetCurrent(this.Page).RegisterScriptControl(this);
            }
        }

        #endregion

        #region protected override void Render(HtmlTextWriter writer)

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);

            base.Render(writer);
        }

        #endregion

        #region public IEnumerable<ScriptDescriptor> GetScriptDescriptors()

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor =
                new ScriptControlDescriptor("Likol.Web.UI.WebControls.Spinner",
                    this.ClientID);

            //descriptor.AddProperty("selectedIndex", this.SelectedIndex);

            return new ScriptDescriptor[] { descriptor };
        }

        #endregion

        #region public IEnumerable<ScriptReference> GetScriptReferences()

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference scriptReference = ResourceManager.GetScriptReference("Spinner");
            return new ScriptReference[] { scriptReference };
        }

        #endregion
    }
}
