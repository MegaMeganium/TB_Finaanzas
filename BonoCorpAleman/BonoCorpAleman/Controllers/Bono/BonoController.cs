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

namespace BonoCorpAleman.Controllers.Bono
{
    public class BonoController : BaseController
    {
        // GET: Bono
        public ActionResult Index()
        {
            return View();
        }

        public void add()
        {

        }
        [HttpGet]
        public ActionResult IBono(Int32? bonoId)
        {
            var viewModel = new AddEditBonoViewModel();
            viewModel.CargarDatos(context, bonoId);
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
                    var bono = new Models.Bono();
                    if (model.BonoId.HasValue)
                    {
                        Debug.WriteLine("IBono - if:editar");
                        bono = context.Bono.Find(model.BonoId);
                    }else
                    {
                        Debug.WriteLine("IBono - else:agregar");
                        context.Bono.Add(bono);
                    }
                    model.userId = Session.GetUsuarioId();
                    model.TransferModel(ref bono);

                    Debug.WriteLine("IBono - antes de guardar");
                    context.SaveChanges();
                    transactionScope.Complete();

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                model.CargarDatos(context, model.BonoId);
                Debug.WriteLine("AEuser - catch -> "+ex.Message+"\n"+ex.InnerException+"\n");
                TryUpdateModel(model);
                return View(model);
            }
        }

        public ActionResult IPrueba(AddEditUserViewModel model, FormCollection form)
        {
            var viewModel = new AddEditUserViewModel();

            return View(viewModel);
        }
    }
}