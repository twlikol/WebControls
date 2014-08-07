using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    [Serializable]
    public class SearchKeyItem
    {
        private string key;
        private string text;
        private bool allowRemove;
        
        private string controlID;
        private string textProperty;
        private string valueProperty;

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        [
        DefaultValue(""),
        Category("Likol")
        ]
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        [
        DefaultValue(true),
        Category("Likol")
        ]
        public bool AllowRemove
        {
            get { return this.allowRemove; }
            set { this.allowRemove = value; }
        }

        [
        TypeConverter(typeof(ControlIDConverter)),
        DefaultValue(""),
        Category("Likol")
        ]
        public string ControlID
        {
            get { return this.controlID; }
            set { this.controlID = value; }
        }

        [
        TypeConverter("Likol.Design.Web.UI.WebControls.SearchKeyItemPropertyNameConverter"),
        DefaultValue(""),
        Category("Likol")
        ]
        public string TextProperty
        {
            get { return this.textProperty; }
            set { this.textProperty = value; }
        }

        [
        TypeConverter("Likol.Design.Web.UI.WebControls.SearchKeyItemPropertyNameConverter"),
        DefaultValue(""),
        Category("Likol")        
        ]
        public string ValueProperty
        {
            get { return this.valueProperty; }
            set { this.valueProperty = value; }
        }

        public SearchKeyItem()
        {
            this.key = "";
            this.text = "";
            this.allowRemove = true;

            this.controlID = "";
            this.textProperty = "";
            this.ValueProperty = "";
        }
    }
}
