using BonoCorpAleman.DAO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BonoCorpAleman.Models;
using BonoCorpAleman.ViewModels.Bono;
using System.Diagnostics;
using BonoCorpAleman.Helpers;

namespace BonoCorpAleman.DAO.Implements
{
    public class BonoImplements : IBono
    {
        public BonoCorpAlemanEntities1 context { get; set; } = new BonoCorpAlemanEntities1();

        public void AddEditEntity(AddEditBonoViewModel model)
        {
            var bono = new Models.Bono();
            if (model.BonoId.HasValue)
            {
                Debug.WriteLine("IBono - if:editar");
                bono = context.Bono.Find(model.BonoId);
            }
            else
            {
                Debug.WriteLine("IBono - else:agregar");
                context.Bono.Add(bono);
            }
            model.TransferModel(ref bono);

            Debug.WriteLine("IBono - antes de guardar");
            context.SaveChanges();
        }

        public void Delete(Bono obj)
        {
            context.Bono.Attach(obj);
            context.Bono.Remove(obj);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            try
            {
                var obj = Find(id);
                context.Bono.Attach(obj);
                context.Bono.Remove(obj);
                context.SaveChanges();
            }catch(Exception ex)
            {
                Debug.WriteLine("delete Bono - " + ex.InnerException);
                return;
            }
        }

        public Bono Find(long id)
        {
            return context.Bono.Find(id);
        }

        public IList<Bono> GetAll()
        {
            return context.Bono.OrderBy(x => x.BonoID).ToList();
        }

        public IList<Bono> GetAllByUserId(string id)
        {
            return context.Bono.Where(x => x.Entidad_ID == id).OrderBy(x => x.BonoID).ToList();
        }

        public IList<Bono> GetAllByUserIdAndFilter(string id, string filter)
        {
            return context.Bono_Tasa.Where(x => x.Bono.Entidad_ID == id && x.TasaInteres == filter.ToDouble()).
                    Select(x => x.Bono).OrderBy(x => x.BonoID).ToList();
        }
    }
}