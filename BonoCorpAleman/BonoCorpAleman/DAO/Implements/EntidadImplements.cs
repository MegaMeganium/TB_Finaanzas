using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BonoCorpAleman.DAO.Interface;
using BonoCorpAleman.Models;
using System.Transactions;
using BonoCorpAleman.ViewModels;
using System.Diagnostics;
using BonoCorpAleman.Helpers;

namespace BonoCorpAleman.DAO.Implements
{
    public class EntidadImplements : IEntidad
    {
        public BonoCorpAlemanEntities1 context { get; set; } = new BonoCorpAlemanEntities1();

        public void AddEditEntity(AddEditUserViewModel model)
        {
            var user = new Entidad();
            if (model.Exist(context))//editar
            {
                Debug.WriteLine("AEuser - if: editar");
                user = context.Entidad.Find(model.Email);
            }else
            {//agregar
                Debug.WriteLine("AEuser - else: agregar");
                context.Entidad.Add(user);
            }

            model.TransferModel(ref user);

            Debug.WriteLine("AEuser - antes de guardar");
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var obj = Find(id);
            context.Entidad.Attach(obj);
            context.Entidad.Remove(obj);
            context.SaveChanges();
        }

        public void Delete(Entidad obj)
        {
            context.Entidad.Attach(obj);
            context.Entidad.Remove(obj);
            context.SaveChanges();
        }

        public List<Entidad> FilterName(string filter)
        {
            throw new NotImplementedException();
        }

        public Entidad Find(string id)
        {
            return context.Entidad.Find(id);
        }

        public IList<Entidad> GetAll()
        {
            return context.Entidad.OrderBy(x => x.ID_email).ToList();
        }
    }
}