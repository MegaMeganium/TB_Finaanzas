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

        public static double ToDouble(this object val)
        {
            if (val == null)
                return 0.0;
            try
            {
                return Convert.ToDouble(val);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        public static bool IsNullOrEmpty(this String val)
        {
            return (val == null || val.Length <= 0);
        }

        public static int? validacionCap(this int? val, int tasaId = 1)
        {
            if (tasaId != 1)
                return null;
            if (val == 0 || val == null || !val.HasValue)
                return null;
            return val;
        }
    }
}