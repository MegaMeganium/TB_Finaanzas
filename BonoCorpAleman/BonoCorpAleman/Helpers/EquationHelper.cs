using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.Helpers
{
    public static class EquationHelper
    {
        public static double Percent (this object val)
        {
            return (double)val / 100;
        }

        public static int NroPeriodosPorAnio(this Models.Bono bono)
        {
            return bono.DiasPorAnio / bono.FrecCupon;
        }
        public static int NroTatalPeriodos(this Models.Bono bono)
        {
            return bono.NroAnios * bono.NroPeriodosPorAnio();
        }
        public static IDictionary<int, double> TasaEfectivaAnual(this Models.Bono bono)
        {
            IDictionary<int, double> list = new Dictionary<int, double>();
            var tasas = bono.Bono_Tasa.OrderBy(x => x.NroCuota).
                Select(x => new {
                            Tasa = x.TipoTasa_ID == 1 ? "Nominal" : "Efectiva",
                            TasaInteres = x.TasaInteres,
                            Capitalizacion = x.Capitalizacion1.ID,
                            NroCuota = x.NroCuota }
                      ).ToList();
            foreach(var tasa in tasas)
            {
                if (tasa.Tasa.Equals("Efectiva"))
                {
                    list.Add(tasa.NroCuota, tasa.TasaInteres);
                }else
                {
                    var Conversion = Math.Pow(1 + tasa.TasaInteres / (bono.DiasPorAnio / tasa.Capitalizacion), (bono.DiasPorAnio / tasa.Capitalizacion) - 1);
                    list.Add(tasa.NroCuota, Conversion);
                }
            }
            return list;
        }
        public static IDictionary<int, double> TasaEfectivaPeriodo(this Models.Bono bono)
        {
            IDictionary<int, double> list = new Dictionary<int, double>();
            var TEAS = bono.TasaEfectivaAnual();
            foreach(var TEA in TEAS)
            {
                list.Add(TEA.Key, Math.Pow(1 + TEA.Value, bono.FrecCupon / bono.DiasPorAnio) - 1);
            }
            return list;
        }
        public static double COKperiodo(this Models.Bono bono)
        {
            return Math.Pow(1 + bono.TasaAnualDescuento, bono.FrecCupon / bono.DiasPorAnio) - 1;
        }
        public static double CostesInicialesEmisor(this Models.Bono bono)
        {
            var costes = bono.Costes_Gastos.OrderBy(x => x.ID).ToList();
            double sum = 0;
            foreach(var costo in costes)
            {
                if(!costo.Nombre.Equals("Prima"))
                    sum += costo.Valor;
            }
            return sum * bono.ValorComercial;
        }
        public static double CostesInicialesBonista(this Models.Bono bono)
        {
            var costes = bono.Costes_Gastos.OrderBy(x => x.ID).ToList();
            double sum = 0;
            foreach(var coste in costes)
            {
                if (coste.Nombre == "Flotacion" && coste.Nombre == "CAVALI")
                    sum += coste.Valor;
            }
            return sum * bono.ValorComercial;
        }
        public static DateTime getFechaByIndex(this Models.Bono bono, int index)
        {
            return (index == 0) ? bono.FechaEmision : (bono.FechaEmision.AddDays(index * bono.FrecCupon));
        }
        public static double Prima(this Models.Bono bono)
        {
            return bono.Costes_Gastos.FirstOrDefault(x => x.Nombre == "Prima").Valor;
        }
        public static double Estructuracion(this Models.Bono bono)
        {
            return bono.Costes_Gastos.FirstOrDefault(x => x.Nombre == "Estructuracion").Valor;
        }
        public static double Colocacion(this Models.Bono bono)
        {
            return bono.Costes_Gastos.FirstOrDefault(x => x.Nombre == "Colocacion").Valor;
        }
        public static double Flotacion(this Models.Bono bono)
        {
            return bono.Costes_Gastos.FirstOrDefault(x => x.Nombre == "Flotacion").Valor;
        }
        public static double CAVALI(this Models.Bono bono)
        {
            return bono.Costes_Gastos.FirstOrDefault(x => x.Nombre == "CAVALI").Valor;
        }
    }
}