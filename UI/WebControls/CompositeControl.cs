using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Likol.Web.UI.WebControls
{
    public abstract class CompositeControl : System.Web.UI.WebControls.CompositeControl
    {
        #region public string StringProperty(string propertyName)
        
        public string StringProperty(string propertyName)
        {
            object value = this.ViewState[propertyName];

            if (value != null)
            {
                return (string)value;
            }

            return string.Empty;
        }

        #endregion

        #region public string StringProperty(string propertyName, string defaultValue)
        
        public string StringProperty(string propertyName, string defaultValue)
        {
            object value = this.ViewState[propertyName];

            if (value != null)
            {
                return (string)value;
            }

            return defaultValue;
        }

        #endregion

        #region public bool BooleanProperty(string propertyName, bool defaultValue)
        
        public bool BooleanProperty(string propertyName, bool defaultValue)
        {
            object value = this.ViewState[propertyName];

            if (value != null)
            {
                return (bool)value;
            }

            return defaultValue;
        }

        #endregion

        #region public int IntProperty(string propertyName, int defaultValue)
        
        public int IntProperty(string propertyName, int defaultValue)
        {
            object value = this.ViewState[propertyName];

            if (value != null)
            {
                return (int)value;
            }

            return defaultValue;
        }

        #endregion

        #region public Unit UnitProperty(string propertyName, Unit defaultValue)
        
        public Unit UnitProperty(string propertyName, Unit defaultValue)
        {
            object value = this.ViewState[propertyName];

            if (value != null)
            {
                return (Unit)value;
            }

            return defaultValue;
        }

        #endregion
    }
}
