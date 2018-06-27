using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BonoCorpAleman.Helpers;
using System.Diagnostics;

namespace BonoCorpAleman.TablaObject
{
    public class Calculo 
    {
        public Models.Bono bono;
        public Periodo[] tabla;
        public int size;

        public Calculo(Models.Bono bono)
        {
            this.bono = bono;
            size = bono.NroTatalPeriodos() + 1;
            tabla = new Periodo[size];
            for (int i = 0; i < size; i++)
                tabla[i] = new Periodo();
        }
        private void PeriodoZero()
        {
            Debug.WriteLine("Calculo - PeriodoZero ejecutando " + size);
            tabla[0].I = 0;
            tabla[0].PlazoGracia = ' ';
            tabla[0].FechaProgramada = bono.FechaEmision;
            tabla[0].FlujoEmisor = bono.ValorComercial - bono.CostesInicialesEmisor();
            tabla[0].FlujoEmisorEscudo = tabla[0].FlujoEmisor;
            tabla[0].FlujoBonista = (bono.ValorComercial*-1) - bono.CostesInicialesBonista();
            Debug.WriteLine("Calculo - PeriodoZeroTermino");
        }
        public void CargarDatos()
        {
            Debug.WriteLine("Calculo - Entro a Cargar Datos");
            var IAS = bono.InflacionAnual();
            Debug.WriteLine("Calculo - Despues de IAS");
            var TEAS = bono.TasaEfectivaAnual();
            var TEPS = bono.TasaEfectivaPeriodo();
            Debug.WriteLine("Calculo - Despues de TEPS");
            var PGS = bono.PlazoDeGracia();
            Debug.WriteLine("Calculo - Antes de ejecutar perido zero");
            /*if (!TEPS.ContainsKey(1))
                throw new Exception("No hay una Tasa en el periodo 1");
            if (!IAS.ContainsKey(1))
                throw new Exception("No hay una Inflacion en el periodo 1");*/
            PeriodoZero();
            Debug.WriteLine("Calculo - despues de ejecutar perido zero");
            double tep = TEPS[1], tea = TEAS[1];
            double ia = IAS[1];
            Debug.WriteLine("Calculo - Antes de for");
            for (int i = 1; i < size; i++)
            {
                tabla[i].I = i;
                tabla[i].FechaProgramada = bono.getFechaByIndex(i);
                if (i > 1 && IAS.ContainsKey(i))
                {
                    ia = IAS[i];
                    tabla[i].InflacionAnual = ia;
                }
                else
                {
                    tabla[i].InflacionAnual = ia;
                }
                tabla[i].InflacionPeriodo = (Math.Pow(1 + tabla[i].InflacionAnual.Percent(), (double)bono.FrecCupon / (double)bono.DiasPorAnio) - 1) * 100;
                if (PGS.ContainsKey(i))
                {
                    tabla[i].PlazoGracia = PGS[i];
                }
                else
                {
                    tabla[i].PlazoGracia = 'S';
                }
                tabla[i].Bono = (i == 1) ? bono.ValorNominal : (tabla[i-1].PlazoGracia == 'T') ? tabla[i-1].BonoIndexado - tabla[i-1].Cupon : tabla[i-1].BonoIndexado + tabla[i-1].Amortizacion;
                tabla[i].BonoIndexado = tabla[i].Bono * (1 + tabla[i].InflacionPeriodo.Percent());
                if (i > 1 && TEPS.ContainsKey(i))
                {
                    tep = TEPS[i];
                    tea = TEAS[i];
                    tabla[i].TasaEfectivaAnual = tea;
                    tabla[i].TasaEfectivaPeriodo = tep;
                    tabla[i].Cupon = (-1 * tabla[i].BonoIndexado) * tep.Percent();
                }
                else
                {
                    tabla[i].TasaEfectivaAnual = tea;
                    tabla[i].TasaEfectivaPeriodo = tep;
                    tabla[i].Cupon = (-1 * tabla[i].BonoIndexado) * tep.Percent();
                }
                tabla[i].Amortizacion = (tabla[i].PlazoGracia == 'T' || tabla[i].PlazoGracia == 'P') ? 0 : (-1 * tabla[i].BonoIndexado) / (bono.NroTatalPeriodos() - i + 1);
                tabla[i].Cuota = (tabla[i].PlazoGracia == 'T') ? 0 : (tabla[i].PlazoGracia == 'P') ? tabla[i].Cupon : tabla[i].Cupon + tabla[i].Amortizacion;
                tabla[i].Prima = (i == size - 1) ? bono.Prima().Percent() * bono.ValorNominal * -1 : 0;
                tabla[i].Escudo = (-1 * tabla[i].Cupon) * bono.ImpRenta.Percent();
                tabla[i].FlujoEmisor = tabla[i].Cuota + tabla[i].Prima;
                tabla[i].FlujoEmisorEscudo = tabla[i].FlujoEmisor + tabla[i].Escudo;
                tabla[i].FlujoBonista = (tabla[i].FlujoEmisor * -1);
                tabla[i].FlujoActivo = tabla[i].FlujoBonista / Math.Pow(1 + bono.COKperiodo().Percent(), i);
                tabla[i].FAxPlazo = tabla[i].FlujoActivo * i * (double)bono.FrecCupon / (double)bono.DiasPorAnio;
                tabla[i].FactorConvexidad = tabla[i].FlujoActivo * i * (1 + i);
            }
        }

    }
}