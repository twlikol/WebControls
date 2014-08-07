using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Likol.Web.Resources;
using System.Web.UI.WebControls;
using System.Web.UI;

[assembly: WebResource("Likol.Web.Resources.Images.ValidatorError.gif", "image/gif")]

namespace Likol.Web.UI.Validation
{
    internal static class CommonValidatorRender
    {
        internal static void ValidatorRender(BaseValidator baseValidator, string imageUrl)
        {
            string validatorImageHTML = "<img alt=\"{0}\" title=\"{0}\" align=\"absmiddle\" src=\"{1}\" />";

            if (imageUrl != "")
            {
                baseValidator.Text = string.Format(validatorImageHTML,
                    baseValidator.ErrorMessage,
                    baseValidator.ResolveUrl(imageUrl));
            }
            else
            {
                baseValidator.Text = string.Format(validatorImageHTML,
                    baseValidator.ErrorMessage,
                    ResourceManager.GetImageWebResourceUrl(baseValidator, "ValidatorError"));
            }
        }
    }
}
