using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BonoCorpAleman.Helpers;

namespace BonoCorpAleman.TablaObject
{
    public class Calculo
    {
        public Models.Bono bono { get; set; }
        public IList<Periodo> tabla { get; set; }

        public Calculo(Models.Bono bono)
        {
            this.bono = bono;
            tabla = new List<Periodo>(bono.NroTatalPeriodos() + 1);
        }
        private void PeriodoZero()
        {
            tabla[0].FechaProgramada = bono.FechaEmision.Date.ToString();
            tabla[0].FlujoEmisor = bono.ValorComercial + bono.CostesInicialesEmisor();
            tabla[0].FlujoEmisorEscudo = tabla[0].FlujoEmisor;
            tabla[0].FlujoBonista = (bono.ValorComercial*-1) - bono.CostesInicialesBonista();

        }
        public void CargarDatos()
        {
            //var inflacion = 0;
            var TEPS = bono.TasaEfectivaPeriodo();
            //var PGS = 0;
            PeriodoZero();
            if (!TEPS.ContainsKey(1))
                throw new Exception("No hay una Tasa en el periodo 1");

            double tep = TEPS[1];
            for (int i = 1; i < tabla.Count; i++)
            {
                tabla[i].FechaProgramada = bono.getFechaByIndex(i).Date.ToString();
                tabla[i].InflacionAnual = 0;//falta
                tabla[i].InflacionPeriodo = Math.Pow(1 + tabla[i].InflacionAnual.Percent(), bono.FrecCupon / bono.DiasPorAnio);
                tabla[i].PlazoGracia = 'S';//falta
                tabla[i].Bono = (i == 1) ? bono.ValorNominal : (tabla[i-1].PlazoGracia == 'T') ? tabla[i-1].BonoIndexado - tabla[i-1].Cupon : tabla[i-1].BonoIndexado + tabla[i-1].Amortizacion;
                tabla[i].BonoIndexado = tabla[i].Bono * (1 + tabla[i].InflacionPeriodo.Percent());
                if (i > 1 && TEPS.ContainsKey(i))
                {
                    tep = TEPS[i];
                    tabla[i].Cupon = (-1 * tabla[i].BonoIndexado) * tep;
                }
                else
                {
                    tabla[i].Cupon = (-1 * tabla[i].BonoIndexado) * tep;
                }
                tabla[i].Cuota = (tabla[i].PlazoGracia == 'T') ? 0 : (tabla[i].PlazoGracia == 'P') ? tabla[i].Cupon : tabla[i].Cupon + tabla[i].Amortizacion;
                tabla[i].Amortizacion = (tabla[i].PlazoGracia == 'T' || tabla[i].PlazoGracia == 'P') ? 0 : (-1 * tabla[i].BonoIndexado) / (bono.NroTatalPeriodos() - i + 1);
                tabla[i].Prima = (i == tabla.Count - 1) ? bono.Prima() * bono.ValorNominal : 0;
                tabla[i].Escudo = (-1 * tabla[i].Cupon) * bono.ImpRenta.Percent();
                tabla[i].FlujoEmisor = tabla[i].Cuota + tabla[i].Prima;
                tabla[i].FlujoEmisorEscudo = tabla[i].FlujoEmisor + tabla[i].Escudo;
                tabla[i].FlujoBonista = (tabla[i].FlujoEmisor * -1);
                tabla[i].FlujoActivo = tabla[i].FlujoBonista / Math.Pow(1 + bono.COKperiodo(), i);
                tabla[i].FAxPlazo = tabla[i].FlujoActivo * i * bono.FrecCupon / bono.DiasPorAnio;
                tabla[i].FactorConvexidad = tabla[i].FlujoActivo * i * (1 + i);
            }
        }
    }
}