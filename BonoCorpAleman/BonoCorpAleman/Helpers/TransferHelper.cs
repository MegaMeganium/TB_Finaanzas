using BonoCorpAleman.Models;
using BonoCorpAleman.ViewModels;
using BonoCorpAleman.ViewModels.Bono;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.Helpers
{
    public static class TransferHelper
    {
        public static void TransferModel(this AddEditUserViewModel model, ref Entidad user)
        {
            user.Nombre = model.Name;
            user.Apellido = model.LastName;
            user.Password = model.Password;
            user.ID_email = model.Email;
        }

        public static void TransferModel(this AddEditBonoViewModel model, ref Models.Bono bono/*, ref Models.Bono_Tasa bonoTasa*/)
        {
            //ID del bono se autogenera
            bono.Entidad_ID = "lagh3.30@gmail.com" /*model.userId*/;
            bono.ValorNominal = model.ValorNominal.ToDouble();
            bono.ValorComercial = model.ValorComercial.ToDouble();
            bono.NroAnios = model.nAnios;
            bono.FrecCupon = model.frecCupon;
            bono.DiasPorAnio = model.diasXanio;
            bono.ImpRenta = model.ImpRenta.ToDouble();
            bono.FechaEmision = model.FechaEmision;
            bono.TasaAnualDescuento = model.TasaAnualDescuento.ToDouble();

            if (model.LstTasaId.Count != model.LstValorTasa.Count)
            {
                Debug.WriteLine("TranferHelper-(bono, bonoTasa) sizes of List non equal");
                return;
            }
            Debug.WriteLine(model.LstValorTasa.Count+", "+ model.LstTasaId.Count);
            for (var i = 0; i < model.LstValorTasa.Count; i++)
            {
                Debug.WriteLine("i: "+ i +" -> "+ model.LstTasaId[i] + ", " + model.LstValorTasa[i].ToDouble() + ", "+ model.LstCapitalizaciones[i] +", "+model.LstNroPeriodos[i]);
            }
            for (var i = 0; i < model.LstValorTasa.Count; i++)
            {
                bono.Bono_Tasa.Add(new Bono_Tasa
                {
                    //ID del Bono_Tasa se autogenera
                    TipoTasa_ID = model.LstTasaId[i],
                    //BonoId se autogenera ya que es IDENTITY el pk del Bono
                    TasaInteres = model.LstValorTasa[i].ToDouble(),
                    NroCuota = model.LstNroPeriodos[i],
                    capitalizacion = model.LstCapitalizaciones[i].validacionCap(model.LstTasaId[i])
                });
            }

            for (var i = 0; i < model.LstValInflaciones.Count; i++)
            {
                bono.Inflacion.Add(new Inflacion
                {
                    Valor = model.LstValInflaciones[i].ToDouble(),
                    Periodo = model.LstPerInflaciones[i]
                });
            }

            for (var i = 0; i < model.LstPerPlazoBono.Count; i++){
                bono.PlazoBono.Add(new PlazoBono
                {
                    PlazoGracia_ID = model.LstPlazosDeGraciaId[i],
                    Periodo = model.LstPerPlazoBono[i]
                });
            }

            for (int i = 0; i < model.LstCostesGastosNombres.Count; i++)
            {
                bono.Costes_Gastos.Add(new Costes_Gastos
                {
                    Nombre = model.LstCostesGastosNombres[i],
                    Valor = model.LstCostesGastosValores[i].ToDouble(),
                    Emisor = model.LstCostesGastosEmisor[i]
                });
            }
        }
    }
}