using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.Helpers
{
    public static class ConvertHelper
    {
        public static String To_String(this object val, String def = "error")
        {
            if (val == null)
                return def;
            try
            {
                return Convert.ToString(val);
            }
            catch (Exception)
            {
                return def;
            }
        }

        public static bool IsNullOrEmpty(this String val)
        {
            return (val == null || val.Length > 0);
        }
    }
}