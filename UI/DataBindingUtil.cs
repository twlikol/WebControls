using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Likol.Web.UI
{
    internal static class DataBindingUtil
    {
        #region internal static string GetDataItemValue(Control control, bool designMode, string fieldName)
        
        internal static string GetDataItemValue(Control control, bool designMode, string fieldName)
        {
            if (designMode) return "¸ê®ÆÃ´µ²";

            object dataItem = DataBinder.GetDataItem(control);

            return DataBinder.Eval(dataItem, fieldName).ToString();
        }

        #endregion

        #region internal static string GetNavigateUrlFormat(Control control, bool designMode, string dataNavigateUrlFields, string dataNavigateUrlFormatString)
        
        internal static string GetNavigateUrlFormat(Control control, bool designMode, string dataNavigateUrlFields, string dataNavigateUrlFormatString)
        {
            string[] fields = dataNavigateUrlFields.Split(',');

            object[] fieldValues = new object[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                fieldValues[i] = DataBindingUtil.GetDataItemValue(control, designMode, fields[i]);
            }

            return string.Format(dataNavigateUrlFormatString, fieldValues);
        }

        #endregion
    }
}
