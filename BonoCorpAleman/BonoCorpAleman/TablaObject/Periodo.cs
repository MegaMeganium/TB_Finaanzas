using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BonoCorpAleman.Helpers;

namespace BonoCorpAleman.TablaObject
{
    public class Periodo
    {
        public int I { get; set; } = 0;
        public double TasaEfectivaAnual { get; set; } = 0;
        public double TasaEfectivaPeriodo { get; set; } = 0;
        public DateTime FechaProgramada { get; set; } = DateTime.Now;
        public double InflacionAnual { get; set; } = 0;
        public double InflacionPeriodo { get; set; } = 0;
        public char PlazoGracia { get; set; } = 'S';
        public double Bono { get; set; } = 0;
        public double BonoIndexado { get; set; } = 0;
        /// <summary>
        /// (Interes)
        /// </summary>
        public double Cupon { get; set; } = 0;
        public double Cuota { get; set; } = 0;
        public double Amortizacion { get; set; } = 0;
        public double Prima { get; set; } = 0;
        public double Escudo { get; set; } = 0;
        public double FlujoEmisor { get; set; } = 0;
        /// <summary>
        /// Flujo Emisor con Escudo
        /// </summary>
        public double FlujoEmisorEscudo { get; set; } = 0;
        public double FlujoBonista { get; set; } = 0;
        public double FlujoActivo { get; set; } = 0;
        /// <summary>
        /// Flujo del Activo por Plazo
        /// </summary>
        public double FAxPlazo { get; set; } = 0;
        /// <summary>
        /// Factor por Convexidad
        /// </summary>
        public double FactorConvexidad { get; set; } = 0;
    }
}