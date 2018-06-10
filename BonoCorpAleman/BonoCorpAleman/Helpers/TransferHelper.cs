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
            bono.ValorNominal = model.ValorNominal;
            bono.ValorComercial = model.ValorComercial;
            bono.NroAnios = model.nAnios;
            bono.FrecCupon = model.frecCupon.Value;
            bono.DiasPorAnio = model.diasXanio;
            bono.ImpRenta = model.ImpRenta;
            bono.FechaEmision = model.FechaEmision;
            bono.TasaAnualDescuento = model.TasaAnualDescuento;

            if (model.LstTasaId.Count != model.LstValorTasa.Count)
            {
                Debug.WriteLine("TranferHelper-(bono, bonoTasa) sizes of List non equal");
                return;
            }
            Debug.WriteLine(model.LstValorTasa.Count+", "+ model.LstTasaId.Count);
            for (var i = 0; i < model.LstValorTasa.Count; i++)
            {
                Debug.WriteLine("i: "+ i +" -> "+model.LstValorTasa[i] + ", " + model.LstTasaId[i]);
            }
            for (var i = 0; i < model.LstValorTasa.Count; i++)
            {
                bono.Bono_Tasa.Add(new Bono_Tasa
                {
                    TipoTasa_ID = model.LstTasaId[i],
                    //BonoId se autogenera ya que es IDENTITY el pk del Bono
                    TasaInteres = model.LstValorTasa[i],
                    NroCuota = 2,
                    capitalizacion = model.capitalizacionId
                });
            }
        }
    }
}