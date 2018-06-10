using BonoCorpAleman.ViewModels;
using BonoCorpAleman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Diagnostics;
using BonoCorpAleman.Models;

namespace BonoCorpAleman.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            var viewModel = new LoginViewModel();
            viewModel.FindUsuario(context);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var Usuario = context.Entidad.FirstOrDefault(x => x.ID_email == model.Email && x.Password == model.Password);
            model.FindUsuario(context);
            if (Usuario == null)
            {
                ViewBag.Mensaje = "Email o Contraseña Incorrectos. ";
                //return RedirectToAction("Index", "Home");
                return View(model);
            }
            Session.SetUsuarioId(model.Usuario.ID_email);
            Session.SetUsuario(model.Usuario);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddEditUser(String Email)
        {
            var viewModel = new AddEditUserViewModel();
            viewModel.CargarDatos(context, Email);
            //viewModel.Email = Session.GetUsuarioId();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEditUser(AddEditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TryUpdateModel(model);
                Debug.WriteLine("AEuser - no es valido");
                return View(model);
            }
            try
            {
                /*TODO O NADA*/
                using(var transactionScope = new TransactionScope())
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
                    transactionScope.Complete();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                model.CargarDatos(context, model.Email);
                Debug.WriteLine("AEuser - catch");
                TryUpdateModel(model);
                return View(model);
            }
        }
    }
}