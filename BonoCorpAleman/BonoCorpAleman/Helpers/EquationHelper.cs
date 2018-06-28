using BonoCorpAleman.TablaObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;

namespace BonoCorpAleman.Helpers
{
    public static class EquationHelper
    {
        public static double Percent(this object val)
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
        public static IDictionary<int, char> PlazoDeGracia(this Models.Bono bono)
        {
            var plazos = bono.PlazoBono.Select(x => new
            {
                Plazo = (x.PlazoGracia.Nombre == "Total") ? 'T' : (x.PlazoGracia.Nombre == "Parcial") ? 'P' : 'S',
                Periodo = x.Periodo
            }).OrderBy(x=>x.Periodo).ToDictionary(x => x.Periodo, x => x.Plazo);
            return plazos;
        }
        public static IDictionary<int, double> InflacionAnual(this Models.Bono bono)
        {
            var IA = bono.Inflacion.OrderBy(x=>x.Periodo).ToDictionary(x => x.Periodo, x => x.Valor);
            return IA;
        }
        public static IDictionary<int, double> TasaEfectivaAnual(this Models.Bono bono)
        {
            IDictionary<int, double> list = new Dictionary<int, double>();
            var tasas = bono.Bono_Tasa.OrderBy(x => x.NroCuota).ToList();
            foreach(var tasa in tasas)
            {
                Debug.WriteLine(tasa.TasaInteres);
                if (tasa.TipoTasa_ID != 1)
                {
                    list.Add(tasa.NroCuota, tasa.TasaInteres);
                }else
                {
                    var Conversion = Math.Pow(1 + tasa.TasaInteres.Percent() / (bono.DiasPorAnio / tasa.Capitalizacion1.ID.CapVal()), (bono.DiasPorAnio / tasa.Capitalizacion1.ID) - 1) * 100;//falta cap
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
                double aux = TEA.Value.Percent();
                double sumAux = 1.00 + aux;
                double exponenete = (double)bono.FrecCupon / (double)bono.DiasPorAnio;
                double valPorciento = (Math.Pow(sumAux, exponenete) - 1) * 100;
                list.Add(TEA.Key, valPorciento);
            }
            return list;
        }
        public static double COKperiodo(this Models.Bono bono)
        {
            return (Math.Pow(1 + bono.TasaAnualDescuento.Percent(), (double)bono.FrecCupon / (double)bono.DiasPorAnio) - 1) * 100;
        }
        public static double CostesInicialesEmisor(this Models.Bono bono)
        {
            var costes = bono.Costes_Gastos.OrderBy(x => x.ID).ToList();
            double sum = 0;
            foreach(var costo in costes)
            {
                if(costo.Nombre != "Prima")
                    sum += costo.Valor;
            }
            return sum.Percent() * bono.ValorComercial;
        }
        public static double CostesInicialesBonista(this Models.Bono bono)
        {
            var costes = bono.Costes_Gastos.OrderBy(x => x.ID).ToList();
            double sum = 0;
            foreach(var coste in costes)
            {
                if (coste.Nombre == "Flotacion" || coste.Nombre == "CAVALI")
                    sum += coste.Valor;
            }
            return sum.Percent() * bono.ValorComercial;
        }
        public static double PrecioActual(this Calculo Tabla)
        {
            double resultado = 0;
            for(int i = 1; i < Tabla.size; i++)
            {
                resultado += Tabla.tabla[i].FlujoBonista / Math.Pow(1 + Tabla.bono.COKperiodo().Percent(), i);
            }
            return resultado;
        }
        public static double UtilidadPerdida(this Calculo Tabla)
        {
            double resultado = 0;
            resultado = Tabla.tabla[0].FlujoBonista + Tabla.PrecioActual();
            return resultado;
        }
        public static double Duracion(this Calculo Tabla)
        {
            double sumFAxPlazo = 0, sumFlujoAct = 0;
            sumFAxPlazo = Tabla.tabla.Sum(x => x.FAxPlazo);
            sumFlujoAct = Tabla.tabla.Sum(x => x.FlujoActivo);
            return sumFAxPlazo / sumFlujoAct;
        }
        public static double Convexidad(this Calculo Tabla)
        {
            double sumFactorConvexidad = 0, sumFlujoAct = 0;
            sumFactorConvexidad = Tabla.tabla.Sum(x => x.FactorConvexidad);
            sumFlujoAct = Tabla.tabla.Sum(x => x.FlujoActivo);
            var res = sumFactorConvexidad / (Math.Pow(1 + Tabla.bono.COKperiodo().Percent(), 2) * sumFlujoAct * Math.Pow(Tabla.bono.DiasPorAnio / Tabla.bono.FrecCupon, 2));
            return res;
        }
        public static double Total(this Calculo Tabla)
        {
            return Tabla.Duracion() + Tabla.Convexidad();
        }
        public static double DuracionModificada(this Calculo Tabla)
        {
            return Tabla.Duracion() / (1 + Tabla.bono.COKperiodo().Percent());
        }
        public static double TCEA_Emisor(this Calculo Tabla)
        {
            var listFlujoEmisor = Tabla.tabla.Select(X => X.FlujoEmisor).ToArray();
            var listFechaProgramada = Tabla.tabla.Select(x => (double)x.FechaProgramada.Day).ToArray();

            var IRR = Financial.IRR(ref listFlujoEmisor);
            return (Math.Pow(IRR + 1, Tabla.bono.DiasPorAnio / Tabla.bono.FrecCupon) - 1) * 100;
        }
        public static double TCEA_EmisorConEscudo(this Calculo Tabla)
        {
            var listFlujoEmisorEscudo = Tabla.tabla.Select(X => X.FlujoEmisorEscudo).ToArray();
            var listFechaProgramada = Tabla.tabla.Select(x => (double)x.FechaProgramada.Day).ToArray();

            var IRR = Financial.IRR(ref listFlujoEmisorEscudo);
            return (Math.Pow(IRR + 1, Tabla.bono.DiasPorAnio / Tabla.bono.FrecCupon) - 1) * 100;
        }
        public static double TREA_Bonista(this Calculo Tabla)
        {
            var listFlujoBonista = Tabla.tabla.Select(X => X.FlujoBonista).ToArray();
            var listFechaProgramada = Tabla.tabla.Select(x => (double)x.FechaProgramada.Day).ToArray();

            var IRR = Financial.IRR(ref listFlujoBonista);
            return (Math.Pow(IRR + 1, Tabla.bono.DiasPorAnio / Tabla.bono.FrecCupon) - 1) * 100;
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
        public static int CapVal(this object CapitalizacionId)
        {
            switch ((int)CapitalizacionId)
            {
                case 1:
                    return 1;
                case 2:
                    return 15;
                case 3:
                    return 30;
                case 4:
                    return 60;
                case 5:
                    return 90;
                case 6:
                    return 120;
                case 7:
                    return 180;
                case 8:
                    return 360;
                default:
                    return -99;
            }
        }
        public static double computeIRR(this double []cf, int numOfFlows)
        {
            int i = 0, j = 0;
            double m = 0.0;
            double old = 0.00;
            double neww = 0.00;
            double oldguessRate = 0.01;
            double newguessRate = 0.01;
            double guessRate = 0.01;
            double lowGuessRate = 0.01;
            double highGuessRate = 0.5;
            double npv = 0.0;
            double denom = 0.0;
            for (i = 0; i < 1000; i++)
            {
                npv = 0.00;
                for (j = 0; j < numOfFlows; j++)
                {
                    denom = Math.Pow((1 + guessRate), j);
                    npv = npv + (cf[j] / denom);
                }
                /* Stop checking once the required precision is achieved */
                if ((npv > 0) && (npv < 0.00000001))
                    break;
                if (old == 0)
                    old = npv;
                else
                    old = neww;
                neww = npv;
                if (i > 0)
                {
                    if (old < neww)
                    {
                        if (old < 0 && neww < 0)
                            highGuessRate = newguessRate;
                        else
                            lowGuessRate = newguessRate;
                    }
                    else
                    {
                        if (old > 0 && neww > 0)
                            lowGuessRate = newguessRate;
                        else
                            highGuessRate = newguessRate;
                    }
                }
                oldguessRate = guessRate;
                guessRate = (lowGuessRate + highGuessRate) / 2;
                newguessRate = guessRate;
            }
            return guessRate;
        }
    }
}