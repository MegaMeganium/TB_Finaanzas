using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.Helpers
{
    public static class ConvertHelper
    {
        public static string ToDate (this DateTime date)
        {
            var dia = date.Day.ToString();
            var mes = date.Month.ToString();
            if (dia.Length == 1)
                dia = "0" + dia;
            if (mes.Length == 1)
                mes = "0" + mes;
            return dia + "/" + mes + "/" + date.Year.ToString();
        }
        public static double RoundDec(this object val, int dec = 2)
        {
            return Math.Round((double)val, dec);
        }
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