using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Likol.Web
{
    internal class Global
    {
        internal static void TimeIsValid()
        {
            int year = 2012;
            int month = 6;
            int day = 30;

            DateTime dtFree = new DateTime(year, month, day);

            if (DateTime.Now > dtFree) throw new Exception("License has expired.");
        }
    }
}
