using BonoCorpAleman.ViewModels.Bono;
using System;
using BonoCorpAleman.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using BonoCorpAleman.ViewModels;
using BonoCorpAleman.DAO.Implements;
using BonoCorpAleman.DAO.Interface;
using BonoCorpAleman.TablaObject;

namespace BonoCorpAleman.Controllers.Bono
{
    public class BonoController : BaseController
    {
        IBono BonoDAO { get; set; } = new BonoImplements();
        // GET: Bono
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ListarBono(string filter)
        {
            var viewModel = new ListBonoViewModel();
            viewModel.CargarDatos(BonoDAO.context, Session.GetUsuarioId(), filter);
            return View(viewModel);
        }

        public ActionResult BorrarBono(int bonoId, string filter)
        {
            BonoDAO.Delete(bonoId);
            var viewModel = new ListBonoViewModel();
            viewModel.CargarDatos(BonoDAO.context, Session.GetUsuarioId(), filter);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult IBono(Int32? bonoId)
        {
            var viewModel = new AddEditBonoViewModel();
            viewModel.CargarDatos(BonoDAO.context, bonoId);
            return View(viewModel);
        }

        [HttpPost]
        public  ActionResult IBono(AddEditBonoViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                TryUpdateModel(model);
                Debug.WriteLine("IBono - no es valido");
                return View(model);
            }
            try
            {
                using(var transactionScope = new TransactionScope())
                {
                    //Le tengo que especificar que es del Model ya que hay un namespace con Bono y se confunde
                    model.userId = Session.GetUsuarioId();
                    BonoDAO.AddEditEntity(model);
                    Debug.WriteLine("IBono - antes de TransactionScope");
                    transactionScope.Complete();
                    Debug.WriteLine("IBono - Redireccionando al Index");

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                model.CargarDatos(BonoDAO.context, model.BonoId);
                Debug.WriteLine("IBono - catch -> "+ex.Message+"\n"+ex.InnerException+"\n");
                TryUpdateModel(model);
                return View(model);
            }
        }

        public ActionResult RBono(int? bonoId)
        {
            if (!bonoId.HasValue)
            {
                Debug.WriteLine("RBono - ID del bono es nullo");
                return RedirectToAction("Error", "Home");
            }
            var bono = BonoDAO.Find(bonoId.Value);
            if (bono == null)
            {
                Debug.WriteLine("RBono - objeto bono no existe");
                return RedirectToAction("Error", "Home");
            }
            try
            {
                var Tabla = new Calculo(bono);
                if(Tabla != null)
                    Debug.WriteLine("RBono - Tabla no nullo " + Tabla.bono.BonoID);
                Debug.WriteLine("RBono - antes de cargar datos");
                Tabla.CargarDatos();
                Debug.WriteLine("RBono - cargaron los datos correctamente");
                return View(Tabla);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}