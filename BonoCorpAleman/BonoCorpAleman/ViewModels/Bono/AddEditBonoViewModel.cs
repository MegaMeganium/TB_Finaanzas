using BonoCorpAleman.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BonoCorpAleman.ViewModels.Bono
{
    public class AddEditBonoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int? BonoId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public String userId { get; set; }

        [Required(ErrorMessage = "Coloque un monto")]
        [Display(Name ="Valor Nominal")]
        public decimal ValorNominal { get; set; }

        [Required(ErrorMessage = "Coloque un monto")]
        [Display(Name = "Valor Comercial")]
        public int ValorComercial { get; set; }

        [Required(ErrorMessage = "¿Cuantos Años?")]
        [Display(Name = "N° Años")]
        public int nAnios { get; set; }

        [Required(ErrorMessage = "Escoja una Frecuencia")]
        [Display(Name = "Frecuencia del Cupon")]
        public int? frecCupon { get; set; } = 1;

        [Required(ErrorMessage = "¿Cuantos dias por año?")]
        [Range(360, 365, ErrorMessage = "debe colocar un valor entre 360 y 365")]
        [Display(Name = "Dias por año")]
        public int diasXanio { get; set; }

        [Required(ErrorMessage = "¿El Tipo de Tasa?")]
        [Range(0, 100, ErrorMessage = "debe colocar un valor entre 0 y 100%")]
        [Display(Name = "Tipo de Tasa de Interes")]
        public int TasaId { get; set; }

        [Display(Name = "Capitalizacion")]
        public int? capitalizacionId { get; set; } = 1;/*defaul value Diaria = 1*/

        [Required(ErrorMessage = "¿La Tasa?")]
        [Range(0, 100, ErrorMessage = "debe colocar un valor entre 0 y 100%")]
        [Display(Name = "Tasa de Interes")]
        public int TasaInteres { get; set; }

        [Required(ErrorMessage = "Coloque un valor")]
        [Range(0, 100, ErrorMessage = "debe colocar un valor entre 0 y 100%")]
        [Display(Name = "Tasa Anual de Descuento")]
        public int TasaAnualDescuento { get; set; }

        [Required(ErrorMessage = "¿Cual es el impuesto a la renta?")]
        [Range(0, 100, ErrorMessage = "debe colocar un valor entre 0 y 100%")]  
        [Display(Name = "Impuesto a la Renta")]
        public int ImpRenta { get; set; }

        [Required(ErrorMessage = "¿Fecha?")]
        [Display(Name = "Fecha de Emision")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaEmision { get; set; }
        
        public List<int> LstTasaId { get; set; }
        public List<int> LstValorTasa { get; set; }

        public List<Bono_Tasa> LstBonoTasa { get; set; }
        public List<Inflacion> LstInflacion { get; set; }
        public List<Costes_Gastos> LstCostes_Gastos { get; set; }
        public List<PlazoBono> LstPlazoBono { get; set; }
        public List<Capitalizacion> LstCapitalizacion { get; set; }
        public List<TipoTasa> LstTipoTasa { get; set; }
        public List<object> LstFrecuencia = new List<object>() {
            new {Dias = 30, Nombre = "Mensual"},
            new {Dias = 60, Nombre = "Bimestral"},
            new {Dias = 90, Nombre = "Trimestral"},
            new {Dias = 120, Nombre = "Cuatrimestral"},
            new {Dias = 180, Nombre = "Semestral"},
            new {Dias = 360, Nombre = "Anual"}
        };

        public IEnumerable<SelectListItem> GetFrecuencias
        {
            get { return new SelectList(LstFrecuencia, "Dias", "Nombre"); }
        }
        public IEnumerable<SelectListItem> GetCapitalizaciones
        {
            get { return new SelectList(LstCapitalizacion, "ID", "Nombre"); }
        }

        public void CargarDatos(BonoCorpAlemanEntities1 context, Int32? bonoId)
        {
            this.BonoId = bonoId;
            var bono = context.Bono.Find(BonoId);
            if (bono != null)
            {
                userId = bono.Entidad_ID;
                ValorNominal = bono.ValorNominal;
                ValorComercial = bono.ValorComercial;
                nAnios = bono.NroAnios;
                frecCupon = bono.FrecCupon;
                diasXanio = bono.DiasPorAnio;
                ImpRenta = bono.ImpRenta;
                FechaEmision = bono.FechaEmision;
                TasaAnualDescuento = bono.TasaAnualDescuento;

                LstInflacion = bono.Inflacion.ToList();
                LstCostes_Gastos = bono.Costes_Gastos.ToList();
                LstBonoTasa = bono.Bono_Tasa.ToList();
                LstPlazoBono = bono.PlazoBono.ToList();
            }
            LstCapitalizacion = context.Capitalizacion.OrderBy(x => x.ID).ToList();
            LstTipoTasa = context.TipoTasa.OrderBy(x => x.ID).ToList();
        }

    }
}