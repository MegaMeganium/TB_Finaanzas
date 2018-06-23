using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BonoCorpAleman.Models;
using BonoCorpAleman.Helpers;

namespace BonoCorpAleman.ViewModels.Bono
{
    public class ListBonoViewModel
    {
        public ICollection<Models.Bono> LstBono { get; set; }

        public IQueryable<Models.Bono> GetBonoByUserId(BonoCorpAlemanEntities1 context, string userId)
        {
            return context.Bono.Where(x => x.Entidad_ID == userId).OrderBy(x => x.BonoID);
        }

        public IQueryable<Models.Bono> GetBonoByUserIdAndFilter(BonoCorpAlemanEntities1 context, string userId, string filter)
        {
            return context.Bono_Tasa.Where(x => x.Bono.Entidad_ID == userId && x.TasaInteres == filter.ToDouble()).Select(x=>x.Bono).OrderBy(x => x.BonoID);
        }

        public void CargarDatos(BonoCorpAlemanEntities1 context, string userId, string filtro)
        {
            if (filtro.IsNullOrEmpty())
                LstBono = GetBonoByUserId(context, userId).ToList();
            else
                LstBono = GetBonoByUserIdAndFilter(context, userId, filtro).ToList();
        }
    }
}